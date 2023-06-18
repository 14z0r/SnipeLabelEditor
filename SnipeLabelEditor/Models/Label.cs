using System.ComponentModel.DataAnnotations;

namespace SnipeLabelEditor.Models
{
    public class Label
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string HTML { get; set; } = string.Empty;

        public string ImageBaseString { get; set; } = string.Empty;
    }
}
