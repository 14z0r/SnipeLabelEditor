using CoreHtmlToImage;

namespace SnipeLabelEditor
{
    public static class HTMLToImage
    {
        private static HtmlConverter _converter = new HtmlConverter();

        public static string RenderImageAsBase64(string html)
        {
            html = $"<div style=\"margin: -8px -8px -8px -8px;\"> {html} </div>";
            var bytes = _converter.FromHtmlString(html, 50, ImageFormat.Png, 100);
            return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bytes));
        }
    }
}
