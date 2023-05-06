
namespace WebCrawler.Model
{
    public class DataItem
    {
        public string keyword { get; set; }
        public string docURL { get; set; }
        public string position { get; set; }

        public DataItem() { }

        public DataItem(string FirstName, string LastName, string position)
        {
            this.keyword = FirstName;
            this.docURL = LastName;
            this.position = position;
        }

    }
}
