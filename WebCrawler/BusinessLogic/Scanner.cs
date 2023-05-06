using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WebCrawler.BinarySearchTree;
using WebCrawler.Model;

namespace WebCrawler.BusinessLogic
{
    public static class Scanner
    {
        public static void scanSequence(string keyword, Tree tree, DataGrid dataGrid, RichTextBox errorLog, Stopwatch stopWatch)
        {
            keyword = Filter.removePrepositionWords(keyword);

            if (keyword.Equals(""))
            {
                MessageBox.Show("You need to enter a keyword before searching!");
                stopWatch.Stop();
            }
            else if (keyword.Any(ch => !Char.IsLetterOrDigit(ch) && !Char.IsWhiteSpace(ch)))
            {
                MessageBox.Show("You cannot use any symbols!");
                stopWatch.Stop();
            }
            else
            {
                HashSet<string> inOrder = new HashSet<string>();
                inOrder = tree.InOrderTraversal(inOrder);

                Permutation permutation = new Permutation();
                string[] keywordArray = keyword.Split(' ');
                permutation.permute(keywordArray, 0, keywordArray.Length - 1);
                List<string> permutations = permutation.getList();

                foreach (string link in inOrder)
                {
                    string html = "";

                    WebClient webClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                    try
                    {
                        html = Filter.removePrepositionWords(webClient.DownloadString(link));
                    }
                    catch
                    {
                        errorLog.Document.Blocks.Add(new Paragraph(new Run("Page not found 404. Could not find " + link)));
                        stopWatch.Stop();
                    }
                    foreach(string word in permutations)
                    {
                        if (html.Contains(word))
                        {
                            
                            dataGrid.Items.Add(new DataItem(word, link, html.IndexOf(word).ToString()));
                        }
                    }
                }
            }
        }

        public static void scanOccurence(string keyword, Tree tree, DataGrid dataGrid, RichTextBox errorLog, Stopwatch stopWatch)
        {
            keyword = Filter.removePrepositionWords(keyword);

            if (keyword.Equals(""))
            {
                MessageBox.Show("You need to enter a keyword before searching!");
                stopWatch.Stop();
            }
            else if (keyword.Any(ch => !Char.IsLetterOrDigit(ch) && !Char.IsWhiteSpace(ch)))
            {
                MessageBox.Show("You cannot use any symbols!");
                stopWatch.Stop();
            }
            else
            {
                HashSet<string> inOrder = new HashSet<string>();
                inOrder = tree.InOrderTraversal(inOrder);
                List<string> listOfKeywords = keyword.Split(' ').ToList();


                foreach (string link in inOrder)
                {
                    string html = "";

                    WebClient webClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                    try
                    {
                        html = Filter.removePrepositionWords(webClient.DownloadString(link));
                    }
                    catch
                    {
                        errorLog.Document.Blocks.Add(new Paragraph(new Run("Page not found 404. Could not find " + link)));
                        stopWatch.Stop();
                    }
                    foreach(string word in listOfKeywords)
                    {
                        if (html.Contains(word) && !word.Equals(""))
                        {
                            dataGrid.Items.Add(new DataItem(word, link, html.IndexOf(word).ToString()));
                        }
                    }
                }
            }
        }
    }
}
