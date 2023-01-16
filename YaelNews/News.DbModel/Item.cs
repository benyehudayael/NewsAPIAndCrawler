using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.DbModel
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        [MaxLength(150)]
        public string? Image { get; set; }
        public Guid SourceId { get; set; }
        public DateTime CreatedOn { get; set; }

        [MaxLength(50)] 
        public string Writer { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public Guid? SubjectId { get; set; }
        [MaxLength(250)]
        public string? Link { get; set; }

        [ForeignKey("SourceId")]
        public virtual Source Source { get; set; } = null!;

        [ForeignKey("SubjectId")]
        public virtual Subject? Subject { get; set; }
      

    }
}