using System.ComponentModel.DataAnnotations;

namespace News.DbModel
{
    public class Shape
    {
        [Key]
        public int DDD { get; set; }
        public int Size { get; set; }
        public string Name2 { get; set; }
        public int Color { get; set; }
    }
}
