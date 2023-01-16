using Microsoft.EntityFrameworkCore;
using News.Contracts;
using News.DAL;
using News.DbModel;
using System.Data;


namespace News.Services
{
    public class DataService : IDataService
    {
        private readonly NewsContext NewsContext;

        public DataService(NewsContext NewsContext)
        {
            this.NewsContext = NewsContext;
        }

        public void AddNewItem(Item item) 
        {
            NewsContext.Items.Add(item);
            NewsContext.SaveChanges();
        }

        public async void UpdateItem(Item item)
        {

        }

        public void RemoveItem()
        {
           
        }

        public async Task<List<Item>> GetItems(int pageSize, int pageIndex, Guid? sid, string? freeText )
        {

            try
            {
                return await NewsContext.Items
                    .Include(x => x.Subject)
                    .Include(x => x.Source)
                    .Where(x => (!sid.HasValue || x.SubjectId == sid.Value) &&
                        (freeText == null ||
                        x.Title.ToLower().Contains(freeText.ToLower()) 
                        //|| x.Content.ToLowerInvariant().Contains(freeText.ToLowerInvariant())
                        ))
                    .OrderByDescending(x => x.CreatedOn)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch(Exception e)
            {
                return new List<Item>();
            }
        }

        public async Task<Item> GetItem(Guid id)
        {
            var item = await NewsContext.Items.FirstOrDefaultAsync();
            return item!;
        }

        public async Task<List<Source>> GetSources()
        {
            try
            {
                return await NewsContext.Sources.ToListAsync();
            }
            catch (Exception e)
            {
                return new List<Source>();
            }
        }

        public async Task<List<Subject>> GetSubjects()
        {
            try
            {
                return await NewsContext.Subjects.ToListAsync();
            }
            catch (Exception e)
            {
                return new List<Subject>();
            }
        }
        public async Task<Source> GetSource(Guid id)
        {
            var Source = await NewsContext.Sources.FirstOrDefaultAsync(x => x.Id == id);
            return Source!;
        }

        public void AddNewSource(Source source)
        {
            NewsContext.Sources.Add(source);
            NewsContext.SaveChanges();
        }

        public void RemoveSource()
        {
            throw new NotImplementedException();
        }

        public void UpdateSource()
        {
            throw new NotImplementedException();
        }

        public Task<Subject> GetSubject(Guid id)
        {
            throw new NotImplementedException();
        }

        public void AddNewSubject(Subject subject)
        {
            NewsContext.Subjects.Add(subject);
            NewsContext.SaveChanges();
        }

        public void UpdateSubject()
        {
            throw new NotImplementedException();
        }

        public void RemoveSubject()
        {
            throw new NotImplementedException();
        }

       
    }
}