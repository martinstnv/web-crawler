using System.Text.RegularExpressions;

namespace WebCrawler.BusinessLogic
{
    public static class Filter
    {
       
        public static void filter(string data)
        {
            // Todo: Call procedurally each needed method needed for filtering.
        }

        private static string extractTitle(string data)
        {
            var regex = new Regex(@"<title>(.*?)</title>");

            var match = regex.Match(data);

            return match.Groups[1].Value;
        }

        public static string removePrepositionWords(string data)
        {
            string prepositionSequence = "aboard abou above across after against " +
                                         "along amid among anti around as at before " +
                                         "behind below beneath beside besides between " +
                                         "beyond but by concerning considering despite " +
                                         "down during except excepting excluding following " +
                                         "fo from in inside into like minus near of off on " +
                                         "onto opposite outside over past per plus regarding " +
                                         "round save since than through to toward towards unde " +
                                         "underneath unlike until up upon via with within without";

            string[] prepositionWords = prepositionSequence.Split(' ');
            foreach(string prepositionWord in prepositionWords)
            {
                data = data.Replace(prepositionWord, "");
            }
            return data;
        }
    }
}
