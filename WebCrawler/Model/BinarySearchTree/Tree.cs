using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WebCrawler.BinarySearchTree
{
    public class Tree
    {
        public Node root;
        public Tree()
        {
            root = null;
        }
        public void insert(string data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                TreeViewItem sub = new TreeViewItem();
                Node current = root;
                Node parent = null;
                while (current != null)
                {
                    parent = current;
                    if (string.Compare(data, current.data, StringComparison.CurrentCulture) < 0)
                    {
                        current = current.left;
                        if (current == null)
                        {
                            parent.left = newItem;
                        }
                    }
                    else
                    {
                        current = current.right;
                        if (current == null)
                        {
                            parent.right = newItem;
                        }
                    }
                }
            }
        }

        public HashSet<string> InOrderTraversal(HashSet<string> list)
        {
            if (root != null)
                list = root.InOrderTraversal(list);
            return list;
        }

        public int Height()
        {
            if (root == null)
            { return 0; }

            return root.Height();
        }

        public bool IsBalanced()
        {
            if (root == null)
            {
                return true;
            }

            return root.IsBalanced();
        }
    }
}
