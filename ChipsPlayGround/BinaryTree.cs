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

        public static void Build(List<List<int>> indexes)
        {
            Node root = new(1);
            var queue = new Queue<Node>();
            queue.Enqueue(root);

            var indexCount = 0;

            while (queue.Count > 0 && indexCount < indexes.Count)
            {
                var node = queue.Dequeue();

                var line = indexes[indexCount];

                if (line[0] != -1)
                {
                    node.Left = new Node(line[0]);
                    queue.Enqueue(node.Left);
                }

                if (line[1] != -1)
                {
                    node.Right = new Node(line[1]);
                    queue.Enqueue(node.Right);
                }
                indexCount++;
            }
        }

        public static void Traverse(Node start)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(start);
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
