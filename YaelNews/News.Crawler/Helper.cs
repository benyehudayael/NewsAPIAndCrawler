namespace News.Crawler
{
    public class Helper
    {
        public async static Task<string> GetWebContent(string url)
        {           
            using var wc = new HttpClient();
            var content = await wc.GetStringAsync(url);
            return content;
        }       
    }
}
