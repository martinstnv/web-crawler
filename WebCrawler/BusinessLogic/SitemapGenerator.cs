using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WebCrawler.BusinessLogic
{
    public static class SitemapGenerator
    {
        public static HashSet<string> generate(string baseURL ,string urlParam, HashSet<string> formattedUrls , RichTextBox errorLog)
        {
            string html = "";
            HashSet<string> unformattedURLs = new HashSet<string>();

            WebClient webClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            try
            {
                html = webClient.DownloadString(urlParam);
            }
            catch
            {
                errorLog.Document.Blocks.Add(new Paragraph(new Run("Page not found 404. Could not find " + urlParam)));
            }

            MatchCollection matchList = Regex.Matches(html, "<a (.*)>");
            unformattedURLs = matchList.Cast<Match>().Select(match => match.Value).ToHashSet();

            foreach (string url in unformattedURLs)
            {
                var extractedUrl = Regex.Match(url, "href\\s*=\\s*\"(?<url>.*?)\"").Groups["url"].Value;

                if (extractedUrl.Contains("htm") || extractedUrl.Contains("pdf"))
                {
                    formattedUrls.Add(baseURL + "/" + extractedUrl);
                }
            }

            return formattedUrls;
        }
    }
}
