﻿using SnipeLabelEditor.Data;
using System.ComponentModel.DataAnnotations;

namespace SnipeLabelEditor.Models
{
    public class Label
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "nameRequired")]
        public string Name { get; set; } = string.Empty;

        public string HTML { get; set; } = string.Empty;

        private string pdfBaseString = "";
        public string PdfBaseString 
        { 
            get 
            {
                if (string.IsNullOrEmpty(pdfBaseString))
                {
                    pdfBaseString = HtmlConverterImagePDF.RenderLabelPDF(HTML, HeightMm, WidthMm, MarginLeft, MarginRight, MarginTop, MarginBottom, null);
                }
                return pdfBaseString;
            } 
            set => pdfBaseString = value;
        }
        public int HeightPx { get; set; }

        public int WidthPx { get; set; }

        public int HeightMm { get; set; }

        public int WidthMm { get; set; }

        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }
        public bool Favorite { get; set; } = false;
    }
}
