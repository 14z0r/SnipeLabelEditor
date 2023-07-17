using BarcodeStandard;
using CoreHtmlToImage;
using HtmlAgilityPack;
using QRCoder;
using SkiaSharp;
using static QRCoder.Base64QRCode;

namespace SnipeLabelEditor.Data
{
    public static class HtmlConverterImagePDF
    {
        private static CoreHtmlToImage.HtmlConverter _imageConverter = new CoreHtmlToImage.HtmlConverter();
        private static CoreHtmlToPDF.HtmlConverter _pdfConverter = new CoreHtmlToPDF.HtmlConverter();
        private static QRCodeGenerator _generator = new QRCodeGenerator();
        private static Barcode _barcode = new Barcode();

        private static string RenderImageAsBase64(string html, int height, int width)
        {
            html = $"<div style=\"margin: -8px -8px -8px -8px;\"> {html} </div>";
            var bytes = _imageConverter.FromHtmlString(html, 50, CoreHtmlToImage.ImageFormat.Png, 100);

            if (height <= 0 || width <= 0)
            {
                return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bytes));
            }
            else
            {
                var image = SKImage.FromEncodedData(bytes);
                var bitmap = SKBitmap.FromImage(image);

                var pixmap = new SKPixmap(bitmap.Info, bitmap.GetPixels());
                SKRectI rectI = new SKRectI(0, 0, width, height);
                var subset = pixmap.ExtractSubset(rectI);
                var data = subset.Encode(SKEncodedImageFormat.Png, 100);
                return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(data.ToArray()));
            }
        }

        private static string RenderPDFAsBase64(string html, int height, int width, out string onlyBaseString)
        {
            html = $"<div style=\"margin: -8px -8px -8px -8px;\"> {html} </div>";

            var bytes = _pdfConverter.FromHtmlString(html, width, height);

            onlyBaseString = Convert.ToBase64String(bytes);

            return string.Format("data:application/pdf;base64,{0}", onlyBaseString);
        }

        private static string RenderQRCode(string data, int size)
        {
            QRCodeData qrCodeData = _generator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            return string.Format("data:image/png;base64,{0}", qrCode.GetGraphic(size, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, ImageType.Png));
        }

        private static string RenderBarcode(string data, int width, int height, bool includeLabel, int fonsize, string fontfamily, string codetype)
        {
            _barcode.IncludeLabel = includeLabel;

            var font = new SkiaSharp.SKFont();
            font.Size = fonsize;
            font.Typeface = SKTypeface.FromFamilyName(fontfamily);
            _barcode.LabelFont = font;

            BarcodeStandard.Type type = (BarcodeStandard.Type)Enum.Parse(typeof(BarcodeStandard.Type), codetype);

            var imagecode = _barcode.Encode(BarcodeStandard.Type.Code128, data, width, height);
            
            if (imagecode is null)
            {
                return string.Empty;
            }
            else 
            {
                var imageencoded = imagecode.Encode(SkiaSharp.SKEncodedImageFormat.Png, 100);
                return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageencoded.ToArray()));
            }
        }

        private static string ReplaceVariabels(string html, Dictionary<string, string> fields)
        {
            //Replace #name# variables with values.
            foreach (var item in fields)
            {
                html = html.Replace($"#{item.Key}#", item.Value);
            }

            return html;
        }

        public static string RenderLabel(string html, int heightpx, int widthpx, Dictionary<string, string> fields)
        {
            if(fields != null)
            {
                html = ReplaceVariabels(html, fields);
            }
            
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //Render QRcodes
            var qrcodenodes = htmlDocument.DocumentNode.SelectNodes("//qrcode");
            if (qrcodenodes != null)
            {
                foreach (var item in qrcodenodes)
                {
                    try
                    {
                        var qrcode = HtmlConverterImagePDF.RenderQRCode(item.InnerText, Convert.ToInt32(item.Attributes["size"]?.Value));
                        var imagenode = HtmlNode.CreateNode($"<img src=\"{qrcode}\">");

                        var parent = item.ParentNode;
                        parent.ReplaceChild(imagenode, item);
                    }
                    catch (Exception ex)
                    {
                        var errornode = HtmlNode.CreateNode($"<p style=\"color:red\">{ex.Message}</p>");
                        var parent = item.ParentNode;
                        parent.ReplaceChild(errornode, item);
                    }

                }
            }
            

            //Render barcodes
            var barcodenodes = htmlDocument.DocumentNode.SelectNodes("//barcode");
            if (barcodenodes != null)
            {
                foreach (var item in barcodenodes)
                {
                    try
                    {
                        int barcodewidth = Convert.ToInt32(item.Attributes["width"]?.Value);
                        int barcodeheight = Convert.ToInt32(item.Attributes["height"]?.Value);
                        bool includeLabel = Convert.ToBoolean(item.Attributes["includelabel"]?.Value);
                        int fontsize = Convert.ToInt32(item.Attributes["fontsize"]?.Value);
                        string fontfamily = Convert.ToString(item.Attributes["fontfamily"]?.Value);
                        string codetype = Convert.ToString(item.Attributes["codetype"]?.Value);
                        var barcode = HtmlConverterImagePDF.RenderBarcode(item.InnerText, barcodewidth, barcodeheight, includeLabel, fontsize, fontfamily, codetype);
                        var imagenode = HtmlNode.CreateNode($"<img src=\"{barcode}\">");
                        var parent = item.ParentNode;
                        parent.ReplaceChild(imagenode, item);
                    }
                    catch (Exception ex)
                    {
                        var errornode = HtmlNode.CreateNode($"<p style=\"color:red\">{ex.Message}</p>");
                        var parent = item.ParentNode;
                        parent.ReplaceChild(errornode, item);
                    }
                }
            }
            
            html = htmlDocument.DocumentNode.OuterHtml;

            string imageBase64String = RenderImageAsBase64(html, heightpx, widthpx);

            return imageBase64String;
        }

        public static string RenderLabelPDF(string html, int heightmm, int widthmm, Dictionary<string, string> fields, out string onlyBaseString)
        {
            if (fields != null)
            {
                html = ReplaceVariabels(html, fields);
            }

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //Render QRcodes
            var qrcodenodes = htmlDocument.DocumentNode.SelectNodes("//qrcode");
            if (qrcodenodes != null)
            {
                foreach (var item in qrcodenodes)
                {
                    try
                    {
                        var qrcode = HtmlConverterImagePDF.RenderQRCode(item.InnerText, Convert.ToInt32(item.Attributes["size"]?.Value));
                        var imagenode = HtmlNode.CreateNode($"<img src=\"{qrcode}\">");

                        var parent = item.ParentNode;
                        parent.ReplaceChild(imagenode, item);
                    }
                    catch (Exception ex)
                    {
                        var errornode = HtmlNode.CreateNode($"<p style=\"color:red\">{ex.Message}</p>");
                        var parent = item.ParentNode;
                        parent.ReplaceChild(errornode, item);
                    }

                }
            }


            //Render barcodes
            var barcodenodes = htmlDocument.DocumentNode.SelectNodes("//barcode");
            if (barcodenodes != null)
            {
                foreach (var item in barcodenodes)
                {
                    try
                    {
                        int barcodewidth = Convert.ToInt32(item.Attributes["width"]?.Value);
                        int barcodeheight = Convert.ToInt32(item.Attributes["height"]?.Value);
                        bool includeLabel = Convert.ToBoolean(item.Attributes["includelabel"]?.Value);
                        int fontsize = Convert.ToInt32(item.Attributes["fontsize"]?.Value);
                        string fontfamily = Convert.ToString(item.Attributes["fontfamily"]?.Value);
                        string codetype = Convert.ToString(item.Attributes["codetype"]?.Value);
                        var barcode = HtmlConverterImagePDF.RenderBarcode(item.InnerText, barcodewidth, barcodeheight, includeLabel, fontsize, fontfamily, codetype);
                        var imagenode = HtmlNode.CreateNode($"<img src=\"{barcode}\">");
                        var parent = item.ParentNode;
                        parent.ReplaceChild(imagenode, item);
                    }
                    catch (Exception ex)
                    {
                        var errornode = HtmlNode.CreateNode($"<p style=\"color:red\">{ex.Message}</p>");
                        var parent = item.ParentNode;
                        parent.ReplaceChild(errornode, item);
                    }
                }
            }

            html = htmlDocument.DocumentNode.OuterHtml;

            string pdfBase64String = RenderPDFAsBase64(html, heightmm, widthmm, out onlyBaseString);

            return pdfBase64String;
        }
    }
}
