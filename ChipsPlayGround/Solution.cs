using Coding;
using System;
using System.Collections.Generic;
using System.Linq;

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
            // 2, 1, 2, 0, 1
            var buyPrice = 2000; // 2,1,0
            var profit = 0; // 1,
            var prev = 0; // 2,1,2,0

            for (int i = 0; i < prices.Length; i++)
            {
                if (prev > prices[i] && prev > buyPrice) // price drop
                {
                    profit += (prev - buyPrice); // sell prev stock
                    buyPrice = prices[i]; // buy new stock
                }
                else if (i == prices.Length - 1 && prices[i] > buyPrice)
                {
                    profit += (prices[i] - buyPrice);
                }

                if (buyPrice > prices[i])
                {
                    buyPrice = prices[i];
                }

                prev = prices[i];
            }

            return profit;

            //const int MAX_VALUE = 10001;
            //int buyPrice = MAX_VALUE;
            //int profit = 0;
            //int sellPrice = 0;
            //int current = MAX_VALUE;

            //for (int i = 0; i < prices.Length; i++)
            //{
            //    if (i == prices.Length - 1 && sellPrice <= prices[i])
            //    {
            //        current = MAX_VALUE;
            //        sellPrice = prices[i];
            //    }

            //    if (current > prices[i])
            //    {
            //        if (sellPrice > buyPrice)
            //        {
            //            profit += (sellPrice - buyPrice);
            //            sellPrice = 0;
            //        }
            //        buyPrice = prices[i];
            //    }
            //    else if (buyPrice < prices[i])
            //    {
            //        sellPrice = prices[i];
            //    }
            //    current = prices[i];
            //}

            //return profit;
        }

        public static int[] Intersect(int[] nums1, int[] nums2)
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

        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            //var list = nums1.ToList();
            //list.AddRange(nums2);

            //list.Sort();
            //return list.Any()
            //    ? list.Count % 2 != 0
            //        ? list.ElementAt(list.Count / 2)
            //        : (double)(list.ElementAt(list.Count / 2 - 1) + list.ElementAt(list.Count / 2)) / 2
            //    : 0;

            //[DataRow(new int[] { 1, 3 }, new int[] { 2 }, 2)]
            //[DataRow(new int[] { 1, 2 }, new int[] { 3, 4 }, 2.5)]

            double findMedian(int[] a, int[] b)
            {
                int minIndex = 0;
                int maxIndex = a.Length;
                int i = 0; // num of elements taken from 1st array
                int j = 0; // num of elements taken from 2nd array
                int median = 0;

                while (minIndex <= maxIndex)
                {
                    i = (minIndex + maxIndex) / 2;
                    j = (a.Length + b.Length + 1) / 2 - i;

                    if (j < b.Length && i > 0 && a[i - 1] > b[j])
                    {
                        maxIndex = i - 1;
                    }
                    else if (j > 0 && i < a.Length && b[j - 1] > a[i])
                    {
                        minIndex = i + 1;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            median = b[j - 1];
                        }
                        else if (j == 0)
                        {
                            median = a[i - 1];
                        }
                        else
                        {
                            median = Math.Max(a[i - 1], b[j - 1]);
                        }
                        break;
                    }
                }

                // calculating the median.
                // If number of elements is odd
                // there is one middle element.
                if ((a.Length + b.Length) % 2 == 1)
                    return median;

                // Elements from a[] in the second
                // half is an empty set.
                if (i == a.Length)
                    return (median + b[j]) / 2.0;

                // Elements from b[] in the second
                // half is an empty set.
                if (j == b.Length)
                    return (median + a[i]) / 2.0;

                return (median + Math.Min(a[i], b[j])) / 2.0;
            };

            if (nums1.Length < nums2.Length)
            {
                return findMedian(nums1, nums2);
            }
            else
            {
                return findMedian(nums2, nums1);
            }
        }

        public static int CountkSpikes(List<int> prices, int k)
        {
            int count = 0;
            int totalPrices = prices.Count;
            int minSpike = 2000;
            int lastRightSmallerIndex = -1;

            for (int i = 0; i < totalPrices; i++)
            {
                bool foundK = false;
                int leftK = 0;
                int rightK = 0;
                for (int j = 1; j < totalPrices; j++)
                {
                    if (prices[i] >= minSpike)
                    {
                        leftK = k;
                    }
                    else if (i - j >= 0 && prices[i - j] < prices[i])
                    {
                        leftK++;
                    }

                    if (i + j < lastRightSmallerIndex)
                    {
                        rightK = k;
                    }
                    else if (i + j < totalPrices && prices[i + j] < prices[i])
                    {
                        if (j > lastRightSmallerIndex)
                        {
                            lastRightSmallerIndex = j;
                        }
                        rightK++;
                    }

                    if (leftK >= k && rightK >= k)
                    {
                        foundK = true;
                        if (minSpike > prices[i])
                        {
                            minSpike = prices[i];
                        }
                        break;
                    }
                }
                if (foundK)
                {
                    count++;
                }
            }

            return count;
        }

        public static long FindMaxProducts(List<int> products)
        {
            List<long> sum = new List<long>() { 0 };
            int sumIndex = 0;

            for (int i = 0; i < products.Count; i++)
            {
                if (i > 0 && products[i] <= products[i - 1])
                {
                    var maxToTake = products[i];
                    var newSum = 0;
                    for (int j = i; j >= 0; j--)
                    {
                        newSum += Math.Min(products[j], maxToTake);
                        maxToTake--;
                        if (products[j] < maxToTake)
                        {
                            maxToTake = products[j] - 1;
                        }
                    }
                    sum.Add(newSum);
                    sumIndex++;
                }
                else
                {
                    sum[sumIndex] += products[i];
                }
            }

            return sum.Max();
        }

        public static int LongestPalindrome(string[] words)
        {
            // ["lc","cl","gg"]
            // ["cc","ll","xx"]
            // ["cl"]
            // ["cc"]
            // ["dd","aa","bb","dd","aa","dd","bb","dd","aa","cc","bb","cc","dd","cc"]
            // ["nn","nn","hg","gn","nn","hh","gh","nn","nh","nh"]
            // ["qo","fo","fq","qf","fo","ff","qq","qf","of","of","oo","of","of","qf","qf","of"]

            Dictionary<string, int> palindromeWordCounts = new Dictionary<string, int>();
            int longestDiffLetterLength = 0;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i][0] == words[i][1])
                {
                    if (!palindromeWordCounts.ContainsKey(words[i]))
                    {
                        palindromeWordCounts.Add(words[i], -2);
                    }
                    else
                    {
                        palindromeWordCounts[words[i]] -= 2; // gg, gg
                    }
                }
                else
                {
                    var reversedString = new string(words[i].Reverse().ToArray());
                    if (palindromeWordCounts.ContainsKey(reversedString)) // lc, cl, cl, lc
                    {
                        longestDiffLetterLength += 4;
                        palindromeWordCounts[reversedString]--;
                        if (palindromeWordCounts[reversedString] == 0)
                        {
                            palindromeWordCounts.Remove(reversedString);
                        }
                    }
                    else if (!palindromeWordCounts.ContainsKey(words[i]))
                    {
                        palindromeWordCounts.Add(words[i], 1);
                    }
                    else
                    {
                        palindromeWordCounts[words[i]]++;
                    }
                }
            }

            int longestSameLetterLength = 0;
            bool oddLengthFound = false;
            foreach (var key in palindromeWordCounts.Keys)
            {
                var length = palindromeWordCounts[key];
                if (length < 0)
                {
                    longestSameLetterLength += (length - length % 4);

                    if (length % 4 != 0)
                    {
                        oddLengthFound = true;
                    }
                }
            }

            if (oddLengthFound) longestSameLetterLength -= 2;

            if (longestSameLetterLength < 0) longestDiffLetterLength -= longestSameLetterLength;

            return longestDiffLetterLength;
        }

        public static int FindLargestAmountToRob(int[] input)
        {
            // 1, 20, 10, 1, 12, 40, 10, 8
            // 1, 20, 10, 1 - m0 1, m1 20, m2 11, m3 21
            // 1, 20, 10, 1, 12 - m1 20, m2 11, m3 21, m0 32
            // 1, 20, 10, 1, 12, 40 - m2 11, m3 21, m0 32, m1 61
            // 1, 20, 10, 1, 12, 40, 10 - m3 21, m0 32, m1 61, m2 42
            // 1, 20, 10, 1, 12, 40, 10, 8 - m0 32, m1 61, m2 42, m3 69

            // 3, 2, 3, 4, 12, 4, 4, 20
            // 3, 2, 3, 4 - m0 3, m1 2, m2 6, m3 7
            // 3, 2, 3, 4, 12 - m1 2, m2 6, m3 7, m0 18
            // 3, 2, 3, 4, 12, 4 - m2 6, m3 7, m0 18, m1 11
            // 3, 2, 3, 4, 12, 4, 4 - m3 7, m0 18, m1 11, m2 22
            // 3, 2, 3, 4, 12, 4, 4, 20 - m0 18, m1 11, m2 22, m3 38

            // 3, 12, 3, 4, 12, 4, 4, 20
            // 3, 12, 3, 4 - m0 3, m1 12, m2 6, m3 16
            // 3, 12, 3, 4, 12 - m1 12, m2 6, m3 16, m0 24
            // 3, 12, 3, 4, 12, 4 - m2 6, m3 16, m0 24, m1 20
            // 3, 12, 3, 4, 12, 4, 4 - m3 16, m0 24, m1 20, m2 28
            // 3, 12, 3, 4, 12, 4, 4, 20 - m0 24, m1 20, m2 28, m3 44

            int[] maxes = new int[] { -1, -1, -1, -1 };
            for (var i = 0; i < input.Length; i++)
            {
                // m0 - m2, m1
                // m1 - m3, m2
                // m2 - m0, m3
                // m3 - m1, m0
                maxes[i % 4] = Math.Max(input[i], Math.Max(input[i] + maxes[(i + 1) % 4], input[i] + maxes[(i + 2) % 4]));
            }

            return maxes.Max();
        }

        public static int FindLargestAmountToRob2(int[] nums)
        {
            // 1, 20, 10, 1, 12, 40, 10, 8
            // 1, 20, 11, 21, 32, 61, 42, 69

            // 3, 2, 3, 4, 12, 4, 4, 20

            // 3, 12, 3, 4, 12, 4, 4, 20

            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Max(nums[0], nums[1]);
            if (nums.Length == 3) return Math.Max(nums[1], nums[0] + nums[2]);

            nums[2] = nums[0] + nums[2];

            for (int i = 3; i < nums.Length; i++)
            {
                nums[i] = Math.Max(nums[i] + nums[i - 2], nums[i] + nums[i - 3]);
            }
            return Math.Max(nums[nums.Length - 1], nums[nums.Length - 2]);
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode current = l1;

            while (current != null)
            {
                int sum;
                if (l2 == null) sum = current.val;
                else sum = current.val + l2.val;

                current.val = sum % 10;

                if (sum >= 10)
                {
                    if (current.next != null)
                        current.next.val++;
                    else if (l2?.next != null)
                        l2.next.val++;
                    else current.next = new ListNode(1);
                }

                if (current.next == null && l2 != null)
                {
                    current.next = l2.next;
                    l2 = null;
                }
                current = current.next;

                l2 = l2?.next;
            }
            return l1;
        }

        public static void FindMinimumBribes(int[] input)
        {
            // 1 2 3 4 5
            // 1 2 3 5 4
            // 1 2 5 3 4

            // 2 1 5 3 4

            // 2 4 1 3 5

            // 2 1 4 3 5
            // 2 1 3 4 5
            // 1 2 3 4 5

            var q = new List<int>(input);
            var minBribe = 0;

            var j = 0;
            while (j != -1)
            {
                var i = j;
                j = -1;
                var counter = 0;

                for (int k = i; k < q.Count - 1;  k++)
                {
                    if (j == -1 && q[k] != k + 1)
                    {
                        j = k;
                    }

                    if (q[k + 1] < q[k])
                    {
                        (q[k], q[k + 1]) = (q[k + 1], q[k]);
                        minBribe++;
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                    }

                    if (counter > 2)
                    {
                        Console.WriteLine("Too chaotic");
                        return;
                    }
                }
            }

            Console.WriteLine(minBribe);
        }
    }
}
