using Newtonsoft.Json;
using System.Xml.Linq;
using News.Model;
using News.Contracts;
using ScrapySharp.Extensions;
using HtmlAgilityPack;
using News.Mappers;


namespace News.Crawler
{
    public class CrawlerService
    {
        private readonly IDataService dataService;

        public CrawlerService(IDataService dataService)
        {
            this.dataService = dataService;
            Sources = dataService.GetSources().GetAwaiter().GetResult().Select(x => Mapper.SourceToModel(x)).ToList();
            Subjects = dataService.GetSubjects().GetAwaiter().GetResult().Select(x => Mapper.SubjectToModel(x)).ToList();
        }

        public static List<Source> Sources { get; set; } = new List<Source>();
        public static List<Subject> Subjects { get; set; } = new List<Subject>();
        public async void LoadAndPersistItems()
        {
            var itemsA = await LoadItemsFromRSSMarketsMain();
            var itemsB = await LoadItemsFromRSScnn();
            SaveSources(Sources.Where(x => x.IsNew).ToList());
            SaveSubjects(Subjects.Where(x => x.IsNew).ToList());
            SaveItems(itemsA);
            SaveItems(itemsB);
        }

        private void SaveSources(List<Source> Sources)
        {
            Sources.ForEach(x => dataService.AddNewSource(Mapper.SourceFromModel(x)));
        }

        private void SaveSubjects(List<Subject> Subjects)
        {
            Subjects.ForEach(x => dataService.AddNewSubject(Mapper.SubjectFromModel(x)));
        }

        private void SaveItems(List<Item> items)
        {
            items.ForEach(x => dataService.AddNewItem(Mapper.ItemFromModel(x)));
        }

        private async Task<List<Item>> LoadItemsFromRSSMarketsMain()
        {
            var newsItems = new List<Item>();
            var m_strFilePath = "https://feeds.a.dj.com/rss/RSSMarketsMain.xml";
            string xmlStr;
            using (var wc = new HttpClient())
            {
                xmlStr = await wc.GetStringAsync(m_strFilePath);
            }
            var doc = XDocument.Parse(xmlStr);
            string jsonText = JsonConvert.SerializeXNode(doc.Document?.Root?.Descendants().FirstOrDefault());
            dynamic json = JsonConvert.DeserializeObject(jsonText)!;
            var items = json.channel.item;
            foreach (dynamic item in items)
            {
                //var htmlContent = await Helper.GetWebContent(item.link.ToString());
                var htmlContent = new HtmlWeb().Load(item.link.ToString());
                var source = GetSource(item.link.ToString());
                var content = GetItemContent(htmlContent, source);
                var image = GetItemImage(htmlContent, source);
                var writer = GetItemWriter(htmlContent, source);
                var subject = GetSubject(JsonConvert.DeserializeObject<dynamic>(item.ToString())["dj:articletype"].ToString());
                var link = item.link.ToString();
                //var title = GetItemTitle(htmlContent, source);
                newsItems.Add(new Item()
                {
                    Title = item.title.ToString(),
                    Content = content,
                    Image = image,
                    CreatedOn = DateTime.Now,
                    SourceID = source.Id,
                    Writer = writer,
                    ID = Guid.NewGuid(),
                    SubjectId = subject.ID,
                    IsDeleted = false,
                    Link = link

                });
            }
            return newsItems;
        }
        private async Task<List<Item>> LoadItemsFromRSScnn()
        {
            var newsItems = new List<Item>();
            var m_strFilePath = "http://rss.cnn.com/rss/edition_world.rss";
            string xmlStr;
            using (var wc = new HttpClient())
            {
                xmlStr = await wc.GetStringAsync(m_strFilePath);
            }
            var doc = XDocument.Parse(xmlStr);
            string jsonText = JsonConvert.SerializeXNode(doc.Document?.Root?.Descendants().FirstOrDefault());
            dynamic json = JsonConvert.DeserializeObject(jsonText)!;
            var items = json.channel.item;
            foreach (dynamic item in items)
            {
                try
                {
                    var htmlContent = new HtmlWeb().Load(Convert.ToString(item.link));
                    var link = Convert.ToString(item.link);
                    var source = GetSource(Convert.ToString(item.link));
                    var image = GetItemImage(htmlContent, source);
                    var writer = GetItemWriter(htmlContent, source);
                    var subject = GetSubject(GetSubjectFromCnn(htmlContent));
                    string? content;
                    if (item.description != null)
                    {
                        var contentObj = Convert.ToString(item.description);
                        content = ((Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(contentObj.ToString()))?.First?.First?.ToString();
                    }
                    else
                    {
                        content = "";
                    }
                    var titleObj = Convert.ToString(item.title);
                    var title = ((Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(titleObj.ToString()))?.First?.First?.ToString();

                    //var title = GetItemTitle(htmlContent, source);
                    newsItems.Add(new Item()
                    {
                        Title = title ?? "",
                        Content = content ?? "",
                        Image = image,
                        CreatedOn = DateTime.Now,
                        SourceID = source.Id,
                        Writer = writer,
                        ID = Guid.NewGuid(),
                        SubjectId = subject.ID,
                        IsDeleted = false,
                        Link = link

                    });
                }
                catch(Exception ex) { }
            }
            return newsItems;
        }

        private string GetSubjectFromCnn(HtmlDocument? document)
        {
            var res1 = document!.DocumentNode.CssSelect(".PageHead__component a");
            return res1.Any() ? res1!.FirstOrDefault()!.InnerText : "";
        }

        private static string GetItemImage(HtmlDocument? document, Source source)
        {
            switch (source.Name)
            {
                case "www.wsj.com":
                    var res1 = document!.DocumentNode.CssSelect("main article img");
                    return res1.Any() ? res1!.FirstOrDefault()!.GetAttributeValue("src") : "";
                case "www.cnn.com":
                    var res2 = document!.DocumentNode.CssSelect("img");
                    var imageSrc = res2.Any() ? res2!.FirstOrDefault()!.GetAttributeValue("src") : "";
                    var clearSrc = imageSrc.Substring(imageSrc.LastIndexOf("/http") + 1);
                    return System.Net.WebUtility.UrlDecode(clearSrc);
                default:
                    return "";
            }
        }

        private static string GetItemContent(HtmlDocument? document, Source source)
        {
            switch (source.Name)
            {
                case "www.wsj.com":
                    var res1 = document!.DocumentNode.CssSelect("section");
                    return res1.Any() ? string.Join(" ", res1!.FirstOrDefault()!.InnerText.Split(' ').Take(10)) : "";

                case "www.cnn.com":
                    return "";
                default:
                    return "";
            }
        }

        private static string GetItemTitle(HtmlDocument? document, Source source)
        {
            switch (source.Name)
            {
                case "www.wsj.com":
                    var res1 = document!.DocumentNode.CssSelect("h1");
                    return res1.Any() ? res1!.FirstOrDefault()!.InnerText : "";
                case "NYTimes2":
                    return "";
                default:
                    return "";
            }
        }

        private static string GetItemWriter(HtmlDocument? document, Source source)
        {
            switch (source.Name)
            {
                case "www.wsj.com":
                    var res1 = document!.DocumentNode.CssSelect("span[class*=Author]");
                    return res1.Any() ? res1!.FirstOrDefault()!.InnerText : "";
                case "www.cnn.com":
                    var res2 = document!.DocumentNode.CssSelect(".Authors__writers span");
                    return res2.Any() ? res2!.FirstOrDefault()!.InnerText : "";
                default:
                    return "";
            }
            //var match = Regex.Match(itemContent, "rel=\"author\"[^<]*");            
            //var inregex = Regex.Match(match.Value, ">[^<]*");
            //return inregex != null ? inregex.Value.Replace(">", "") : "";
        }

        private static Source GetSource(string url)
        {
            var domain = new Uri(url).Authority;
            if (Sources.Any(x => x.Name == domain))
                return Sources.First(x => x.Name == domain)!;
            else
            {
                var newSource = new Source() { BaseUrl = new Uri(url).Host, Id = Guid.NewGuid(), Name = domain, IsNew = true };
                Sources.Add(newSource);
                return newSource;
            }
        }

        private static Subject GetSubject(string name)
        {
            if (Subjects.Any(x => x.Name == name))
                return Subjects.First(x => x.Name == name)!;
            else
            {
                var newSubject = new Subject() { ID = Guid.NewGuid(), Name = name, IsNew = true };
                Subjects.Add(newSubject);
                return newSubject;
            }

        }
    }
}
