﻿using System;
using System.Collections.Generic;

namespace ChipsPlayGround
{
    public class Solution
    {
        public static void Rotate(int[] nums, int k)
        {
            //int maxRotateCount = k % nums.Length;
            //for (int i = 0; i < maxRotateCount; i++)
            //{
            //    int last = nums[nums.Length - 1];
            //    for (int j = nums.Length - 1; j > 0; j--)
            //    {
            //        nums[j] = nums[j - 1];
            //    }
            //    nums[0] = last;
            //}

            //[DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new int[] { 5, 6, 7, 1, 2, 3, 4 })]
            // k: 3 - 6 -> 2, 2 -> 5, 5 -> 1, 1 -> 4, 4 -> 0, 0 -> 3, 3 -> 6
            //[DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 2, new int[] { 6, 7, 1, 2, 3, 4, 5 })]
            // k: 2 - 6 -> 1, 1 -> 3, 3 -> 5, 5 -> 0, 0 -> 2, 2 -> 4, 4 -> 6
            //[DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 1, new int[] { 7, 1, 2, 3, 4, 5, 6 })]
            // k: 1 - 6 -> 0, 0 -> 1, 1 -> 2, 2 -> 3, 3 -> 4, 4 -> 5, 5 -> 6
            //[DataRow(new int[] { -1, -100, 3, 99 }, 2, new int[] { 3, 99, -1, -100 })]
            // k: 2 - ... 
            //[DataRow(new int[] { 1, 2, 3, 4, 5, 6 }, 2, new int[] { 5, 6, 1, 2, 3, 4 })]
            // k: 2 - 5 -> 1, 1 -> 3, 3 -> 5

            if (k * 2 == nums.Length)
            {
                int maxRotate = nums.Length / 2;
                for (int i = 0; i < maxRotate; i++)
                {
                    int indexToReplace = maxRotate + i;
                    int temp = nums[indexToReplace];
                    nums[indexToReplace] = nums[i];
                    nums[i] = temp;
                }
            }
            else
            {
                int shiftingNumberCount = k % nums.Length;

                if (shiftingNumberCount == 0)
                {
                    return;
                }
                int splitPoint = nums.Length - shiftingNumberCount - 1;
                int getNextIndexToReplace(int oldIndex)
                {
                    int newIndex;
                    if (oldIndex > splitPoint)
                    {
                        newIndex = shiftingNumberCount - (nums.Length - oldIndex);
                    }
                    else
                    {
                        newIndex = nums.Length - 1 - (splitPoint - oldIndex);
                    }
                    return newIndex;
                }

                int numberBeingMoved;
                int count = 0;

                for (int startPoint = 0; count < nums.Length - 1; startPoint++)
                {
                    numberBeingMoved = nums[startPoint];
                    int indexToReplace = getNextIndexToReplace(startPoint);

                    while (startPoint != indexToReplace)
                    {
                        int temp = nums[indexToReplace];
                        nums[indexToReplace] = numberBeingMoved;
                        numberBeingMoved = temp;

                        indexToReplace = getNextIndexToReplace(indexToReplace);
                        count++;
                    }
                    nums[indexToReplace] = numberBeingMoved;
                    count++;
                }
            }
        }

        public static int MaxProfit(int[] prices)
        {
            const int MAX_VALUE = 10001;
            int buyPrice = MAX_VALUE;
            int profit = 0;
            int sellPrice = 0;
            int current = MAX_VALUE;

            for (int i = 0; i < prices.Length; i++)
            {
                if (i == prices.Length - 1 && sellPrice <= prices[i])
                {
                    current = MAX_VALUE;
                    sellPrice = prices[i];
                }

                if (current > prices[i])
                {
                    if (sellPrice > buyPrice)
                    {
                        profit += (sellPrice - buyPrice);
                        sellPrice = 0;
                    }
                    buyPrice = prices[i];
                }
                else if (buyPrice < prices[i])
                {
                    sellPrice = prices[i];
                }
                current = prices[i];
            }

            return profit;
        }

        public int[] Intersect(int[] nums1, int[] nums2)
        {
            //Input: nums1 = [4, 9, 10, 15], nums2 = [4, 4, 8, 9, 9, 10, 12, 15]
            //Output: [4,9,15]

            List<int> result = new List<int>();
            Dictionary<int, int> distinct1 = new Dictionary<int, int>();
            Dictionary<int, int> distinct2 = new Dictionary<int, int>();

            for (int i = 0; i < nums1.Length; i++)
            {
                if (!distinct1.ContainsKey(nums1[i]))
                {
                    distinct1[nums1[i]] = 0;
                }
                else
                {
                    distinct1[nums1[i]] += 1;
                }
            }

            for (int i = 0; i < nums2.Length; i++)
            {
                if (!distinct2.ContainsKey(nums2[i]))
                {
                    distinct2[nums2[i]] = 0;
                }
                else
                {
                    distinct2[nums2[i]] += 1;
                }
            }

            foreach (var key in distinct1.Keys)
            {
                if (distinct2.ContainsKey(key))
                {
                    for (int i = 0; i < Math.Min(distinct1[key], distinct2[key]) + 1; i++)
                    {
                        result.Add(key);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
