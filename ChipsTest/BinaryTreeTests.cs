using System.Collections.Generic;
using Xunit;
using static Coding.BinaryTree;

namespace CodingTest
{
    public class BinaryTreeTests
    {
        [Fact]
        public void Build_ReturnRootNode()
        {
            var input = new List<List<int>>
            {
                new List<int> { 2, 3 },
                new List<int> { 4, -1 },
                new List<int> { 5, -1 },
                new List<int> { 6, -1 },
                new List<int> { 7, 8 },
                new List<int> { -1, 9 },
                new List<int> { -1, -1 },
                new List<int> { 10, 11 },
                new List<int> { -1, -1 },
                new List<int> { -1, -1 },
                new List<int> { -1, -1 }
            };

            Build(input);
        }

        [Fact]
        public void Traverse_ReturnLevelOrder()
        {
            var node = new Node(1)
            {
                Right = new Node(2)
                {
                    Right = new Node(5)
                    {
                        Left = new Node(3)
                        {
                            Right = new Node(4)
                        },
                        Right = new Node(6)
                    }
                }
            };

            Traverse(node);
        }
    }
}
