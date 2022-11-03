using NUnit.Framework;
using static Coding.Dijkstra;

namespace CodingTest
{
    [TestFixture]
    public class DijkstraTests
    {
        [Test]
        public void Run_ReturnShortestPath()
        {
            int[,] graph = new int[,]
            {
                { 0, 4, 3, 0, 7, 0, 0 },
                { 4, 0, 6, 5, 0, 0, 0 },
                { 3, 6, 0, 11, 8, 0, 0 },
                { 0, 5, 11, 0, 2, 2, 10 },
                { 7, 0, 8, 2, 0, 0, 5 },
                { 0, 0, 0, 2, 0, 0, 3 },
                { 0, 0, 0, 10, 5, 3, 0 }
            };
            PriorityQueue.Run(graph, 0, 5);
        }
    }
}
