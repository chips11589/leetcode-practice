using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding
{
    public class BFSGraph
    {
        public class Vertex
        {
            public Vertex(int id)
            {
                Id = id;
            }

            public int Id { get; }

            public int Weight { get; set; }
        }

        public class Graph<T>
        {
            public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
            {
                NodeCount = vertices.Count();

                foreach (var vertex in vertices)
                {
                    AdjacencyList[vertex] = new HashSet<T>();
                }

                foreach (var edge in edges)
                {
                    AdjacencyList[edge.Item1].Add(edge.Item2);
                    AdjacencyList[edge.Item2].Add(edge.Item1);
                }
            }

            public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

            public int NodeCount { get; }
        }

        public static class Solution
        {
            public static List<int> Run(Graph<Vertex> graph, Vertex start)
            {
                var queue = new Queue<Vertex>();
                var visited = new HashSet<Vertex>();
                var constantWeight = 6;
                var index = 0;
                var distances = new List<int>();
                for (int i = 0; i < graph.NodeCount - 1; i++)
                {
                    distances.Add(-1);
                }

                queue.Enqueue(start);

                while (queue.Any())
                {
                    var vertex = queue.Dequeue();

                    if (visited.Contains(vertex)) continue;

                    visited.Add(vertex);

                    foreach (var neighbour in graph.AdjacencyList[vertex])
                    {
                        if (visited.Contains(neighbour)) continue;

                        neighbour.Weight = vertex.Weight + constantWeight;
                        queue.Enqueue(neighbour);
                        distances[index] = neighbour.Weight;
                        index++;
                    }
                }

                return distances;
            }
        }
    }
}
