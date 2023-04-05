using System.Collections;

namespace Coding
{
    public class QuickSort
    {
        public static class Solution
        {
            private static void Sort(ref int[] input)
            {
                var stack = new Stack();
                int pivotIndex = 0;
                int rightIndex = input.Length - 1;

                stack.Push(pivotIndex);
                stack.Push(rightIndex);

                while (stack.Count > 0)
                {
                    pivotIndex = (int)stack.Pop();
                    int pivot = input[pivotIndex];
                    int leftIndex = pivotIndex + 1;
                    rightIndex = (int)stack.Pop();
                    int i = leftIndex;
                    int j = rightIndex;

                    while (i < j)
                    {
                        while (i <= j && input[i] < pivot)
                        {
                            i++;
                        }
                        while (i <= j && input[j] > pivot)
                        {
                            j--;
                        }

                        if (i < j)
                        {
                            (input[j], input[i]) = (input[i], input[j]);
                        }
                    }

                    if (i > pivotIndex)
                    {
                        stack.Push(pivotIndex);
                        stack.Push(i);
                    }

                    if (i < rightIndex)
                    {
                        stack.Push(i);
                        stack.Push(rightIndex);
                    }
                }
            }

            public static int[] Run(int[] input)
            {
                Sort(ref input);

                return input;
            }
        }
    }
}
