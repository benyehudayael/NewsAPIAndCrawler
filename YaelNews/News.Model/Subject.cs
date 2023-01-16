using System.ComponentModel.DataAnnotations;

namespace News.Model
{
    public class Subject
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public bool ShowInMenu { get; set; } 
        [Required]
        public bool ShowInNewItem { get; set; }
        [Required]
        public int Sort { get; set; }
        public bool IsNew { get; set; }
    }
}
