using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Coding.BFSGraph;

namespace CodingTest
{
    [TestFixture]
    public class BFSGraphTests
    {
        [Test]
        public void Run_ReturnShortestReach()
        {
            var vertices = new List<Vertex>
            {
                new Vertex(1), new Vertex(2), new Vertex(3), new Vertex(4), new Vertex(5)
            };

            Solution.Run(new Graph<Vertex>(vertices, new List<Tuple<Vertex, Vertex>>
            {
                new Tuple<Vertex, Vertex>(vertices[0], vertices[1]),
                new Tuple<Vertex, Vertex>(vertices[0], vertices[2]),
                new Tuple<Vertex, Vertex>(vertices[2], vertices[3])
            }), vertices[0]);
        }

        [Test]
        public void Run_ReturnShortestReach2()
        {
            var vertices = new List<Vertex>
            {
                new Vertex(1), new Vertex(2), new Vertex(3), new Vertex(4), new Vertex(5), new Vertex(6)
            };

            Solution.Run(new Graph<Vertex>(vertices, new List<Tuple<Vertex, Vertex>>
            {
                new Tuple<Vertex, Vertex>(vertices[0], vertices[1]),
                new Tuple<Vertex, Vertex>(vertices[0], vertices[4]),
                new Tuple<Vertex, Vertex>(vertices[1], vertices[2]),
                new Tuple<Vertex, Vertex>(vertices[2], vertices[3])
            }), vertices[0]);
        }
    }
}
