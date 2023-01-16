namespace News.DbModel
{
    public partial class Source
    {
        public Source()
        {
            Items = new HashSet<Item>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
        public string? IconUrl { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}