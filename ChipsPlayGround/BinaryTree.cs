using System.Collections.Generic;
using System.Linq;

namespace Coding
{
    public class BinaryTree
    {
        public class Node
        {
            public Node(int id)
            {
                Id = id;
            }

            public int Id { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public static class Solution
        {
            public static void Run(Node root)
            {
                var queue = new Queue<Node>();
                queue.Enqueue(root);
                HashSet<Node> visited = new HashSet<Node>();

                while (queue.Any())
                {
                    var node = queue.Dequeue();

                    if (node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);

                    visited.Add(node);
                }
            }
        }
    }
}
