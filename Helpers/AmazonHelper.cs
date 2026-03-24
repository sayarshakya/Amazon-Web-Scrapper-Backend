using System.Text.RegularExpressions;

namespace WebScrapper.Helpers
{
    public static class AmazonHelper
    {
        public static string ExtractAsin(string url)
        {
            var match = Regex.Match(url, @"/dp/([A-Z0-9]{10})");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }
    }
}
