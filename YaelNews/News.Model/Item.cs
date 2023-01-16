using System.ComponentModel.DataAnnotations;

namespace News.Model
{
    public class Item
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
        public string? Image { get; set; }
        [Required]
        public Guid SourceID { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public string Writer { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public Guid? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? SourceName { get; set; }
        public string? Link { get; set; }
    }
}
