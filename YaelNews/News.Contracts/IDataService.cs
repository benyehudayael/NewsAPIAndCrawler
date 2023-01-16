using News.DbModel;

namespace News.Contracts
{
    public interface IDataService
    {
        Task<List<Item>> GetItems(int pageSize, int pageIndex, Guid? sid, string? freeText);
        Task<Item> GetItem(Guid id);
        void AddNewItem(Item item);
        void UpdateItem(Item item);
        void RemoveItem();

        Task<List<Source>> GetSources();
        Task<List<Subject>> GetSubjects();
        Task<Source> GetSource(Guid id);
        void AddNewSource(Source x);
        void RemoveSource();
        void UpdateSource();

        Task<Subject> GetSubject(Guid id);
        void AddNewSubject(Subject subject);
        void UpdateSubject();
        void RemoveSubject();
    }
}
