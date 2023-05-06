using System.Collections.Generic;

namespace WebCrawler.BusinessLogic
{
    class Permutation
    {
        private List<string> combinations = new List<string>();

        public List<string> getList()
        {
            return combinations;
        }

        public void swap(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }

        public void permute(string[] list, int k, int m)
        {
            int i;
            string temp = "";
            if (k == m)
            {
                for (i = 0; i <= m; i++)
                {
                    temp += list[i] + " ";
                }
                combinations.Add(temp);
            }
            else
            {
                for (i = k; i <= m; i++)
                {
                    swap(ref list[k], ref list[i]);
                    permute(list, k + 1, m);
                    swap(ref list[k], ref list[i]);
                }
            }
        }
    }
}
