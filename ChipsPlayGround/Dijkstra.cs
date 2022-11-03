using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace Coding
{
    public class Dijkstra
    {
        //public class Node
        //{
        //    public int Id { get; set; }
        //    public int Weight { get; set; }
        //    public Node PreviousNode { get; set; }
        //}

        public static class PriorityQueue
        {
            public static void Run(int[,] graph, int start, int end)
            {
                var numOfVertices = graph.GetLength(start);
                var distances = new int[numOfVertices]; // distances from start node
                var previousNodes = new int[numOfVertices]; // previous node for back tracking

                for (int i = 0; i < numOfVertices; i++)
                {
                    distances[i] = int.MaxValue;
                    previousNodes[i] = -1;
                }
                distances[start] = 0;

                var visisted = new bool[numOfVertices];
                var queue = new PriorityQueue<int, int>();
                queue.Enqueue(start, distances[start]);

                while (queue.Count > 0)
                {
                    var currentNode = queue.Dequeue();

                    for (var i = 0; i < numOfVertices; i++)
                    {
                        if (visisted[i]) continue;

                        if (graph[currentNode, i] != 0
                            && distances[currentNode] + graph[currentNode, i] < distances[i])
                        {
                            distances[i] = distances[currentNode] + graph[currentNode, i];
                            queue.Enqueue(i, distances[i]);
                            previousNodes[i] = currentNode;
                        }
                    }

                    visisted[currentNode] = true;

                    if (currentNode == end)
                    {
                        break;
                    }
                }

                var path = new List<int>();
                path.Add(end);
                for (var i = end; previousNodes[i] != -1; i = previousNodes[i])
                {
                    path.Add(previousNodes[i]);
                }
            }
        }
    }
}
