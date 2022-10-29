using System.Collections.Generic;

namespace Coding
{
    public class RunningMedian
    {
        private class ReverseIntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return x > y ? -1 : 1;
            }
        }

        public static class Solution
        {
            public static List<double> Run()
            {
                var input = new int[] { 12, 4, 5, 3, 8, 7 };
                List<double> output = new List<double>();

                PriorityQueue<int, int> minQueue = new PriorityQueue<int, int>();
                PriorityQueue<int, int> maxQueue = new PriorityQueue<int, int>(new ReverseIntComparer());

                for (int i = 0; i < input.Length; i++)
                {
                    var tempMedian = maxQueue.EnqueueDequeue(input[i], input[i]);

                    minQueue.Enqueue(tempMedian, tempMedian);

                    if (minQueue.Count > maxQueue.Count)
                    {
                        tempMedian = minQueue.Dequeue();
                        maxQueue.Enqueue(tempMedian, tempMedian);
                    }

                    if (minQueue.Count != maxQueue.Count)
                    {
                        output.Add(maxQueue.Peek());
                    }
                    else
                    {
                        output.Add((double)(minQueue.Peek() + maxQueue.Peek()) / 2);
                    }
                }

                return output;
            }
        }
    }
}
