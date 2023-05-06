using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WebCrawler.BinarySearchTree;
using WebCrawler.BusinessLogic;

namespace WebCrawler
{
    public partial class MainWindow : Window
    {
        private Tree tree;
        private HashSet<string> temporaryUrls = new HashSet<string>();
        private HashSet<string> formattedUrls = new HashSet<string>();

        public MainWindow()
        {
            InitializeComponent();
            ErrorLog.Document.Blocks.Clear();
        }

        private void startCrawling(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            treeView.Items.Clear();
            temporaryUrls.Clear();
            formattedUrls.Clear();
            crawlerTime.Clear();
            tree = new Tree();
            
            tree.insert(baseURL.Text);

            // Generate first level site map.
            SitemapGenerator.generate(baseURL.Text, baseURL.Text, formattedUrls, ErrorLog);

            // Create a hashset of temporary URLs so that the original would not modify itself while itrerating.
            foreach (string url in formattedUrls)
            {
                temporaryUrls.Add(url);
            }

            // Generate second level site map.
            foreach (string url in temporaryUrls)
            {
                SitemapGenerator.generate(baseURL.Text, url, formattedUrls, ErrorLog);
            }

            // Add all unique website address paths to tree.
            foreach (string url in formattedUrls)
            {
                tree.insert(url);
            }

            // Visualize the binary tree into the tree view.
            PrintTree(tree);
            
            // Number of nodes details
            numberOfNodes.Text = (formattedUrls.Count() + 1).ToString();

            // Tree height details
            treeHeight.Text = tree.Height().ToString();

            // Tree type details (Balanced / Unbalanced)
            if (tree.IsBalanced())
            { treeType.Text = "Balanced"; }
            else
            { treeType.Text = "Unbalanced"; }
            

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            crawlerTime.Text = ts.ToString();
        }

        private void searchForSequence(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            dataGrid.Items.Clear();
            scannerTime.Clear();

            try
            {
                Scanner.scanSequence(searchKeyword.Text, tree, dataGrid, ErrorLog, stopWatch);

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                scannerTime.Text = ts.ToString();
            }
            catch (NullReferenceException nullReferenceException)
            {
                MessageBox.Show("You need to generate site map first!");
                stopWatch.Stop();
            }
            
        }

        private void searchForOccurence(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            dataGrid.Items.Clear();
            scannerTime.Clear();

            try
            {
                Scanner.scanOccurence(searchKeyword.Text, tree, dataGrid, ErrorLog, stopWatch);

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                scannerTime.Text = ts.ToString();
            }
            catch (NullReferenceException nullReferenceException)
            {
                MessageBox.Show("You need to generate site map first!");
                stopWatch.Stop();
            }

        }

        void printTreeNode(Node node, TreeViewItem parent)
        {
            TreeViewItem child = new TreeViewItem();
            child.IsExpanded = true;
            child.Header = node.data;
            parent.Items.Add(child);

            if (node.left != null)
            {
                printTreeNode(node.left, child);
            }
            if (node.right != null)
            {
                printTreeNode(node.right, child);
            }
        }

        void PrintTree(Tree tree)
        {
            TreeViewItem ParentItem = new TreeViewItem();
            ParentItem.IsExpanded = true;
            ParentItem.Header =tree.root.data;

            if (tree.root.left != null)
            {
                printTreeNode(tree.root.left, ParentItem);
            }
            if (tree.root.right != null)
            {
                printTreeNode(tree.root.right, ParentItem);
            }

            treeView.Items.Add(ParentItem);
        }
    }
}
