using System;
using System.Collections.Generic;

namespace WebCrawler.BinarySearchTree
{
    public class Node
    {
        public string data;
        public Node left { get; set; }
        public Node right { get; set; }

        public Node(string data)
        {
            this.data = data;
        }

        public HashSet<string> InOrderTraversal(HashSet<string> list)
        {
            if (left != null)
                left.InOrderTraversal(list);
            list.Add(data);

            if (right != null)
                right.InOrderTraversal(list);

            return list;
        }

        public int Height()
        {
            if (this.left == null && this.right == null)
            {
                return 1;
            }

            int left = 0;
            int right = 0;

            if (this.left != null)
                left = this.left.Height();
            if (this.right != null)
                right = this.right.Height();

            if (left > right)
            {
                return (left + 1);
            }
            else
            {
                return (right + 1);
            }

        }

        public bool IsBalanced()
        {
            int LeftHeight = left != null ? left.Height() : 0;
            int RightHeight = right != null ? right.Height() : 0;

            int heightDifference = LeftHeight - RightHeight;

            if (Math.Abs(heightDifference) > 1)
            {
                return false;
            }
            else
            {
                return ((left != null ? left.IsBalanced() : true) && (right != null ? right.IsBalanced() : true));
            }
        }
    }
}
