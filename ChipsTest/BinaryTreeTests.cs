using NUnit.Framework;
using static Coding.BinaryTree;

namespace CodingTest
{
    [TestFixture]
    public class BinaryTreeTests
    {
        [Test]
        public void Run_ReturnLevelOrder()
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

            Solution.Run(node);
        }
    }
}
