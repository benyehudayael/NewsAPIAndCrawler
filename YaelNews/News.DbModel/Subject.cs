
namespace News.DbModel
{
    public class Subject
    {
        public Subject()
        {
            Items = new HashSet<Item>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool ShowInMenu { get; set; }
        public bool ShowInNewItem { get; set; }
        public int Sort { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
