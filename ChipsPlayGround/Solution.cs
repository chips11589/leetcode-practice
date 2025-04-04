﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using BenchmarkDotNet.Configs;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using Microsoft.Diagnostics.Tracing.Parsers.JSDumpHeap;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;

namespace Coding;

public class Solution
{
    /// <summary>
    /// https://leetcode.com/problems/rotate-array
    /// </summary>
    public static void Rotate(int[] nums, int k)
    {
        var n = nums.Length;
        k = k % n;
        var nums2 = new int[k];

        for (int i = 0; i < k; i++)
        {
            nums2[i] = nums[n - k + i];
        }

        for (int i = n - 1 - k; i >= 0; i--)
        {
            nums[i + k] = nums[i];
        }

        for (int i = 0; i < k; i++)
        {
            nums[i] = nums2[i];
        }
    }

    public static int MaxProfit(int[] prices)
    {
        // 2, 1, 2, 0, 1
        //var buyPrice = 2000; // 2,1,0
        var profit = 0; // 1,
        //var prev = 0; // 2,1,2,0

        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] > prices[i - 1])
            {
                profit += prices[i] - prices[i - 1];
            }

            //if (prev > prices[i] && prev > buyPrice) // price drop
            //{
            //    profit += (prev - buyPrice); // sell prev stock
            //    buyPrice = prices[i]; // buy new stock
            //}
            //else if (i == prices.Length - 1 && prices[i] > buyPrice)
            //{
            //    profit += (prices[i] - buyPrice);
            //}

            //if (buyPrice > prices[i])
            //{
            //    buyPrice = prices[i];
            //}

            //prev = prices[i];
        }

        return profit;
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

        static double FindMedian(int[] a, int[] b)
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
        }
        ;

        if (nums1.Length < nums2.Length)
        {
            return FindMedian(nums1, nums2);
        }
        else
        {
            return FindMedian(nums2, nums1);
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
                longestSameLetterLength += length - length % 4;

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

    public static int FindLargestAmountToRob3(int[] nums) // brute force
    {
        // 1, 20, 10, 1, 12, 40, 10, 8

        static int Rob(int[] nums, int i)
        {
            if (i >= nums.Length)
            {
                return 0;
            }

            int robCurrent = nums[i] + Rob(nums, i + 2);
            int skipCurrent = Rob(nums, i + 1);

            return Math.Max(robCurrent, skipCurrent);
        }

        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        return Rob(nums, 0);
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

            for (int k = i; k < q.Count - 1; k++)
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

    public static string FindAbbreviation(string a, string b)
    {
        #region non dp attempt
        // daBcd
        // ABC
        //int bIndex = 0;
        //string substring = string.Empty;
        //int lastCapPos = 0;
        //for (int i = 0; i < a.Length; i++)
        //{
        //    if (char.IsUpper(a[i]) && !b.Contains(a[i]))
        //    {
        //        return "NO";
        //    }

        //    if (char.ToUpper(a[i]) == b[bIndex])
        //    {
        //        // already reached desired state and new char can be skipped
        //        if (substring.ToUpper() == b)
        //        {
        //            if (char.IsLower(a[i]))
        //            {
        //                goto afterIteration;
        //            }
        //            else if (substring.Last() == char.ToLower(a[i]))
        //            {
        //                substring = substring.Remove(substring.Length - 1);
        //                substring += a[i];
        //                goto afterIteration;
        //            }
        //        }

        //        substring += a[i];
        //        bIndex = bIndex == b.Length - 1 ? b.Length - 1 : bIndex + 1;
        //    }
        //    else if (char.IsUpper(a[i]))
        //    {
        //        if (substring.LastIndexOf(char.ToLower(a[i]), substring.Length - 1, substring.Length - lastCapPos) is var index
        //            && index > -1)
        //        {
        //            substring = substring.Remove(index);
        //            substring = substring.Insert(index, a[i].ToString());
        //            var backMove = substring.Length;
        //            bIndex = backMove >= b.Length - 1 ? b.Length - 1 : backMove;
        //        }
        //        else
        //        {
        //            for (var j = substring.Length - 1; j >= 0; j--)
        //            {
        //                if (char.ToUpper(substring[j]) != a[i])
        //                {
        //                    break;
        //                }
        //                else if (char.IsLower(substring[j]))
        //                {
        //                    substring = substring.Remove(j, 1);
        //                }
        //            }
        //            substring += a[i];
        //        }
        //    }

        //afterIteration:
        //    if (substring.Length > 0 && char.IsUpper(substring.Last()))
        //    {
        //        lastCapPos = substring.Length - 1;
        //    }
        //}

        //return substring.ToUpper() == b ? "YES" : "NO";
        #endregion

        //daBcd
        //ABC
        int rows = b.Length;
        int cols = a.Length;

        bool[,] dp = new bool[rows + 1, cols + 1];
        dp[0, 0] = true;
        //Fill the first col
        for (int row = 1; row <= rows; row++)
        {
            dp[row, 0] = false;
        }

        //Fill the first row
        for (int col = 1; col <= cols; col++)
        {
            if (char.IsLower(a[col - 1]))
                dp[0, col] = dp[0, col - 1];
            else
                dp[0, col] = false;
        }

        for (int row = 1; row <= rows; row++)
        {
            for (int col = 1; col <= cols; col++)
            {
                if (char.ToUpper(a[col - 1]) == b[row - 1])
                {
                    dp[row, col] = dp[row - 1, col - 1];

                    if (char.IsLower(a[col - 1]))
                    {
                        dp[row, col] |= dp[row, col - 1];
                    }
                }
                else if (char.IsLower(a[col - 1]))
                {
                    dp[row, col] = dp[row, col - 1];
                }
                else
                {
                    dp[row, col] = false;
                }
            }
        }
        return dp[rows, cols] ? "YES" : "NO";
    }

    // find order to deliver goods
    public static List<int> FindOrder(int cityNodes, List<int> cityFrom, List<int> cityTo, int company)
    {
        var output = new HashSet<int>();

        // building adjacent nodes
        List<PriorityQueue<int, int>> nodes = new(cityNodes + 1);
        for (int i = 0; i < cityNodes; i++)
        {
            nodes.Add(new PriorityQueue<int, int>());
        }

        for (int i = 0; i < cityFrom.Count; i++)
        {
            nodes[cityFrom[i]].Enqueue(cityTo[i], cityTo[i]);
        }

        for (int i = 0; i < cityTo.Count; i++)
        {
            nodes[cityTo[i]].Enqueue(cityFrom[i], cityFrom[i]);
        }

        // breadth first search
        Queue<int> queue = new();
        queue.Enqueue(company); // add company node
        var visited = new bool[cityNodes + 1];

        while (queue.Count > 0)
        {
            int node = queue.Dequeue(); // current node

            if (visited[node]) continue;

            visited[node] = true;

            if (node != company)
            {
                output.Add(node);
            }

            while (nodes[node].Count > 0)
            {
                var connected = nodes[node].Dequeue();

                if (visited[connected]) continue;

                queue.Enqueue(connected);
            }
        }

        return output.ToList();
    }

    public static int FindLowestPrice(List<List<string>> products, List<List<string>> discounts)
    {
        var discountLookup = new Dictionary<string, Tuple<decimal, decimal>>();
        for (int i = 0; i < discounts.Count; i++)
        {
            if (!discountLookup.ContainsKey(discounts[i][0]))
            {
                discountLookup.Add(discounts[i][0],
                    new Tuple<decimal, decimal>(decimal.Parse(discounts[i][1]), decimal.Parse(discounts[i][2])));
            }
            else
            {
                discountLookup[discounts[i][0]]
                    = new Tuple<decimal, decimal>(decimal.Parse(discounts[i][1]), decimal.Parse(discounts[i][2]));
            }
        }

        var discountedPrices = new List<int>();
        for (int i = 0; i < products.Count; i++) // loop through products
        {
            decimal minPrice = decimal.Parse(products[i][0]);
            for (int j = 1; j < products[i].Count; j++) // loop through discounts
            {
                decimal discountedPrice = decimal.Parse(products[i][0]);
                if (discountLookup.ContainsKey(products[i][j]))
                {
                    switch (discountLookup[products[i][j]].Item1) // switch discount types
                    {
                        case 0:
                            discountedPrice = discountLookup[products[i][j]].Item2;
                            break;
                        case 1:
                            discountedPrice -= discountedPrice * discountLookup[products[i][j]].Item2 / 100;
                            break;
                        case 2:
                            discountedPrice -= discountLookup[products[i][j]].Item2;
                            break;
                    }
                }
                if (minPrice > discountedPrice)
                {
                    minPrice = discountedPrice;
                }
            }

            discountedPrices.Add((int)Math.Round(minPrice));
        }

        return discountedPrices.Sum();
    }

    public static long FindOptimalCandies(int n, List<int> arr)
    {
        var dp = new long[n];

        for (int i = 0; i < n; i++)
        {
            dp[i] = 1;
        }

        for (int i = 1; i < n; i++)
        {
            if (arr[i] > arr[i - 1])
            {
                dp[i] = dp[i - 1] + 1;
            }
        }

        for (int i = n - 1; i > 0; i--)
        {
            if (arr[i] < arr[i - 1] && dp[i - 1] <= dp[i])
            {
                dp[i - 1] = dp[i] + 1;
            }
        }

        return dp.Sum();
    }

    public static int FindMaxSubsetSum(int[] arr)
    {
        // non-adjacent elements with the maximum sum
        if (arr.Length == 0) return 0;
        if (arr.Length == 1) return arr[1] > 0 ? arr[1] : 0;

        int[] dp = new int[arr.Length];
        dp[0] = arr[0] > 0 ? arr[0] : 0;
        dp[1] = Math.Max(dp[0], arr[1]);

        for (int i = 2; i < arr.Length; i++)
        {
            dp[i] = Math.Max(dp[i - 2] + arr[i], dp[i - 1]);
        }

        return dp[arr.Length - 1];
    }

    public static int FindMaxProfitIV(int k, int[] prices)
    {
        if (prices == null || prices.Length == 0)
        {
            return 0;
        }

        int n = prices.Length;
        int[,] dp = new int[k + 1, n];

        //for (int i = 1; i <= k; i++)
        //{
        //    for (int j = 1; j < n; j++)
        //    {
        //        int maxProfit = 0;

        //        for (int l = 0; l < j; l++)
        //        {
        //            maxProfit = Math.Max(maxProfit, dp[i - 1, l] + prices[j] - prices[l]);
        //        }
        //        dp[i, j] = Math.Max(dp[i, j -1], maxProfit);
        //    }
        //}

        for (int i = 1; i <= k; i++)
        {
            int maxPrevDiff = int.MinValue;

            for (int j = 1; j < n; j++)
            {
                maxPrevDiff = Math.Max(maxPrevDiff, dp[i - 1, j - 1] - prices[j - 1]);

                dp[i, j] = Math.Max(dp[i, j - 1], prices[j] + maxPrevDiff);
            }
        }

        return dp[k, n - 1];
    }

    public static int FindMinimumSwaps(int[] arr)
    {
        var minSwap = 0;
        int n = arr.Length;

        for (int i = 0; i < n; i++)
        {
            while (arr[i] != i + 1)
            {
                (arr[i], arr[arr[i] - 1]) = (arr[arr[i] - 1], arr[i]);
                minSwap++;
            }
        }

        return minSwap;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/torque-and-development/problem
    /// </summary>
    public static long BuildRoadsAndLibraries(int n, int c_lib, int c_road, List<List<int>> cities)
    {
        #region find no of cities per connected area
        //long cost = 0;
        //var nodes = new Dictionary<int, List<int>>();
        //var citiesPerConnectedArea = new List<int>();
        //var visited = new bool[n + 1];

        //// if c_road > c_lib
        //if ((long)c_road * (n - 1) > (long)c_lib * n)
        //{
        //    return (long)c_lib * n;
        //}

        //// build adjacent list
        //for (int i = 0; i < cities.Count; i++)
        //{
        //    if (!nodes.ContainsKey(cities[i][0]))
        //    {
        //        nodes.Add(cities[i][0], new List<int> { cities[i][1] });
        //    }
        //    else
        //    {
        //        nodes[cities[i][0]].Add(cities[i][1]);
        //    }

        //    if (!nodes.ContainsKey(cities[i][1]))
        //    {
        //        nodes.Add(cities[i][1], new List<int> { cities[i][0] });
        //    }
        //    else
        //    {
        //        nodes[cities[i][1]].Add(cities[i][0]);
        //    }
        //}

        //// find connected areas
        //var queue = new Queue<int>();
        //int areaIndex = 0;

        //for (int i = 1; i <= n; i++)
        //{
        //    if (visited[i]) continue;

        //    citiesPerConnectedArea.Add(0);
        //    queue.Enqueue(i);

        //    while (queue.Count > 0)
        //    {
        //        var node = queue.Dequeue();

        //        if (visited[node]) continue;
        //        visited[node] = true;

        //        if (nodes.ContainsKey(node))
        //        {
        //            foreach (var adjacentNode in nodes[node])
        //            {
        //                if (visited[adjacentNode]) continue;

        //                queue.Enqueue(adjacentNode);
        //            }
        //        }
        //        citiesPerConnectedArea[areaIndex]++;
        //    }
        //    areaIndex++;
        //}

        //foreach (var numOfCity in citiesPerConnectedArea)
        //{
        //    cost += (c_lib + (numOfCity - 1) * (long)c_road);
        //}

        //return cost;
        #endregion

        var connectedAreas = 0;
        bool[] visited = new bool[n + 1];
        List<List<int>> nextNodes = new List<List<int>>(n + 1);

        // if c_road > c_lib
        if ((long)c_road * (n - 1) > (long)c_lib * n)
        {
            return (long)c_lib * n;
        }

        for (int i = 0; i <= n; i++)
        {
            nextNodes.Add(new List<int>());
        }

        // build adjacent list
        for (int i = 0; i < cities.Count; i++)
        {
            nextNodes[cities[i][0]].Add(cities[i][1]);
            nextNodes[cities[i][1]].Add(cities[i][0]);
        }

        var queue = new Queue<int>();

        // find connected areas
        for (int i = 1; i <= n; i++)
        {
            if (visited[i]) continue;

            queue.Enqueue(i);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (visited[node] || node > nextNodes.Count - 1) continue;
                visited[node] = true;

                var current = nextNodes[node];
                foreach (var adjacentNode in current)
                {
                    if (!visited[adjacentNode])
                    {
                        queue.Enqueue(adjacentNode);
                    }
                }
            }
            connectedAreas++;
        }

        return connectedAreas * (long)c_lib + (n - connectedAreas) * (long)c_road;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/find-the-nearest-clone/problem
    /// </summary>
    public static int FindShortest(int graphNodes, int[] graphFrom, int[] graphTo, long[] ids, int val)
    {
        var nodes = new List<List<int>>(graphNodes + 1);
        var visited = new bool[graphNodes + 1];
        var edgeCount = graphFrom.Length;
        var minLength = int.MaxValue;

        for (int i = 0; i <= graphNodes; i++)
        {
            nodes.Add(new List<int>());
        }

        for (int i = 0; i < edgeCount; i++)
        {
            nodes[graphFrom[i]].Add(graphTo[i]);
            nodes[graphTo[i]].Add(graphFrom[i]);
        }

        for (int i = 1; i <= graphNodes; i++)
        {
            if (ids[i - 1] != val) continue;

            var queue = new Queue<int>();
            queue.Enqueue(i);

            var nodeWeight = new int[graphNodes + 1]; // store distant from start node

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (visited[node]) continue;

                visited[node] = true;

                if (ids[node - 1] == val && nodeWeight[node] > 0)
                {
                    visited[node] = false; // we want to revisit matching colour node as a new starting point
                    minLength = Math.Min(nodeWeight[node], minLength);
                }
                else // spread node search
                {
                    foreach (var nextNode in nodes[node])
                    {
                        if (visited[nextNode]) continue;

                        queue.Enqueue(nextNode);
                        nodeWeight[nextNode] = nodeWeight[node] + 1;
                    }
                }
            }
        }

        return minLength == int.MaxValue ? -1 : minLength;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/sherlock-and-anagrams/problem
    /// </summary>
    public static int FindSherlockAndAnagrams(string s)
    {
        var n = s.Length;
        var subStrLength = 1;
        var count = 0;

        while (subStrLength < n)
        {
            for (int i = 0; i < n; i++)
            {
                if (i + subStrLength > n) break;

                var subStrArr = s.Substring(i, subStrLength).ToArray();
                Array.Sort(subStrArr);
                var subStr = new string(subStrArr);

                for (int j = i + 1; j < n; j++)
                {
                    if (j + subStrLength > n) break;

                    var subStrArr2 = s.Substring(j, subStrLength).ToArray();
                    Array.Sort(subStrArr2);
                    var subStr2 = new string(subStrArr2);

                    if (subStr == subStr2) count++;
                }
            }
            subStrLength++;
        }

        return count;

        //int n = s.Length;
        //int count = 0;
        //Dictionary<string, int> dict = new Dictionary<string, int>();
        //for (int i = 0; i < n; i++)
        //{
        //    for (int j = i + 1; j <= n; j++)
        //    {
        //        char[] arr = s.Substring(i, j - i).ToCharArray();
        //        Array.Sort(arr);
        //        string str = new string(arr);
        //        if (dict.ContainsKey(str))
        //        {
        //            count += dict[str];
        //            dict[str]++;
        //        }
        //        else
        //        {
        //            dict.Add(str, 1);
        //        }
        //    }
        //}
        //return count;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem
    /// Naive solution - counting sort for every element added/removed
    /// </summary>
    public static int FindActivityNotifications(List<int> expenditure, int d)
    {
        var countingSort = (int[] input, int max) =>
        {
            int[] output = new int[input.Length];
            int[] count = new int[max + 1];

            for (int i = 0; i < input.Length; i++)
            {
                count[input[i]]++;
            }

            var oIndex = 0;
            for (int i = 0; i < count.Length; i++)
            {
                var c = count[i];
                while (c > 0)
                {
                    output[oIndex] = i;
                    oIndex++;
                    c--;
                }
            }

            return output;
        };

        var count = 0;

        List<int> sorted = new List<int>(d);

        for (int i = 0; i < expenditure.Count; i++)
        {
            if (i >= d)
            {
                double median;
                if (d % 2 == 0)
                {
                    median = (sorted[d / 2] + sorted[d / 2 - 1]) / 2d;
                }
                else
                {
                    median = sorted[d / 2];
                }
                if (expenditure[i] >= median * 2)
                {
                    count++;
                }

                sorted.Remove(expenditure[i - d]);
            }
            sorted.Add(expenditure[i]);
            sorted = countingSort(sorted.ToArray(), 200).ToList();
        }

        return count;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem
    /// Optimised solution - quickly add or remove item by indexes stored in the count array to maintain a sorted array
    /// </summary>
    public static int FindActivityNotificationsV2(List<int> expenditure, int d)
    {
        var getPos = (int[] count, int add, int remove) =>
        {
            var max = count.Length;
            var pos = new int[max];
            count[add]++;

            if (remove > -1)
            {
                count[remove]--;
            }

            Array.Copy(count, pos, max);
            for (int i = 1; i < max; i++)
            {
                pos[i] += pos[i - 1];
            }

            return pos;
        };

        var sort = (int[] input, int[] pos, int add, int removePos) =>
        {
            var n = input.Length;
            if (removePos > -1)
            {
                for (int i = removePos; i < n - 1; i++)
                {
                    input[i] = input[i + 1];
                }
            }
            if (pos[add] > -1)
            {
                for (int i = n - 1; i > pos[add] - 1; i--)
                {
                    input[i] = input[i - 1];
                }
                input[pos[add] - 1] = add;
            }
        };

        var count = 0;
        var sorted = new int[d];
        var max = 201;
        var countArr = new int[max];
        int[] pos = new int[max];
        Array.Fill(pos, -1);

        for (int i = 0; i < expenditure.Count; i++)
        {
            var add = expenditure[i];
            var removePos = -1;
            if (i >= d)
            {
                double median;
                if (d % 2 == 0)
                {
                    median = (sorted[d / 2] + sorted[d / 2 - 1]) / 2d;
                }
                else
                {
                    median = sorted[d / 2];
                }
                if (expenditure[i] >= median * 2)
                {
                    count++;
                }

                var remove = expenditure[i - d];
                removePos = pos[remove] - 1;
                pos = getPos(countArr, add, remove);
            }
            else
            {
                pos = getPos(countArr, add, -1);
            }
            sort(sorted, pos, add, removePos);
        }

        return count;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem
    /// Optimised solution 2 - no need to sort, based on the count array to determine the median value
    /// </summary>
    public static int FindActivityNotificationsV3(List<int> expenditure, int d)
    {
        bool isEven = d % 2 == 0;
        int medianPos = d / 2;

        var updateCount = (int[] count, int add, int remove) =>
        {
            count[add]++;

            if (remove > -1)
            {
                count[remove]--;
            }
        };

        var findMedian = (int[] count) =>
        {
            var max = count.Length;
            var sum = 0;
            var median = 0d;

            var findNext = (int[] count, int i) =>
            {
                var n = count.Length;
                for (int j = i + 1; j < n; j++)
                {
                    if (count[j] > 0)
                    {
                        return j;
                    }
                }
                throw new Exception();
            };

            for (int i = 0; i < max; i++)
            {
                sum += count[i];

                if (sum == medianPos && isEven)
                {
                    median = (i + findNext(count, i)) / 2d;
                    break;
                }
                if (sum > medianPos)
                {
                    median = i;
                    break;
                }
            }

            return median;
        };

        var count = 0;
        var max = 201;
        var countArr = new int[max];
        var n = expenditure.Count;

        for (int i = 0; i < n; i++)
        {
            var add = expenditure[i];
            if (i >= d)
            {
                double median = findMedian(countArr);
                if (expenditure[i] >= median * 2)
                {
                    count++;
                }

                var remove = expenditure[i - d];
                updateCount(countArr, add, remove);
            }
            else
            {
                updateCount(countArr, add, -1);
            }
        }

        return count;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/frequency-queries/problem
    /// </summary>
    public static List<int> FreqQuery(List<List<int>> queries)
    {
        var output = new List<int>();
        var itemCount = new Dictionary<int, int>();
        var freqCount = new Dictionary<int, int>();

        var incrementKey = (Dictionary<int, int> dict, int key) =>
        {
            if (dict.ContainsKey(key))
            {
                dict[key]++;
            }
            else
            {
                dict.Add(key, 1);
            }
        };

        var decrementKey = (Dictionary<int, int> dict, int key) =>
        {
            if (dict.ContainsKey(key) && dict[key] > 0)
            {
                dict[key]--;
                return true;
            }
            return false;
        };

        foreach (var query in queries)
        {
            switch (query[0])
            {
                case 1:
                    incrementKey(itemCount, query[1]);

                    incrementKey(freqCount, itemCount[query[1]]);
                    decrementKey(freqCount, itemCount[query[1]] - 1);
                    break;
                case 2:
                    if (decrementKey(itemCount, query[1]))
                    {
                        incrementKey(freqCount, itemCount[query[1]]);
                        decrementKey(freqCount, itemCount[query[1]] + 1);
                    }
                    break;
                case 3:
                    output.Add(freqCount.ContainsKey(query[1]) && freqCount[query[1]] > 0 ? 1 : 0);
                    break;
            }
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/ctci-comparator-sorting/problem
    /// </summary>
    public static List<Tuple<string, int>> SortingWithComparator(List<Tuple<string, int>> input)
    {
        //var n = int.Parse(Console.ReadLine());
        //List<Tuple<string, int>> input = new();

        //for (int i = 0; i < n; i++)
        //{
        //    var lineParts = Console.ReadLine().Split(' ');
        //    input.Add(new(lineParts[0], int.Parse(lineParts[1])));
        //}

        input.Sort((x, y) =>
        {
            if (x.Item2 < y.Item2) return 1;
            if (x.Item2 > y.Item2) return -1;

            return x.Item1.CompareTo(y.Item1);
        });

        return input;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/special-palindrome-again/problem
    /// Best performance but long and slightly complex approach
    /// </summary>
    public static long SubstrCount(int n, string s)
    {
        var countSpecialStringsAllSame = (int @in) =>
        {
            long @out = 0;
            for (int i = 0; i < @in; i++)
            {
                @out += i;
            }

            return @out;
        };

        if (n == 1) return 1;

        long result = 0;
        int count = 1;
        int balanceCount = 0;

        for (int i = 0; i < n; i++)
        {
            if (i > 1)
            {
                if (s[i] == s[i - 2] && s[i] != s[i - 1])
                {
                    if (balanceCount > 0)
                    {
                        result += balanceCount;
                    }
                    balanceCount = 1;
                }
                else if (balanceCount > 0 && s[i] == s[i - 1]
                    && i - (balanceCount + 1) * 2 is int oppositeIndex && oppositeIndex > -1 && s[i] == s[oppositeIndex])
                {
                    balanceCount++;
                }
                else
                {
                    result += balanceCount;
                    balanceCount = 0;
                }

                if (i == n - 1)
                {
                    result += balanceCount;
                    balanceCount = 0;
                }
            }

            if (i < n - 1 && s[i] == s[i + 1])
            {
                count++;
            }
            else
            {
                if (count > 1)
                {
                    result += countSpecialStringsAllSame(count);
                }
                count = 1;
            }
        }
        result += n;

        return result;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/special-palindrome-again/problem
    /// Solve by storing the counts of repeating chars
    /// </summary>
    public static long SubstrCountV2(int n, string s)
    {
        long output = 0;

        var charCounts = new List<(char @char, long count)>();

        if (n == 1) return 1;

        long counter = 1;

        for (int i = 1; i < n; i++)
        {
            if (s[i] == s[i - 1])
            {
                counter++;
            }
            else
            {
                charCounts.Add((s[i - 1], counter));
                counter = 1;
            }
        }
        charCounts.Add((s[n - 1], counter));

        // same characters repeating, i.e. aaa, aaaa...
        for (int i = 0; i < charCounts.Count; i++)
        {
            output += charCounts[i].count * (charCounts[i].count + 1) / 2;
        }

        // characters repeating on both sides of another character, i.e. aba, abaa...
        for (int i = 2; i < charCounts.Count; i++)
        {
            if (charCounts[i - 1].count == 1 && charCounts[i - 2].@char == charCounts[i].@char)
            {
                output += Math.Min(charCounts[i - 2].count, charCounts[i].count);
            }
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/common-child/problem
    /// Unoptimised approach - store max values in an array and loop back to find the max value before an index
    /// </summary>
    public static int CommonChild(string s1, string s2)
    {
        int output = 0;
        var n1 = s1.Length;
        var n2 = s2.Length;
        var maxes = new int[5001];
        var posDict = new Dictionary<char, List<int>>();

        var maxBefore = (int key) =>
        {
            var max = 0;
            for (int i = key; i >= 0; i--)
            {
                if (max < maxes[i]) max = maxes[i];
            }
            return max;
        };

        for (int i = 0; i < n2; i++)
        {
            if (!posDict.ContainsKey(s2[i])) posDict.Add(s2[i], new List<int> { i });
            else posDict[s2[i]].Add(i);
        }

        for (int i = 0; i < n1; i++)
        {
            if (posDict.ContainsKey(s1[i]))
            {
                var toUpdates = new List<KeyValuePair<int, int>>();

                foreach (var pos in posDict[s1[i]])
                {
                    toUpdates.Add(new KeyValuePair<int, int>(pos + 1, maxBefore(pos) + 1));
                }

                foreach (var kvp in toUpdates)
                {
                    maxes[kvp.Key] = kvp.Value;

                    if (output < kvp.Value) output = kvp.Value;
                }
            }
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/common-child/problem
    /// Optimised approach - Dp table
    /// </summary>
    public static int CommonChildV2(string s1, string s2)
    {
        var dp = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i < s1.Length; i++)
        {
            for (int j = 0; j < s2.Length; j++)
            {
                if (dp[i, j + 1] == dp[i + 1, j]
                    && dp[i, j] == dp[i + 1, j]
                    && s1[i] == s2[j])
                {
                    dp[i + 1, j + 1] = Math.Max(dp[i, j + 1], dp[i + 1, j]) + 1;
                }
                else
                {
                    dp[i + 1, j + 1] = Math.Max(dp[i, j + 1], dp[i + 1, j]);
                }
            }
        }

        return dp[s1.Length, s2.Length];
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/greedy-florist/problem
    /// </summary>
    public static int GetMinimumCost(int k, int[] c)
    {
        int output = 0;
        var n = c.Length;
        int round = 0;

        Array.Sort(c);

        for (int i = n - 1; i >= 0;)
        {
            for (int j = 0; j < k && i >= 0; j++)
            {
                output += (round + 1) * c[i];
                i--;
            }
            round++;
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/angry-children/problem
    /// </summary>
    public static int MaxMin(int k, List<int> arr)
    {
        var output = int.MaxValue;

        arr.Sort();

        for (int i = 0; i < arr.Count - k + 1; i++)
        {
            var gap = arr[i + k - 1] - arr[i];
            if (gap < output)
            {
                output = gap;
            }
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/ctci-ice-cream-parlor/problem
    /// </summary>
    public static string WhatFlavors(List<int> cost, int money)
    {
        var dict = new Dictionary<int, int>();
        var output = string.Empty;

        for (int i = 0; i < cost.Count; i++)
        {
            var makeUp = money - cost[i];

            if (makeUp <= 0) continue;

            if (dict.ContainsKey(cost[i]))
            {
                output = $"{dict[cost[i]]} {i + 1}";
            }

            if (!dict.ContainsKey(makeUp))
            {
                dict[makeUp] = i + 1;
            }
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/pairs/problem
    /// </summary>
    public static int Pairs(int k, List<int> arr)
    {
        var output = 0;
        var set = new HashSet<int>();

        foreach (var item in arr)
        {
            set.Add(item);

            if (set.Contains(item + k))
            {
                output++;
            }
            if (set.Contains(item - k))
            {
                output++;
            }
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/triple-sum/problem
    /// </summary>
    public static long Triplets(int[] a, int[] b, int[] c)
    {
        var output = 0L;
        var countA = 0L;
        var countC = 0L;
        var minusA = 0L; // minus duplicate elements
        var minusC = 0L;

        Array.Sort(a);
        Array.Sort(b);
        Array.Sort(c);

        HashSet<int> seenA = new();
        HashSet<int> seenB = new();
        HashSet<int> seenC = new();

        foreach (var item in b)
        {
            if (seenB.Contains(item)) continue;

            seenB.Add(item);

            while (countA < a.Length && item >= a[countA])
            {
                if (seenA.Contains(a[countA])) minusA++;

                seenA.Add(a[countA]);

                countA++;
            }

            while (countC < c.Length && item >= c[countC])
            {
                if (seenC.Contains(c[countC])) minusC++;

                seenC.Add(c[countC]);

                countC++;
            }

            output += (countA - minusA) * (countC - minusC);
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/minimum-time-required/problem
    /// Naive solution - count each day of production
    /// </summary>
    public static long MinTime(long[] machines, long goal)
    {
        var output = 0L;
        var production = 0L;

        while (production < goal)
        {
            output++;

            foreach (var machine in machines)
            {
                if (output % machine == 0)
                {
                    production++;
                }
            }
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/minimum-time-required/problem
    /// Optimised - Try to search for the min days by calculating approximate bounds and deltas
    /// </summary>
    public static long MinTimeV2(long[] machines, long goal)
    {
        var productionPerDay = 0M;

        foreach (var machine in machines)
        {
            productionPerDay += 1M / machine;
        }

        var approximate = (long)Math.Round(goal / productionPerDay, MidpointRounding.ToPositiveInfinity);
        var production = 0L;

        foreach (var machine in machines)
        {
            production += approximate / machine;
        }

        // Delta for the search
        var days = Math.Max((long)(1 / productionPerDay), 1) * Math.Abs(goal - production);

        do
        {
            if (days < 10)
            {
                days = 1;
            }

            while (production >= goal)
            {
                production = 0;
                approximate -= days;
                foreach (var machine in machines)
                {
                    production += approximate / machine;
                }
            }

            while (production < goal)
            {
                production = 0;
                approximate += days;
                foreach (var machine in machines)
                {
                    production += approximate / machine;
                }
            }

            days = Math.Max(days / 10, 1);
        }
        while (days != 1); // search until we reach highest precision

        return approximate;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/minimum-time-required/problem
    /// Optimised - Binary search
    /// </summary>
    public static long MinTimeV3(long[] machines, long goal)
    {
        Array.Sort(machines);
        var lowerBound = 1L;
        var upperBound = machines[machines.Length - 1] * goal;

        while (lowerBound < upperBound)
        {
            var mid = (lowerBound + upperBound) / 2;
            var production = 0L;

            foreach (var machine in machines)
            {
                production += mid / machine;
            }

            // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
            if (production >= goal)
            {
                upperBound = mid;
            }
            else
            {
                lowerBound = mid + 1;
            }
        }

        return lowerBound;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/maximum-subarray-sum/problem
    /// </summary>
    public static long MaximumSum(List<long> a, long m)
    {
        // m = 7
        // 3 3 9 9 5 - 3 6 15 24 29 - 3 6 1 3 1
        // 1 1 2 3
        // 1 1 3 4 4
        // 1 1 3 2 4 2

        // (2 + 1 + 5 + 7) % 7 - (5 + 7) % 7

        var output = 0L;
        var currentSum = 0L;
        var possibleSums = new SortedSet<long>();

        foreach (var item in a)
        {
            currentSum = (currentSum + item) % m;
            output = Math.Max(currentSum, output);

            // all sums range from 0 to m. Find max by looking at possibleSums[i] greater and closest to currentSum
            // max = (currentSum - possibleSums[i] + m) % m
            var greaterAndClosest = possibleSums.GetViewBetween(currentSum + 1, m).Min;

            if (greaterAndClosest > 0)
            {
                output = Math.Max((currentSum - greaterAndClosest + m) % m, output);
            }
            if (output == m - 1) break;

            possibleSums.Add(currentSum);
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/maxsubarray/problem
    /// </summary>
    public static List<int> MaxSubarray(List<int> arr)
    {
        var output = new List<int>();
        var max1 = 0;
        var max2 = 0;
        var currentSum = 0;
        var possibleSums = new SortedSet<int>();
        var sortedValues = new SortedSet<int>();

        foreach (var item in arr)
        {
            // find max subarrays
            currentSum += item;
            max1 = Math.Max(currentSum, max1);

            if (possibleSums.Min < 0)
            {
                max1 = Math.Max(currentSum - possibleSums.Min, max1);
            }

            sortedValues.Add(item);
            possibleSums.Add(currentSum);

            // find max subsequences
            if (item > 0)
            {
                max2 += item;
            }
        }

        if (max2 == 0)
        {
            max1 = max2 = sortedValues.Max;
        }
        output.Add(max1);
        output.Add(max2);

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/stockmax/problem
    /// </summary>
    public static long Stockmax(List<int> prices)
    {
        var output = 0L;
        var max = 0;

        for (int i = prices.Count - 1; i >= 0; i--)
        {
            max = Math.Max(max, prices[i]);

            output += max - prices[i];
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/largest-rectangle/problem
    /// </summary>
    public static long LargestRectangle(List<int> h)
    {
        var output = 0L;

        for (int i = 0; i < h.Count; i++)
        {
            var left = i;
            var right = i;
            var width = 1;

            while (left > 0 && h[i] <= h[left - 1])
            {
                width++;
                left--;
            }

            while (right < h.Count - 1 && h[i] <= h[right + 1])
            {
                width++;
                right++;
            }

            output = Math.Max(output, width * h[i]);
        }

        return output;
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/min-max-riddle/problem
    /// Brute force
    /// </summary>
    public static long[] Riddle(long[] arr)
    {
        var output = new List<long>();

        for (int windowSize = 1; windowSize < arr.Length + 1; windowSize++)
        {
            int start = 0;
            long maxOfMin = int.MinValue;

            while (start <= arr.Length - windowSize)
            {
                var min = arr[start];

                for (int j = 0; j < windowSize; j++)
                {
                    if (arr[start + j] < min) min = arr[start + j];
                }

                if (maxOfMin < min) maxOfMin = min;

                start++;
            }
            output.Add(maxOfMin);
        }

        return output.ToArray();
    }

    /// <summary>
    /// https://www.hackerrank.com/challenges/min-max-riddle/problem
    /// Solution copied from https://www.geeksforgeeks.org/find-the-maximum-of-minimums-for-every-window-size-in-a-given-array/
    /// Still trying to understand why `ans[len] = Math.Max(ans[len], arr[i]);`
    /// </summary>
    public static long[] RiddleV2(long[] arr)
    {
        // Used to find previous and next smaller
        Stack<int> s = new Stack<int>();
        var n = arr.Length;

        // Arrays to store previous
        // and next smaller
        int[] left = new int[n + 1];
        int[] right = new int[n + 1];

        // Initialize elements of left[]
        // and right[]
        for (int i = 0; i < n; i++)
        {
            left[i] = -1;
            right[i] = n;
        }

        // Fill elements of left[] using logic discussed on
        // https://www.geeksforgeeks.org/next-greater-element/
        for (int i = 0; i < n; i++)
        {
            while (s.Count > 0 && arr[s.Peek()] >= arr[i])
            {
                s.Pop();
            }

            if (s.Count > 0)
            {
                left[i] = s.Peek();
            }

            s.Push(i);
        }

        // Empty the stack as stack is going
        // to be used for right[]
        while (s.Count > 0)
        {
            s.Pop();
        }

        // Fill elements of right[] using
        // same logic
        for (int i = n - 1; i >= 0; i--)
        {
            while (s.Count > 0 && arr[s.Peek()] >= arr[i])
            {
                s.Pop();
            }

            if (s.Count > 0)
            {
                right[i] = s.Peek();
            }

            s.Push(i);
        }

        // Create and initialize answer array
        long[] ans = new long[n + 1];

        // Fill answer array by comparing
        // minimums of all lengths computed
        // using left[] and right[]
        for (int i = 0; i < n; i++)
        {
            // length of the interval
            int len = right[i] - left[i] - 1;

            // 2, 6, 1, 12 - left: -1, 0, -1, 2 - right: 2, 2, 4, 4
            // 2, 6, 1, 12, 4 - left: -1, 0, -1, 2, 2, 0 - right: 2, 2, 5, 4, 5, 0
            // 6, 1, 12 - left: -1, -1, 1, 0 - right: 1, 3, 3, 0

            // arr[i] is a possible answer for
            // this length 'len' interval, check x
            // if arr[i] is more than max for 'len'
            ans[len] = Math.Max(ans[len], arr[i]);
        }

        // Some entries in ans[] may not be
        // filled yet. Fill them by taking
        // values from right side of ans[]
        for (int i = n - 1; i >= 1; i--)
        {
            ans[i] = Math.Max(ans[i], ans[i + 1]);
        }

        return ans.Skip(1).ToArray();
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-rounds-to-complete-all-tasks/
    /// </summary>
    public static int MinimumRounds(int[] tasks)
    {
        var dict = new Dictionary<int, int>();
        var output = 0;

        for (int i = 0; i < tasks.Length; i++)
        {
            if (!dict.ContainsKey(tasks[i]))
            {
                dict.Add(tasks[i], 1);
            }
            else
            {
                dict[tasks[i]]++;
            }
        }

        foreach (var val in dict.Values)
        {
            if (val < 2) return -1;

            var div = val / 3;
            var mod = val % 3;

            if (mod > 0)
            {
                output += div + 1;
            }
            else
            {
                output += div;
            }
        }

        return output;
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-sum-of-squared-difference/description/
    /// </summary>
    public static long MinSumSquareDiff(int[] nums1, int[] nums2, int k1, int k2)
    {
        var output = 0L;
        var diffs = new PriorityQueue<int, int>();
        var n = nums1.Length;
        var sumK = k1 + k2;

        for (int i = 0; i < n; i++)
        {
            var diff = Math.Abs(nums1[i] - nums2[i]);
            diffs.Enqueue(diff, -diff);
        }

        // 7, 10
        // 2, 1
        // 5, 9 - 5, 0 - 2, 3
        // 9

        while (sumK > 0)
        {
            var diff = diffs.Dequeue();
            diff--;

            if (diff < 0) break;

            diffs.Enqueue(diff, -diff);

            sumK--;
        }

        while (diffs.Count > 0)
        {
            output += (long)Math.Pow(diffs.Dequeue(), 2);
        }

        return output;
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-sum-of-squared-difference/description/
    /// </summary>
    public static long MinSumSquareDiffV2(int[] nums1, int[] nums2, int k1, int k2)
    {
        var output = 0L;
        var diffs = new List<int>();
        var n = nums1.Length;
        var sumK = k1 + k2;

        // the trick here is to levelling the diffs
        for (int i = 0; i < n; i++)
        {
            var diff = Math.Abs(nums1[i] - nums2[i]);
            diffs.Add(diff);
        }
        diffs.Add(0);

        diffs.Sort();

        // nums1: 7, 10
        // nums2: 2, 1
        // diffs: 5, 9 - k1 + k2 = 9
        // choices: 5, 0 vs 2, 3
        for (int i = n; i >= 0; i--)
        {
            if (i > 0 && diffs[i] > diffs[i - 1])
            {
                // distribute shared portion to level the diffs
                var gap = diffs[n] - diffs[i - 1];
                var portion = Math.Max(sumK / (n - i + 1), 1);

                for (int j = 0; sumK > 0 && j <= n - i; j++)
                {
                    var sub = Math.Min(portion, gap);
                    diffs[n - j] -= sub;
                    sumK -= sub;

                    if (sumK == 0) break;
                }
            }

            if (sumK == 0) break;
        }

        var prev = diffs[n];

        // distribute left overs of sumK
        while (sumK > 0 && prev > 0)
        {
            for (int i = n; i > 0; i--)
            {
                prev = diffs[i];

                if (prev == 0 || sumK == 0)
                {
                    break;
                }

                diffs[i]--;
                sumK--;
            }
        }

        for (int i = 1; i <= n; i++)
        {
            output += (long)Math.Pow(diffs[i], 2);
        }

        return output;
    }

    /// <summary>
    /// https://leetcode.com/problems/count-number-of-bad-pairs/
    /// </summary>
    public static long CountBadPairs(int[] nums)
    {
        var count = 0L;
        var n = nums.Length;

        //--- brute force ---\\
        //for (int i = 0; i < n; i++)
        //{
        //    for (int j = i + 1; j < n; j++)
        //    {
        //        if (nums[j] - nums[i] != j - i)
        //            count++;
        //    }
        //}

        //--- improved ---\\
        // ( j - i ) != ( A[j] - A[i] ) => ( A[i] - i ) != ( A[j] - j )
        // good pairs: ( A[i] - i ) == ( A[j] - j )

        // total pairs
        // (n - 1) + (n - 2) + ... 1 for (n - 1) times
        // 1 + 2 + ... + (n - 1) => n * (n - 1) / 2 loops
        // 0 0 0 0
        // 1 0 0 0
        // 1 1 0 0
        // 1 1 1 0
        // 1 1 1 1

        // or just simply loop :D
        var totalPairs = 0L;
        for (int i = 0; i < n - 1; i++)
        {
            totalPairs += n - i - 1;
        }

        Dictionary<long, long> dict = new();
        for (int i = 0; i < n; i++)
        {
            var diff = nums[i] - i;
            if (!dict.ContainsKey(diff)) dict[diff] = 1;
            else dict[diff]++;
        }

        foreach (var goodKeys in dict.Values)
        {
            var goodPairs = goodKeys * (goodKeys - 1) / 2;
            count += goodPairs;
        }

        return totalPairs - count;
    }

    /// <summary>
    /// https://leetcode.com/problems/all-paths-from-source-to-target/
    /// </summary>
    public static IList<IList<int>> AllPathsSourceTarget(int[][] graph)
    {
        var output = new List<IList<int>>();
        var n = graph.Length;

        void Next(int curr, int[] path, int depth)
        {
            path[depth] = curr;

            if (curr == n - 1)
            {
                output.Add(path[..(depth + 1)]);
            }

            foreach (var node in graph[curr])
            {
                Next(node, path, depth + 1);
            }
        }

        Next(0, new int[n], 0);

        return output;
    }

    /// <summary>
    /// https://leetcode.com/problems/unique-paths/
    /// </summary>
    public static int UniquePaths(int m, int n)
    {
        var dp = new int[m, n];

        for (int i = 0; i < m; i++)
        {
            dp[i, 0] = 1;
        }

        for (int j = 0; j < n; j++)
        {
            dp[0, j] = 1;
        }

        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
            }
        }

        return dp[m - 1, n - 1];
    }

    /// <summary>
    /// https://leetcode.com/problems/triangle/
    /// </summary>
    public static int MinimumTotal(IList<IList<int>> triangle)
    {
        for (int i = 1; i < triangle.Count; i++)
        {
            for (int j = 0; j < triangle[i].Count; j++)
            {
                triangle[i][j] = triangle[i][j]
                    + Math.Min(triangle[i - 1][j > 0 ? j - 1 : j], triangle[i - 1][j > triangle[i - 1].Count - 1 ? triangle[i - 1].Count - 1 : j]);
            }
        }

        return triangle[triangle.Count - 1].Min();
    }

    /// <summary>
    /// https://leetcode.com/problems/partition-equal-subset-sum
    /// brute force
    /// </summary>
    public static bool CanPartition(int[] nums)
    {
        var sum = nums.Sum();

        if (sum % 2 != 0) return false;

        var total = sum / 2;
        Array.Sort(nums);

        if (nums[^1] > total || nums.Length == 1) return false;

        var possibleSums = new HashSet<int> { 0 };

        // 23, 13, 11, 7, 6, 5, 5
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            if (nums[i] > total)
            {
                return false;
            }

            var count = possibleSums.Count;
            for (int j = 0; j < count; j++)
            {
                possibleSums.Add(possibleSums.ElementAt(j) + nums[i]);
            }

            if (possibleSums.Contains(total)) return true;
        }

        return false;
    }

    /// <summary>
    /// https://leetcode.com/problems/partition-equal-subset-sum
    /// recursion
    /// </summary>
    public static bool CanPartitionV2(int[] nums)
    {
        var sum = nums.Sum();

        if (sum % 2 != 0) return false;

        var total = sum / 2;

        if (nums[^1] > total || nums.Length == 1) return false;

        var calculated = new HashSet<(int, int)>();

        bool Check(int tmpSum, int i)
        {
            if (calculated.Contains((tmpSum, i))) return false;

            if (tmpSum == total) return true;

            if (i >= nums.Length || nums[i] > total) return false;

            calculated.Add((tmpSum, i));

            return Check(tmpSum, i + 1) || Check(tmpSum + nums[i], i + 1);
        }

        return Check(0, 0);
    }

    /// <summary>
    /// https://leetcode.com/problems/remove-duplicate-letters/
    /// brute force
    /// </summary>
    public static string RemoveDuplicateLetters(string s)
    {
        var shortest = string.Empty;
        var calculated = new HashSet<(string, int)>();

        static bool IsGreater(string s1, string s2)
        {
            if (s1.Length == 0) return true;

            for (int i = 0; i < s1.Length && i < s2.Length; i++)
            {
                if (s1[i] == s2[i]) continue;

                return s1[i] > s2[i];
            }

            return s1.Length > s2.Length;
        }

        var stack = new Stack<(string, int)>();
        stack.Push((string.Empty, 0));

        while (stack.Count > 0)
        {
            var s1 = stack.Pop();

            if (s1.Item2 == s.Length)
            {
                shortest = IsGreater(shortest, s1.Item1) ? s1.Item1 : shortest;
                continue;
            }

            if (calculated.Contains(s1)) continue;

            calculated.Add(s1);

            var index = s1.Item1.IndexOf(s[s1.Item2]);

            if (index == -1)
            {
                stack.Push((s1.Item1 + s[s1.Item2], s1.Item2 + 1));
                continue;
            }

            stack.Push((s1.Item1.Remove(index, 1) + s[s1.Item2], s1.Item2 + 1));
            stack.Push((s1.Item1, s1.Item2 + 1));
        }

        return shortest;
    }

    /// <summary>
    /// https://leetcode.com/problems/remove-duplicate-letters/
    /// optimised
    /// </summary>
    public static string RemoveDuplicateLettersV2(string s)
    {
        var stack = new Stack<char>();
        var seen = new HashSet<int>();
        var lastOccurrence = new Dictionary<char, int>();

        for (int i = 0; i < s.Length; i++)
        {
            lastOccurrence[s[i]] = i;
        }

        for (int i = 0; i < s.Length; i++)
        {
            var c = s[i];
            if (!seen.Contains(c))
            {
                while (stack.Count > 0 && c < stack.Peek() && i < lastOccurrence[stack.Peek()])
                {
                    seen.Remove(stack.Pop());
                }
                seen.Add(c);
                stack.Push(c);
            }
        }

        return string.Join("", stack.ToArray().Reverse());
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-moves-to-equal-array-elements-ii/
    /// </summary>
    public static int MinMoves2(int[] nums)
    {
        // 1, 2, 9, 10 - chosen - any from 2 to 9
        // 1, 2, 3, 9, 10 - chosen - 3

        Array.Sort(nums);
        var chosen = nums[nums.Length / 2];
        var output = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            output += Math.Abs(chosen - nums[i]);
        }

        return output;
    }

    /// <summary>
    /// https://leetcode.com/problems/merge-sorted-array
    /// </summary>
    public static void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        // 1,2,3,4,0,0,0,0 - 1,2,3,4,0,0,0,5 - 1,2,3,4,0,0,4,5 - 1,2,3,4,0,3,4,5 - 1,2,3,4,3,3,4,5 - 1,2,3,2,3,3,4,5 - 1,2,2,2,3,3,4,5
        // 2,2,3,5

        var rightIndex = m + n - 1;
        while (m > 0 && n > 0)
        {
            if (nums2[n - 1] >= nums1[m - 1])
            {
                nums1[rightIndex] = nums2[n - 1];
                n--;
            }
            else
            {
                nums1[rightIndex] = nums1[m - 1];
                m--;
            }
            rightIndex--;
        }

        while (n > 0)
        {
            nums1[rightIndex] = nums2[n - 1];
            n--;
            rightIndex--;
        }
    }

    public static int RemoveElement(int[] nums, int val)
    {
        var k = 0;
        var j = nums.Length - 1;

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != val) k++;
        }

        for (int i = 0; i < j; i++)
        {
            if (nums[i] != val) continue;

            while (nums[j] == val && j > 0)
            {
                j--;
            }

            nums[i] = nums[j];
            j--;
        }

        return k;
    }

    /// <summary>
    /// https://leetcode.com/problems/majority-element
    /// </summary>
    public static int MajorityElement(int[] nums)
    {
        var majorNum = 0;
        var counter = 0;

        foreach (var num in nums)
        {
            if (counter == 0) majorNum = num;

            if (num != majorNum) counter--;
            else counter++;
        }

        return majorNum;
    }

    /// <summary>
    /// https://leetcode.com/problems/gas-station/description
    /// </summary>
    public static int CanCompleteCircuit(int[] gas, int[] cost)
    {
        // gas = [1,2,3,4,5], cost = [3,4,5,1,2]
        // -2, -2, -2, 3, 3

        var n = gas.Length;
        var total = 0;
        var totalSurplus = 0;
        var start = 0;

        for (int i = 0; i < n; i++)
        {
            var surplus = gas[i] - cost[i];
            totalSurplus += surplus;

            if (totalSurplus < 0)
            {
                totalSurplus = 0;
                start = i + 1;
            }
            total += surplus;
        }

        return total < 0 ? -1 : start;
    }

    /// <summary>
    /// https://leetcode.com/problems/trapping-rain-water
    /// </summary>
    public static int Trap(int[] height)
    {
        var n = height.Length;
        var ans = 0;

        var maxes = new int[n];
        maxes[0] = height[0];

        for (int i = 1; i < n; i++)
        {
            maxes[i] = Math.Max(maxes[i - 1], height[i]);
        }

        maxes[n - 1] = height[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            maxes[i] = Math.Min(maxes[i], Math.Max(height[i], maxes[i + 1]));
        }

        for (int i = 0; i < n; i++)
        {
            if (maxes[i] - height[i] > 0)
            {
                ans += maxes[i] - height[i];
            }
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/longest-common-prefix/description
    /// </summary>
    public static string LongestCommonPrefix(string[] strs)
    {
        if (strs.Length == 0) return "";
        if (strs.Length == 1) return strs[0];

        var ans = strs[0].AsSpan();

        for (int i = 1; i < strs.Length; i++)
        {
            var length = Math.Min(strs[i].Length, ans.Length);
            ans = ans[..length];

            for (int j = 0; j < length; j++)
            {
                if (ans[j] != strs[i][j])
                {
                    ans = ans[..j];

                    if (ans.Length == 0) return string.Empty;

                    break;
                }
            }
        }

        return ans.ToString();
    }

    /// <summary>
    /// https://leetcode.com/problems/zigzag-conversion/description
    /// </summary>
    public static string Convert(string s, int numRows)
    {
        if (numRows == 1) return s;
        if (numRows >= s.Length) return s;

        var strs = new string[numRows];
        var ans = new StringBuilder();
        var k = 0;
        var isDownward = true;

        for (int i = 0; i < s.Length; i++)
        {
            strs[k] += s[i];

            if (isDownward)
            {
                if (k == numRows - 1)
                {
                    isDownward = false;
                    k--;
                    continue;
                }

                k++;
            }
            else
            {
                if (k == 0)
                {
                    isDownward = true;
                    k++;
                    continue;
                }

                k--;
            }
        }

        for (int i = 0; i < strs.Length; i++)
        {
            ans.Append(strs[i]);
        }

        return ans.ToString();
    }

    /// <summary>
    /// https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string
    /// </summary>
    public static int StrStr(string haystack, string needle)
    {
        var span = haystack.AsSpan();

        for (int i = 0; i < span.Length; i++)
        {
            if (i + needle.Length <= span.Length && span.Slice(i, needle.Length).SequenceEqual(needle))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// https://leetcode.com/problems/text-justification
    /// </summary>
    public static IList<string> FullJustify(string[] words, int maxWidth)
    {
        var lines = new List<List<string>>();
        var currLineLength = 0;
        var currLineCount = 0;

        // calculate words per line
        lines.Add(new List<string>());

        for (int i = 0; i < words.Length; i++)
        {
            if (currLineLength == 0)
            {
                lines[currLineCount].Add(words[i]);
                currLineLength += words[i].Length;
            }
            else
            {
                if (currLineLength + words[i].Length + 1 <= maxWidth)
                {
                    lines[currLineCount].Add(words[i]);
                    currLineLength += words[i].Length + 1;
                }
                else
                {
                    i--;
                    lines.Add(new List<string>());
                    currLineCount++;
                    currLineLength = 0;
                }
            }
        }

        // calculate spaces per line
        var allSpaces = new List<List<string>>(lines.Count);

        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var length = line.Sum(word => word.Length);

            if (i != lines.Count - 1 && line.Count != 1)
            {
                var lineSpaces = new List<string>(line.Count);
                var availableSpaces = maxWidth - length;
                var isEvenSpaces = false;
                var maxSpace = 0;
                var idealSpace = string.Empty;

                for (int j = 1; j < line.Count; j++)
                {
                    if (!isEvenSpaces) // spaces can be evenly distributed, no need to recalculate
                    {
                        isEvenSpaces = availableSpaces % (line.Count - j) == 0;
                        maxSpace = isEvenSpaces
                            ? availableSpaces / (line.Count - j)
                            : availableSpaces / (line.Count - j) + 1;

                        idealSpace = new string(' ', maxSpace);
                    }

                    availableSpaces -= maxSpace;
                    lineSpaces.Add(idealSpace);
                }
                allSpaces.Add(lineSpaces);
            }
            else // fill spaces in last line, or single word lines
            {
                var arrSpace = new string[line.Count];
                Array.Fill(arrSpace, " ");
                arrSpace[line.Count - 1] = new string(' ', maxWidth - length - line.Count + 1);

                allSpaces.Add(new List<string>(arrSpace));
            }
        }

        // aggregate the result
        var ans = new List<string>(lines.Count);

        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var spaces = allSpaces[i];
            var lineStr = new StringBuilder();

            for (int j = 0; j < line.Count; j++)
            {
                lineStr.Append(line[j]).Append(j < spaces.Count ? spaces[j] : string.Empty);
            }

            ans.Add(lineStr.ToString());
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/3sum
    /// </summary>
    public static IList<IList<int>> ThreeSum(int[] nums)
    {
        var ans = new List<IList<int>>();
        Array.Sort(nums);

        for (int i = 0; i < nums.Length - 2; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1]) continue;

            var right = nums.Length - 1;
            var left = i + 1;

            while (left < right)
            {
                if (nums[i] + nums[left] + nums[right] < 0)
                {
                    left++;
                    continue;
                }

                if (nums[i] + nums[left] + nums[right] == 0)
                {
                    List<int> match = [nums[i], nums[left], nums[right]];
                    ans.Add(match);

                    while (left < right && nums[left] == match[1])
                        left++;
                    while (left < right && nums[right] == match[2])
                        right--;
                }

                if (nums[i] + nums[left] + nums[right] > 0)
                {
                    right--;
                }
            }
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-size-subarray-sum
    /// </summary>
    public static int MinSubArrayLen(int target, int[] nums)
    {
        var sum = 0;
        var currWindowLength = 0;
        var minWindowLength = int.MaxValue;

        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];
            currWindowLength++;

            while (i + 1 - currWindowLength >= 0 && sum - nums[i + 1 - currWindowLength] >= target)
            {
                sum -= nums[i + 1 - currWindowLength];
                currWindowLength--;
            }

            if (sum >= target)
                minWindowLength = Math.Min(minWindowLength, currWindowLength);
        }

        return minWindowLength == int.MaxValue ? 0 : minWindowLength;
    }

    /// <summary>
    /// https://leetcode.com/problems/longest-substring-without-repeating-characters
    /// </summary>
    public static int LengthOfLongestSubstring(string s)
    {
        var max = 0;
        var currWindow = 0;
        var lastSeen = new Dictionary<char, int>();

        for (int i = 0; i < s.Length; i++)
        {
            if (!lastSeen.TryAdd(s[i], i))
            {
                var j = i - currWindow;
                while (j < lastSeen[s[i]])
                {
                    lastSeen.Remove(s[j]);
                    j++;
                }
                currWindow = i - lastSeen[s[i]];
                lastSeen[s[i]] = i;
            }
            else
            {
                currWindow++;
                max = Math.Max(max, currWindow);
            }

        }

        return max;
    }

    /// <summary>
    /// https://leetcode.com/problems/substring-with-concatenation-of-all-words
    /// </summary>
    public static IList<int> FindSubstring(string s, string[] words)
    {
        var ans = new List<int>();

        // ["foo","bar"]
        // barfoothefoobarman
        var wordLength = words[0].Length;
        var allDict = new Dictionary<string, int>();
        var allWordLength = wordLength * words.Length;

        if (allWordLength > s.Length)
            return ans;

        // Try to reuse the previously calculated observedDict: i -> i + w -> i + 2w...
        var observedDicts = new Dictionary<int, Dictionary<string, int>>();

        for (int i = 0; i < wordLength; i++)
        {
            var observedDict = new Dictionary<string, int>();
            var j = i;

            while (j - i + wordLength <= allWordLength && j + wordLength <= s.Length)
            {
                var word = s.Substring(j, wordLength);

                if (observedDict.ContainsKey(word))
                {
                    observedDict[word]++;
                }
                else
                {
                    observedDict.Add(word, 1);
                }

                j += wordLength;
            }

            observedDicts.Add(i, observedDict);
        }

        for (int i = 0; i < words.Length; i++)
        {
            if (allDict.ContainsKey(words[i]))
            {
                allDict[words[i]]++;
            }
            else
            {
                allDict.Add(words[i], 1);
            }
        }

        for (int i = 0; i < s.Length; i++)
        {
            var found = true;

            if (!observedDicts.TryGetValue(i, out Dictionary<string, int> observedDict) && i + allWordLength <= s.Length)
            {
                observedDict = observedDicts[i - wordLength];

                var headWord = s.Substring(i - wordLength, wordLength);
                if (observedDict[headWord] == 1)
                {
                    observedDict.Remove(headWord);
                }
                else
                {
                    observedDict[headWord]--;
                }

                var tailWord = s.Substring(i + allWordLength - wordLength, wordLength);
                if (observedDict.ContainsKey(tailWord))
                {
                    observedDict[tailWord]++;
                }
                else
                {
                    observedDict.Add(tailWord, 1);
                }

                observedDicts.Add(i, observedDict);
                observedDicts.Remove(i - wordLength);
            }


            if (observedDict != null)
            {
                foreach (var (key, value) in allDict)
                {
                    if (!observedDict.TryGetValue(key, out var observedVal) || observedVal != value)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                    ans.Add(i);
            }
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-window-substring/description/
    /// </summary>
    public static string MinWindow(string s, string t)
    {
        if (t.Length > s.Length) return string.Empty;

        if (s == t) return s;

        int[] observed = new int[128];
        int[] allChars = new int[128];

        for (int i = 0; i < t.Length; i++)
        {
            allChars[t[i]]++;
        }

        int start = -1, end = -1;
        var observedCount = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (allChars[s[i]] > 0)
            {
                if (t.Length == 1)
                    return t;

                start = i;
                end = start + 1;
                observedCount++;
                observed[s[i]]++;
                break;
            }
        }

        if (start == -1) return string.Empty;

        var nextMatches = new Queue<int>();
        var minStart = 0;
        var minEnd = int.MaxValue;

        while (end < s.Length)
        {
            // ADOBECODEBANC
            // ADOBEC
            // BECODEBA
            // CODEBA
            // BANC
            if (observedCount == t.Length)
            {
                observed[s[start]]--;

                if (end - start < minEnd - minStart)
                {
                    minEnd = end;
                    minStart = start;
                }

                if (observed[s[start]] < allChars[s[start]])
                    observedCount--;

                if (nextMatches.Count == 0) break;

                start = nextMatches.Dequeue();
            }
            else
            {
                if (allChars[s[end]] > 0)
                {
                    if (observed[s[end]] == 0 || observed[s[end]] < allChars[s[end]])
                    {
                        observedCount++;
                    }

                    observed[s[end]]++;

                    nextMatches.Enqueue(end);
                }
            }

            if (observedCount != t.Length)
                end++;
        }

        if (minEnd == int.MaxValue) return string.Empty;

        return s[minStart..(minEnd + 1)];
    }

    /// <summary>
    /// https://leetcode.com/problems/valid-sudoku
    /// </summary>
    public static bool IsValidSudoku(char[][] board)
    {
        board = [
            ['.', '.', '4', '.', '.', '.', '6', '3', '.'],
            ['.', '.', '.', '.', '.', '.', '.', '.', '.'],
            ['5', '.', '.', '.', '.', '.', '.', '9', '.'],
            ['.', '.', '.', '5', '6', '.', '.', '.', '.'],
            ['4', '.', '3', '.', '.', '.', '.', '.', '1'],
            ['.', '.', '.', '7', '.', '.', '.', '.', '.'],
            ['.', '.', '.', '5', '.', '.', '.', '.', '.'],
            ['.', '.', '.', '.', '.', '.', '.', '.', '.'],
            ['.', '.', '.', '.', '.', '.', '.', '.', '.']];


        var rowHashSets = new HashSet<char>[9];
        var columnHashSets = new HashSet<char>[9];
        var boxHashSets = new HashSet<char>[9];

        for (int i = 0; i < 9; i++)
        {
            rowHashSets[i] = [];
            columnHashSets[i] = [];
            boxHashSets[i] = [];
        }

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] != '.')
                {
                    if (!rowHashSets[i].Add(board[i][j]))
                    {
                        return false;
                    }

                    if (!columnHashSets[j].Add(board[i][j]))
                    {
                        return false;
                    }

                    if (!boxHashSets[3 * (i / 3) + j / 3].Add(board[i][j]))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    /// https://leetcode.com/problems/spiral-matrix
    /// </summary>
    public static IList<int> SpiralOrder(int[][] matrix)
    {
        var ans = new List<int>();
        var size = matrix.Length * matrix[0].Length;
        var direction = 0;
        var checkedMatrix = new int[matrix.Length, matrix[0].Length];
        int currRow = 0, currCol = 0;

        while (ans.Count < size)
        {
            if (direction == 0) // left to right
            {
                for (int i = currCol; i <= matrix[0].Length; i++)
                {
                    if (i < matrix[0].Length && checkedMatrix[currRow, i] != 1)
                    {
                        ans.Add(matrix[currRow][i]);
                        currCol = i;
                        checkedMatrix[currRow, i] = 1;
                    }
                    else
                    {
                        direction = 1;
                        if (currRow < matrix.Length - 1) currRow++;
                        break;
                    }
                }
            }

            if (direction == 1) // top to bottom
            {
                for (int j = currRow; j <= matrix.Length; j++)
                {
                    if (j < matrix.Length && checkedMatrix[j, currCol] != 1)
                    {
                        ans.Add(matrix[j][currCol]);
                        currRow = j;
                        checkedMatrix[j, currCol] = 1;
                    }
                    else
                    {
                        direction = 2;
                        if (currCol > 0) currCol--;
                        break;
                    }
                }
            }

            if (direction == 2) // right to left
            {
                for (int i = currCol; i >= -1; i--)
                {
                    if (i > -1 && checkedMatrix[currRow, i] != 1)
                    {
                        ans.Add(matrix[currRow][i]);
                        currCol = i;
                        checkedMatrix[currRow, i] = 1;
                    }
                    else
                    {
                        direction = 3;
                        if (currRow > 0) currRow--;
                        break;
                    }
                }
            }

            if (direction == 3) // bottom to top
            {
                for (int j = currRow; j >= -1; j--)
                {
                    if (j > -1 && checkedMatrix[j, currCol] != 1)
                    {
                        ans.Add(matrix[j][currCol]);
                        currRow = j;
                        checkedMatrix[j, currCol] = 1;
                    }
                    else
                    {
                        direction = 0;
                        if (currCol < matrix[0].Length) currCol++;
                        break;
                    }
                }
            }
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/rotate-image
    /// </summary>
    public static void Rotate(int[][] matrix)
    {
        // [1,2,3],
        // [4,5,6],
        // [7,8,9]

        if (matrix.Length == 1) return;

        var currRow = 0;

        while (currRow < matrix.Length / 2)
        {
            for (int j = currRow; j < matrix.Length - currRow - 1; j++)
            {
                var prev = matrix[currRow][j];

                for (int k = 0; k < 4; k++) // four sides
                {
                    switch (k)
                    {
                        case 0: // right
                            (prev, matrix[j][matrix.Length - 1 - currRow]) = (matrix[j][matrix.Length - 1 - currRow], prev);
                            break;
                        case 1: // bottom
                            (prev, matrix[matrix.Length - 1 - currRow][matrix.Length - 1 - j]) = (matrix[matrix.Length - 1 - currRow][matrix.Length - 1 - j], prev);
                            break;
                        case 2: // left
                            (prev, matrix[matrix.Length - 1 - j][currRow]) = (matrix[matrix.Length - 1 - j][currRow], prev);
                            break;
                        case 3: // top
                            (prev, matrix[currRow][j]) = (matrix[currRow][j], prev);
                            break;
                    }
                }
            }

            currRow++;
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/set-matrix-zeroes
    /// </summary>
    public static void SetZeroes(int[][] matrix)
    {
        var rowCols = new List<(int, int)>();

        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[0].Length; j++)
            {
                if (matrix[i][j] == 0)
                {
                    rowCols.Add((i, j));
                }
            }
        }

        for (int i = 0; i < rowCols.Count; i++)
        {
            for (int j = 0; j < matrix[0].Length; j++)
            {
                matrix[rowCols[i].Item1][j] = 0;
            }

            for (int j = 0; j < matrix.Length; j++)
            {
                matrix[j][rowCols[i].Item2] = 0;
            }
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/game-of-life
    /// </summary>
    public static void GameOfLife(int[][] board)
    {
        var flips = new List<(int, int)>();

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                var liveCells = 0;

                if (i > 0 && j > 0 && board[i - 1][j - 1] == 1) liveCells++;
                if (i > 0 && board[i - 1][j] == 1) liveCells++;
                if (i > 0 && j < board[0].Length - 1 && board[i - 1][j + 1] == 1) liveCells++;
                if (j > 0 && board[i][j - 1] == 1) liveCells++;
                if (j < board[0].Length - 1 && board[i][j + 1] == 1) liveCells++;
                if (j > 0 && i < board.Length - 1 && board[i + 1][j - 1] == 1) liveCells++;
                if (i < board.Length - 1 && board[i + 1][j] == 1) liveCells++;
                if (i < board.Length - 1 && j < board[0].Length - 1 && board[i + 1][j + 1] == 1) liveCells++;

                if (liveCells < 2 && board[i][j] == 1) flips.Add((i, j));
                else if (liveCells == 3 && board[i][j] == 0) flips.Add((i, j));
                else if (liveCells > 3 && board[i][j] == 1) flips.Add((i, j));
            }
        }

        for (int i = 0; i < flips.Count; i++)
        {
            board[flips[i].Item1][flips[i].Item2] = Math.Abs(board[flips[i].Item1][flips[i].Item2] - 1);
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/ransom-note
    /// </summary>
    public static bool CanConstruct(string ransomNote, string magazine)
    {
        var dict = new int[26];

        for (int i = 0; i < magazine.Length; i++)
        {
            dict[magazine[i] - 97]++;
        }

        for (int i = 0; i < ransomNote.Length; i++)
        {
            dict[ransomNote[i] - 97]--;

            if (dict[ransomNote[i] - 97] < 0) return false;
        }

        return true;
    }

    /// <summary>
    /// https://leetcode.com/problems/isomorphic-strings
    /// </summary>
    public static bool IsIsomorphic(string s, string t)
    {
        var dict = new int[128];
        var dict2 = new int[128];

        for (int i = 0; i < s.Length; i++)
        {
            if (dict[s[i]] != 0 && dict[s[i]] != t[i]
                || dict2[t[i]] != 0 && dict2[t[i]] != s[i])
            {
                return false;
            }
            else
            {
                dict[s[i]] = t[i];
                dict2[t[i]] = s[i];
            }
        }

        return true;
    }

    /// <summary>
    /// https://leetcode.com/problems/word-pattern
    /// </summary>
    public static bool WordPattern(string pattern, string s)
    {
        var dict = new string[128];
        var dict2 = new Dictionary<string, char>();

        var words = s.Split(' ');

        if (pattern.Length != words.Length)
            return false;

        for (int i = 0; i < pattern.Length; i++)
        {
            if (dict[pattern[i]] != null && dict[pattern[i]] != words[i])
            {
                return false;
            }
            else if (dict[pattern[i]] == null)
            {
                dict[pattern[i]] = words[i];
            }

            if (dict2.TryGetValue(words[i], out char c) && c != pattern[i])
                return false;

            if (c == default(char))
            {
                dict2[words[i]] = pattern[i];
            }
        }

        return true;
    }

    /// <summary>
    /// https://leetcode.com/problems/valid-anagram
    /// </summary>
    public static bool IsAnagram(string s, string t)
    {
        var dict1 = new int[128];
        var dict2 = new int[128];

        for (int i = 0; i < s.Length; i++)
        {
            dict1[s[i]]++;
        }

        for (int i = 0; i < t.Length; i++)
        {
            dict2[t[i]]++;
        }

        for (int i = 0; i < dict1.Length; i++)
        {
            if (dict1[i] != dict2[i])
                return false;
        }

        return true;
    }

    /// <summary>
    /// https://leetcode.com/problems/group-anagrams
    /// </summary>
    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        // var ans = new List<IList<string>>();

        // for (int k = 0; k < strs.Length; k++)
        // {
        //     var existing = false;

        //     foreach (var list in ans)
        //     {
        //         var dict1 = new int[26];
        //         var dict2 = new int[26];

        //         var s = list[0];
        //         var t = strs[k];

        //         if (s.Length != t.Length) continue;

        //         for (int i = 0; i < s.Length; i++)
        //         {
        //             dict1[s[i] - 'a']++;
        //         }

        //         for (int i = 0; i < t.Length; i++)
        //         {
        //             dict2[t[i] - 'a']++;
        //         }

        //         if (dict1.SequenceEqual(dict2))
        //         {
        //             existing = true;
        //             list.Add(strs[k]);
        //             break;
        //         }
        //     }

        //     if (!existing)
        //     {
        //         ans.Add([strs[k]]);
        //     }
        // }

        // return ans;

        var dict = new Dictionary<string, IList<string>>();

        for (int i = 0; i < strs.Length; i++)
        {
            Span<char> chars = strs[i].ToCharArray();
            chars.Sort();
            var sorted = new string(chars);

            if (dict.TryGetValue(sorted, out var list))
            {
                list.Add(strs[i]);
            }
            else
            {
                dict[sorted] = [strs[i]];
            }
        }

        return [.. dict.Values];
    }

    /// <summary>
    /// https://leetcode.com/problems/two-sum
    /// </summary>
    public static int[] TwoSum(int[] nums, int target)
    {
        var dict = new Dictionary<int, int>();
        var ans = new int[2];

        for (int i = 0; i < nums.Length; i++)
        {
            if (dict.TryGetValue(nums[i], out var index))
            {
                ans = [index, i];
                break;
            }
            else
            {
                dict[target - nums[i]] = i;
            }
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/happy-number
    /// </summary>
    public static bool IsHappy(int n)
    {
        HashSet<int> repeats = [];
        int number;

        repeats.Add(n);

        do
        {
            number = 0;

            while (n != 0)
            {
                number += n % 10 * (n % 10);

                n /= 10;
            }

            if (number == 1)
                return true;

            n = number;
        }
        while (repeats.Add(number));

        return false;
    }

    /// <summary>
    /// https://leetcode.com/problems/contains-duplicate-ii
    /// </summary>
    public static bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        var dict = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (dict.TryGetValue(nums[i], out var val) && Math.Abs(i - val) <= k)
            {
                return true;
            }
            else
            {
                dict[nums[i]] = i;
            }
        }

        return false;
    }

    /// <summary>
    /// https://leetcode.com/problems/longest-consecutive-sequence
    /// </summary>
    public static int LongestConsecutive(int[] nums)
    {
        // 100,4,200,1,3,2
        // 100 - 99, 101
        // 4 - 3, 5
        // 200 - 199, 201
        // 1 - 0, 2
        // 3 - 2, 4
        // 2 - 1, 2

        var hashSet = new HashSet<int>();

        for (int i = 0; i < nums.Length; i++)
        {
            hashSet.Add(nums[i]);
        }

        var checks = new HashSet<int>();
        var ans = 0;

        foreach (var item in hashSet)
        {
            if (checks.Contains(item))
                continue;

            var count = 1;
            var i = item + 1;
            checks.Add(item);

            while (hashSet.Contains(i))
            {
                count++;
                checks.Add(i);
                i++;
            }

            var j = item - 1;

            while (hashSet.Contains(j))
            {
                count++;
                checks.Add(j);
                j--;
            }

            if (count > ans)
                ans = count;
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/summary-ranges
    /// </summary>
    public static IList<string> SummaryRanges(int[] nums)
    {
        if (nums.Length == 0)
            return [];

        if (nums.Length == 1)
            return [nums[0].ToString()];

        var ans = new List<string>();
        int prev = nums[0];
        int start = 0;

        for (int i = 1; i < nums.Length; i++)
        {
            if (prev != nums[i] - 1)
            {
                if (start != i - 1)
                {
                    ans.Add($"{nums[start]}->{nums[i - 1]}");
                }
                else
                {
                    ans.Add($"{nums[start]}");
                }
                start = i;
            }

            if (i == nums.Length - 1)
            {
                if (start != i)
                {
                    ans.Add($"{nums[start]}->{nums[i]}");
                }
                else
                {
                    ans.Add($"{nums[start]}");
                }
            }

            prev = nums[i];
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/merge-intervals
    /// </summary>
    public static int[][] Merge(int[][] intervals)
    {
        if (intervals.Length == 1)
            return intervals;

        var ans = new List<int[]>();

        Array.Sort(intervals, (a, b) =>
        {
            return a[0] - b[0];
        });

        var curr = intervals[0];

        for (int i = 1; i < intervals.Length; i++)
        {
            if (intervals[i][0] <= curr[1])
            {
                curr[1] = Math.Max(curr[1], intervals[i][1]);
            }
            else
            {
                ans.Add(curr);
                curr = intervals[i];
            }

            if (i == intervals.Length - 1)
            {
                ans.Add(curr);
            }
        }

        return [.. ans];
    }

    /// <summary>
    /// https://leetcode.com/problems/insert-interval/
    /// </summary>
    public static int[][] Insert(int[][] intervals, int[] newInterval)
    {
        if (intervals.Length == 0)
            return [newInterval];

        var ans = new List<int[]>();
        var added = false;

        for (int i = 0; i < intervals.Length; i++)
        {
            if (!added && intervals[i][0] > newInterval[0])
            {
                ans.Add(newInterval);
                added = true;
            }
            ans.Add(intervals[i]);

            if (!added && i == intervals.Length - 1)
                ans.Add(newInterval);
        }

        var prev = ans[0];

        // [[1,2],[3,8],[6,7],[8,10],[12,16]]
        for (int i = 1; i < ans.Count; i++)
        {
            if (prev[1] >= ans[i][0])
            {
                prev[1] = Math.Max(prev[1], ans[i][1]);
                ans.RemoveAt(i);
                i--;
            }
            else
            {
                prev = ans[i];
            }
        }

        return [.. ans];
    }

    /// <summary>
    /// https://leetcode.com/problems/insert-interval/
    /// </summary>
    public static int[][] Insert2(int[][] intervals, int[] newInterval)
    {
        var ans = new List<int[]>();
        var i = 0;

        while (i < intervals.Length && intervals[i][1] < newInterval[0])
        {
            ans.Add(intervals[i]);
            i++;
        }

        while (i < intervals.Length && intervals[i][0] <= newInterval[1])
        {
            newInterval[0] = Math.Min(intervals[i][0], newInterval[0]);
            newInterval[1] = Math.Max(intervals[i][1], newInterval[1]);
            i++;
        }
        ans.Add(newInterval);

        while (i < intervals.Length)
        {
            ans.Add(intervals[i]);
            i++;
        }

        return [.. ans];
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons
    /// </summary>
    public static int FindMinArrowShots(int[][] points)
    {
        var ans = 1;

        if (points.Length == 1) return ans;

        Array.Sort(points, (a, b) =>
        {
            if (a[0] == b[0]) return 0;

            return a[0] > b[0] ? 1 : -1;
        });

        var currRange = points[0];

        // [1,6],[2,8],[7,12],[10,16]
        // [1,2],[3,4],[5,6],[7,8]
        // [1,10],[3,9],[4,11],[6,9],[6,7],[8,12],[9,12]
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i][0] > currRange[1])
            {
                currRange = points[i];
                ans++;
            }
            else
            {
                currRange[0] = points[i][0];
                currRange[1] = Math.Min(points[i][1], currRange[1]);
            }
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/valid-parentheses
    /// </summary>
    public static bool IsValid(string s)
    {
        if (s.Length == 1) return false;

        var stack = new Stack<char>();
        stack.Push(s[0]);

        var dict = new Dictionary<char, char>(3)
        {
            {')', '('},
            {'}', '{'},
            {']', '['}
        };

        // ([])
        // ((
        for (int i = 1; i < s.Length; i++)
        {
            if (dict.TryGetValue(s[i], out char val))
            {
                if (stack.Count == 0 || stack.Pop() != val)
                    return false;
            }
            else
            {
                stack.Push(s[i]);
            }
        }

        return stack.Count == 0;
    }

    /// <summary>
    /// https://leetcode.com/problems/simplify-path
    /// </summary>
    public static string SimplifyPath(string path)
    {
        const string slash = "/";

        var stack = new Stack<string>();
        var parts = path.Split(slash);

        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] == string.Empty || parts[i] == ".")
                continue;

            if (parts[i] == "..")
            {
                if (stack.Count > 0)
                    stack.Pop();
            }
            else
            {
                stack.Push(parts[i]);
            }
        }

        if (stack.Count == 0) return slash;

        var ans = new StringBuilder();

        while (stack.Count > 0)
        {
            var next = stack.Pop();

            ans.Insert(0, next);
            ans.Insert(0, slash);
        }

        return ans.ToString();
    }

    /// <summary>
    /// https://leetcode.com/problems/min-stack
    /// </summary>
    public class MinStack
    {
        private class Node
        {
            public Node Next { get; set; }
            public int Val { get; set; }
            public int Min { get; set; }
        }

        private Node head;

        public MinStack()
        {

        }

        public void Push(int val)
        {
            if (head == null)
            {
                head = new Node { Val = val, Min = val };
            }
            else
            {
                head = new Node { Val = val, Min = Math.Min(head.Min, val), Next = head };
            }
        }

        public void Pop()
        {
            head = head.Next;
        }

        public int Top()
        {
            return head.Val;
        }

        public int GetMin()
        {
            return head.Min;
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/evaluate-reverse-polish-notation
    /// </summary>
    public static int EvalRPN(string[] tokens)
    {
        var stack = new Stack<int>(tokens.Length);

        for (int i = 0; i < tokens.Length; i++)
        {
            switch (tokens[i])
            {
                case "+":
                    stack.Push(stack.Pop() + stack.Pop());
                    break;
                case "-":
                    var b = stack.Pop();
                    var a = stack.Pop();
                    stack.Push(a - b);
                    break;
                case "*":
                    stack.Push(stack.Pop() * stack.Pop());
                    break;
                case "/":
                    b = stack.Pop();
                    a = stack.Pop();
                    stack.Push(a / b);
                    break;
                default:
                    stack.Push(int.Parse(tokens[i]));
                    break;
            }
        }

        return stack.Pop();
    }

    /// <summary>
    /// https://leetcode.com/problems/linked-list-cycle
    /// </summary>
    public static bool HasCycle(ListNode head)
    {
        var set = new HashSet<ListNode>();

        while (head?.next != null)
        {
            if (!set.Add(head))
                return true;

            head = head.next;
        }

        return false;
    }

    /// <summary>
    /// https://leetcode.com/problems/add-two-numbers/description
    /// </summary>
    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        ListNode ans = null;
        ListNode next = null;
        var add = 0;

        while (l1 != null && l2 != null)
        {
            var sum = l1.val + l2.val + add;

            if (ans == null)
            {
                ans = new ListNode(sum % 10);
                next = ans;
            }
            else
            {
                next.next = new ListNode(sum % 10);
                next = next.next;
            }

            add = sum / 10;

            l1 = l1.next;
            l2 = l2.next;
        }

        var remain = l1 ?? l2;

        while (remain != null)
        {
            var sum = remain.val + add;
            add = sum / 10;

            next.next = new ListNode(sum % 10);
            next = next.next;

            remain = remain.next;
        }

        if (add > 0)
        {
            next.next = new ListNode(add);
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/merge-two-sorted-lists
    /// </summary>
    public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        ListNode first;

        if (list2 == null || list1?.val < list2.val)
        {
            first = list1;
            list1 = list1?.next;
        }
        else
        {
            first = list2;
            list2 = list2?.next;
        }

        var curr = first;

        while (list1 != null || list2 != null)
        {
            ListNode next = null;

            if (list2 == null || list1?.val < list2.val)
            {
                next = list1;
                list1 = list1?.next;
            }
            else
            {
                next = list2;
                list2 = list2?.next;
            }

            curr.next = next;
            curr = curr.next;
        }

        return first;
    }

    /// <summary>
    /// https://leetcode.com/problems/copy-list-with-random-pointer
    /// </summary>
    public static Node CopyRandomList(Node head)
    {
        if (head == null)
            return null;

        var newHead = new Node(head.val);
        var first = newHead;

        var firstHead = head;

        var i = 0;
        var nodeDict = new Dictionary<Node, int>
        {
            { head, i }
        };

        var nodeList = new List<Node>
        {
            newHead
        };

        head = head?.next;
        i++;

        while (head != null)
        {
            newHead.next = new Node(head.val);
            newHead = newHead.next;

            nodeList.Add(newHead);
            nodeDict.Add(head, i);

            head = head.next;
            i++;
        }

        head = firstHead;
        newHead = first;

        while (head != null)
        {
            if (head.random != null)
            {
                newHead.random = nodeList[nodeDict[head.random]];
            }

            newHead = newHead.next;
            head = head.next;
        }

        return first;
    }

    /// <summary>
    /// https://leetcode.com/problems/reverse-linked-list-ii
    /// </summary>
    public static ListNode ReverseBetween(ListNode head, int left, int right)
    {
        head = new ListNode(1);
        var next = head;
        left = 2;
        right = 4;

        for (int i = 1; i < 5; i++)
        {
            next.next = new ListNode(i + 1);
            next = next.next;
        }

        // head = [1,2,3,4,5], left = 2, right = 4
        var first = head;

        ListNode preLeft = null;

        var prev = head;
        var curr = head.next;
        var index = 0;

        while (index < left - 1 && curr != null)
        {
            preLeft = prev;
            prev = curr;
            curr = curr.next;
            index++;
        }

        var firstLeft = prev;

        while (index < right - 1 && curr != null)
        {
            var oldNext = curr.next;
            curr.next = prev;
            prev = curr;

            curr = oldNext;
            index++;
        }

        if (preLeft != null)
            preLeft.next = prev;
        else
            first = prev;

        firstLeft.next = curr;

        return first;
    }

    /// <summary>
    /// https://leetcode.com/problems/maximum-depth-of-binary-tree
    /// </summary>
    public static int MaxDepth(TreeNode root)
    {
        if (root == null)
            return 0;

        var stack = new Stack<(TreeNode, int)>();
        stack.Push((root, 1));

        var maxDepth = 0;

        while (stack.Count > 0)
        {
            var item = stack.Pop();
            var node = item.Item1;
            maxDepth = Math.Max(maxDepth, item.Item2);

            if (node.left != null)
            {
                stack.Push((node.left, item.Item2 + 1));
            }

            if (node.right != null)
            {
                stack.Push((node.right, item.Item2 + 1));
            }
        }

        return maxDepth;
    }

    /// <summary>
    /// https://leetcode.com/problems/same-tree
    /// </summary>
    public static bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p?.val != q?.val)
            return false;

        if (p == null && q == null)
            return true;

        return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }

    /// <summary>
    /// https://leetcode.com/problems/invert-binary-tree
    /// </summary>
    public static TreeNode InvertTree(TreeNode root)
    {
        if (root == null)
            return null;

        (root.left, root.right) = (root.right, root.left);

        InvertTree(root.left);
        InvertTree(root.right);

        return root;
    }

    /// <summary>
    /// https://leetcode.com/problems/symmetric-tree
    /// </summary>
    public static bool IsSymmetric(TreeNode root)
    {
        var stack1 = new Stack<TreeNode>();
        stack1.Push(root.left);

        var stack2 = new Stack<TreeNode>();
        stack2.Push(root.right);

        while (stack1.Count > 0 && stack2.Count > 0)
        {
            var node1 = stack1.Pop();
            var node2 = stack2.Pop();

            if (node1?.val != node2?.val)
                return false;

            if (node1 != null)
            {
                stack1.Push(node1.left);
                stack1.Push(node1.right);
            }

            if (node2 != null)
            {
                stack2.Push(node2.right);
                stack2.Push(node2.left);
            }
        }

        return true;
    }

    /// <summary>
    /// https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal
    /// </summary>
    public static TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        // preorder = [3,9,20,15,7], inorder = [9,3,15,20,7]

        TreeNode BuildTreeNode(ref int preOrderIndex, ReadOnlySpan<int> inorder)
        {
            if (preOrderIndex > preorder.Length - 1 || inorder.Length == 0)
                return null;

            var node = new TreeNode(preorder[preOrderIndex]);
            var mid = inorder.IndexOf(preorder[preOrderIndex]); ;

            preOrderIndex++;

            node.left = BuildTreeNode(ref preOrderIndex, inorder[0..mid]);
            node.right = BuildTreeNode(ref preOrderIndex, inorder[(mid + 1)..]);

            return node;
        }

        var preOrderIndex = 0;
        var firstNode = BuildTreeNode(ref preOrderIndex, inorder);

        return firstNode;
    }

    /// <summary>
    /// https://leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal
    /// </summary>
    public static TreeNode BuildTree2(int[] inorder, int[] postorder)
    {
        // inorder = [9,3,15,20,7], postorder = [9,15,7,20,3]

        TreeNode BuildTreeNode(ref int postOrderIndex, ReadOnlySpan<int> inorder)
        {
            if (postOrderIndex < 0 || inorder.Length == 0)
                return null;

            var node = new TreeNode(postorder[postOrderIndex]);
            var mid = inorder.IndexOf(node.val);

            postOrderIndex--;

            node.right = BuildTreeNode(ref postOrderIndex, inorder[(mid + 1)..]);
            node.left = BuildTreeNode(ref postOrderIndex, inorder[0..mid]);

            return node;
        }

        var postOrderIndex = postorder.Length - 1;
        var firstNode = BuildTreeNode(ref postOrderIndex, inorder);

        return firstNode;
    }

    /// <summary>
    /// https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii
    /// </summary>
    public static Node Connect(Node root)
    {
        if (root == null)
            return null;

        var queue = new Queue<Node>();

        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var prev = queue.Dequeue();
            var tmpQueue = new Queue<Node>();
            tmpQueue.Enqueue(prev);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                prev.next = node;
                prev = node;

                tmpQueue.Enqueue(node);
            }

            while (tmpQueue.Count > 0)
            {
                var node = tmpQueue.Dequeue();

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }
        }

        return root;
    }

    /// <summary>
    /// https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii
    /// </summary>
    public static Node Connect2(Node root)
    {
        if (root == null)
            return null;

        var curr = root;
        Node firstChild = null;
        Node prev = null;

        while (curr != null)
        {
            if (curr.left != null)
            {
                if (prev != null)
                {
                    prev.next = curr.left;
                }
                prev = curr.left;

                firstChild ??= prev;
            }

            if (curr.right != null)
            {
                if (prev != null)
                {
                    prev.next = curr.right;
                }
                prev = curr.right;

                firstChild ??= prev;
            }

            if (curr.next != null)
            {
                curr = curr.next;
            }
            else
            {
                curr = firstChild;
                prev = null;
                firstChild = null;
            }
        }

        return root;
    }

    /// <summary>
    /// https://leetcode.com/problems/flatten-binary-tree-to-linked-list
    /// </summary>
    public static void Flatten(TreeNode root)
    {
        if (root == null)
            return;

        Flatten(root.left);
        Flatten(root.right);

        if (root.left != null)
        {
            var tmp = root.right;
            root.right = root.left;

            var next = root.right;
            while (next.right != null)
            {
                next = next.right;
            }
            next.right = tmp;

            root.left = null;
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/path-sum
    /// </summary>
    public static bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root == null)
            return false;

        targetSum -= root.val;

        if (targetSum == 0 && root.left == null && root.right == null)
            return true;

        return HasPathSum(root.left, targetSum) || HasPathSum(root.right, targetSum);
    }

    /// <summary>
    /// https://leetcode.com/problems/sum-root-to-leaf-numbers
    /// </summary>
    public static int SumNumbers(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        var sum = 0;

        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var count = queue.Count;

            for (int i = 0; i < count; i++)
            {
                var node = queue.Dequeue();

                if (node.left != null)
                {
                    node.left.val += node.val * 10;
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    node.right.val += node.val * 10;
                    queue.Enqueue(node.right);
                }

                if (node.left == null && node.right == null) sum += node.val;
            }
        }

        return sum;
    }

    /// <summary>
    /// https://leetcode.com/problems/binary-tree-right-side-view
    /// </summary>
    public static IList<int> RightSideView(TreeNode root)
    {
        if (root == null) return [];

        var queue = new Queue<TreeNode>();
        var ans = new List<int>();

        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var count = queue.Count;

            for (int i = 0; i < count; i++)
            {
                var node = queue.Dequeue();

                if (i == count - 1) ans.Add(node.val);

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/average-of-levels-in-binary-tree
    /// </summary>
    public static IList<double> AverageOfLevels(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        var ans = new List<double>();

        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var count = queue.Count;
            long sum = 0;

            for (int i = 0; i < count; i++)
            {
                var node = queue.Dequeue();

                sum += node.val;

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }

            ans.Add((double)sum / count);
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/binary-tree-level-order-traversal
    /// </summary>
    public static IList<IList<int>> LevelOrder(TreeNode root)
    {
        var ans = new List<IList<int>>();

        if (root == null) return ans;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var count = queue.Count;
            var list = new List<int>(count);

            for (int i = 0; i < count; i++)
            {
                var node = queue.Dequeue();

                list.Add(node.val);

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }

            ans.Add(list);
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal
    /// </summary>
    public static IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        var ans = new List<IList<int>>();

        if (root == null) return ans;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        var leftToRight = true;

        while (queue.Count > 0)
        {
            var count = queue.Count;
            var list = new List<int>(count);

            for (int i = 0; i < count; i++)
            {
                var node = queue.Dequeue();

                list.Add(node.val);

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }

            if (!leftToRight) list.Reverse();

            ans.Add(list);
            leftToRight = !leftToRight;
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-absolute-difference-in-bst
    /// </summary>
    public static int GetMinimumDifference(TreeNode root)
    {
        TreeNode prev = null;
        var min = int.MaxValue;

        void DFS(TreeNode root)
        {
            if (root == null) return;

            DFS(root.left);

            if (prev != null)
                min = Math.Min(min, root.val - prev.val);

            prev = root;

            DFS(root.right);
        }

        DFS(root);

        return min;
    }

    /// <summary>
    /// https://leetcode.com/problems/kth-smallest-element-in-a-bst
    /// </summary>
    public static int KthSmallest(TreeNode root, int k)
    {
        int count = 0;
        TreeNode node = null;

        void DFS(TreeNode root)
        {
            if (root == null || node != null)
            {
                return;
            }

            DFS(root.left);

            count++;

            if (count == k)
            {
                node = root;
                return;
            }

            DFS(root.right);
        }

        DFS(root);

        return node.val;
    }

    /// <summary>
    /// https://leetcode.com/problems/validate-binary-search-tree
    /// </summary>
    public static bool IsValidBST(TreeNode root)
    {
        TreeNode prev = null;
        var ans = true;

        void DFS(TreeNode root)
        {
            if (root == null || !ans) return;

            DFS(root.left);

            if (prev != null && root.val <= prev.val)
                ans = false;

            prev = root;

            DFS(root.right);
        }

        DFS(root);

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/number-of-islands
    /// </summary>
    public static int NumIslands(char[][] grid)
    {
        var count = 0;

        int CheckSurrounding(int i, int j)
        {
            var found = grid[i][j] - '0';

            if (found == 1)
            {
                grid[i][j] = '2';

                if (i > 0) found = Math.Max(found, CheckSurrounding(i - 1, j));
                if (j > 0) found = Math.Max(found, CheckSurrounding(i, j - 1));
                if (i < grid.Length - 1) found = Math.Max(found, CheckSurrounding(i + 1, j));
                if (j < grid[0].Length - 1) found = Math.Max(found, CheckSurrounding(i, j + 1));
            }

            return found;
        }

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '1')
                {
                    CheckSurrounding(i, j);
                    count++;
                }
            }
        }

        return count;
    }

    /// <summary>
    /// https://leetcode.com/problems/surrounded-regions
    /// </summary>
    public static void Solve(char[][] board)
    {
        void UpdateSurrounding(int i, int j)
        {
            if (board[i][j] == 'O')
            {
                board[i][j] = 'T';

                if (i > 0) UpdateSurrounding(i - 1, j);
                if (j > 0) UpdateSurrounding(i, j - 1);
                if (i < board.Length - 1) UpdateSurrounding(i + 1, j);
                if (j < board[0].Length - 1) UpdateSurrounding(i, j + 1);
            }
        }

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i][0] == 'O') UpdateSurrounding(i, 0);
            if (board[i][board[0].Length - 1] == 'O') UpdateSurrounding(i, board[0].Length - 1);
        }

        for (int j = 0; j < board[0].Length; j++)
        {
            if (board[0][j] == 'O') UpdateSurrounding(0, j);
            if (board[^1][j] == 'O') UpdateSurrounding(board.Length - 1, j);
        }

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] == 'O') board[i][j] = 'X';
                else if (board[i][j] == 'T') board[i][j] = 'O';
            }
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/clone-graph
    /// </summary>
    public static Node CloneGraph(Node node)
    {
        if (node == null) return null;

        var queue = new Queue<Node>();
        var dict = new Dictionary<int, Node>();
        var visited = new HashSet<int>();
        Node firstClone = null;

        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var curr = queue.Dequeue();

            if (visited.Add(curr.val))
            {
                if (!dict.TryGetValue(curr.val, out var clonedCurr))
                {
                    clonedCurr = new Node(curr.val);
                    dict.Add(curr.val, clonedCurr);
                }
                firstClone ??= clonedCurr;

                foreach (var neighbor in curr.neighbors)
                {
                    if (!dict.TryGetValue(neighbor.val, out var cloned))
                    {
                        cloned = new Node(neighbor.val);
                        dict.Add(neighbor.val, cloned);
                    }

                    clonedCurr.neighbors.Add(cloned);

                    queue.Enqueue(neighbor);
                }
            }
        }

        return firstClone;
    }

    /// <summary>
    /// https://leetcode.com/problems/evaluate-division
    /// </summary>
    public static double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
    {
        var ans = new double[queries.Count];
        var dict = new Dictionary<string, List<(string, double)>>();

        for (int i = 0; i < equations.Count; i++)
        {
            if (!dict.TryGetValue(equations[i][0], out var adjacentList))
            {
                adjacentList = [];
                dict[equations[i][0]] = adjacentList;
            }
            adjacentList.Add((equations[i][1], values[i]));

            if (!dict.TryGetValue(equations[i][1], out var adjacentList2))
            {
                adjacentList2 = [];
                dict[equations[i][1]] = adjacentList2;
            }
            adjacentList2.Add((equations[i][0], 1 / values[i]));
        }

        for (int i = 0; i < queries.Count; i++)
        {
            if (!dict.ContainsKey(queries[i][0]) || !dict.ContainsKey(queries[i][1]))
            {
                ans[i] = -1;
                continue;
            }

            var queue = new Queue<(string, double)>();
            var visited = new HashSet<string>();
            double? queryWeight = null;

            queue.Enqueue((queries[i][0], 1));

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Item1 == queries[i][1])
                {
                    queryWeight = node.Item2;
                    break;
                }

                visited.Add(node.Item1);

                for (int j = 0; j < dict[node.Item1].Count; j++)
                {
                    var neighbour = dict[node.Item1][j];

                    if (visited.Contains(neighbour.Item1)) continue;

                    var weight = dict[node.Item1].First(item => item.Item1 == neighbour.Item1).Item2;

                    queue.Enqueue((neighbour.Item1, node.Item2 * weight));
                }
            }

            ans[i] = queryWeight != null ? queryWeight.Value : -1;
        }

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/course-schedule
    /// </summary>
    public static bool CanFinish(int numCourses, int[][] prerequisites)
    {
        if (prerequisites.Length == 0) return true;

        var adjacents = new List<List<int>>(numCourses);

        for (int i = 0; i < numCourses; i++)
        {
            adjacents.Add([]);
        }

        foreach (var pre in prerequisites)
        {
            if (pre[0] == pre[1]) return false;

            adjacents[pre[0]].Add(pre[1]);
        }

        var visited = new bool[numCourses];

        for (int i = 0; i < numCourses; i++)
        {
            if (!visited[i] && adjacents[i].Count > 0)
            {
                var queue = new Queue<(int, List<int>)>();
                queue.Enqueue((i, []));

                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();

                    visited[node.Item1] = true;

                    foreach (var neighbour in adjacents[node.Item1])
                    {
                        if (node.Item2.Contains(neighbour)) return false;

                        if (visited[neighbour]) continue;

                        queue.Enqueue((neighbour, [.. node.Item2, node.Item1]));
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    /// https://leetcode.com/problems/course-schedule
    /// </summary>
    public static bool CanFinish2(int numCourses, int[][] prerequisites)
    {
        var adjacents = new List<int>[numCourses];
        var states = new int[numCourses];

        for (int i = 0; i < numCourses; i++)
        {
            adjacents[i] = [];
        }

        foreach (var preq in prerequisites)
        {
            if (preq[0] == preq[1]) return false;

            adjacents[preq[0]].Add(preq[1]);
        }

        bool DFS(int node)
        {
            if (adjacents[node].Count == 0)
            {
                states[node] = 2;

                return true;
            }

            states[node] = 1;

            foreach (var neighbour in adjacents[node])
            {
                if (states[neighbour] == 2) continue;

                if (states[neighbour] == 1) return false;

                if (!DFS(neighbour)) return false;
            }

            states[node] = 2;

            return true;
        }

        for (int i = 0; i < numCourses; i++)
        {
            if (!DFS(i)) return false;
        }

        return true;
    }

    /// <summary>
    /// https://leetcode.com/problems/course-schedule-ii
    /// </summary>
    public static int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        var adjacents = new List<int>[numCourses];
        var states = new int[numCourses];

        for (int i = 0; i < numCourses; i++)
        {
            adjacents[i] = [];
        }

        foreach (var preq in prerequisites)
        {
            if (preq[0] == preq[1]) return [];

            adjacents[preq[0]].Add(preq[1]);
        }

        var steps = new List<int>();

        bool DFS(int node)
        {
            if (adjacents[node].Count == 0)
            {
                states[node] = 2;
                steps.Add(node);

                return true;
            }

            states[node] = 1;

            foreach (var neighbour in adjacents[node])
            {
                if (states[neighbour] == 1) return false;

                if (states[neighbour] == 2) continue;

                if (!DFS(neighbour)) return false;
            }

            states[node] = 2;
            steps.Add(node);

            return true;
        }

        for (int i = 0; i < numCourses; i++)
        {
            if (states[i] != 0) continue;

            if (!DFS(i)) return [];
        }

        for (int i = 0; i < states.Length; i++)
        {
            if (states[i] == 0) steps.Add(i);
        }

        return [.. steps];
    }

    /// <summary>
    /// https://leetcode.com/problems/snakes-and-ladders
    /// </summary>
    public static int SnakesAndLadders(int[][] board)
    {
        var n = board.Length * board.Length + 1;
        var adjacents = new List<int>[n];
        var leftToRight = true;
        var cells = new int[n];
        var cellIndex = 1;
        var j = 0;

        for (int i = 1; i < n; i++)
        {
            adjacents[i] = [];
        }

        for (int i = board.Length - 1; i >= 0; i--)
        {
            while (j >= 0 && j < board.Length)
            {
                cells[cellIndex] = board[i][j];

                if (leftToRight)
                {
                    j++;
                }
                else
                {
                    j--;
                }

                cellIndex++;
            }

            j = Math.Max(j, 0);
            j = Math.Min(j, board.Length - 1);
            leftToRight = !leftToRight;
        }

        for (int i = 1; i < cells.Length; i++)
        {
            bool furthestReached = false;

            for (var k = 6; k > 0; k--)
            {
                if (i + k >= cells.Length) continue;

                if (cells[i + k] == -1)
                {
                    if (!furthestReached) adjacents[i].Add(i + k);

                    furthestReached = true;
                }
                else
                {
                    adjacents[i].Add(cells[i + k]);
                }
            }
        }

        var queue = new Queue<(int, int)>();
        var visited = new bool[n];
        var node = (1, 0);
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var queueLength = queue.Count;

            for (int i = 0; i < queueLength; i++)
            {
                node = queue.Dequeue();

                visited[node.Item1] = true;

                foreach (var neighbour in adjacents[node.Item1])
                {
                    if (neighbour == n - 1) return node.Item2 + 1;

                    if (visited[neighbour]) continue;

                    queue.Enqueue((neighbour, node.Item2 + 1));
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// https://leetcode.com/problems/minimum-genetic-mutation/
    /// </summary>
    public static int MinMutation(string startGene, string endGene, string[] bank)
    {
        if (!bank.Contains(endGene)) return -1;

        var queue = new Queue<(string, int)>();
        queue.Enqueue((startGene, 0));
        var visited = new HashSet<string>();

        static bool IsNeighbour(string item1, string item2)
        {
            var diff = 0;

            for (int i = 0; i < item1.Length; i++)
            {
                if (item1[i] != item2[i]) diff++;

                if (diff > 1) return false;
            }

            return diff == 1;
        }

        while (queue.Count > 0)
        {
            var queueLength = queue.Count;

            for (int i = 0; i < queueLength; i++)
            {
                var node = queue.Dequeue();

                visited.Add(node.Item1);

                foreach (var neighbour in bank)
                {
                    if (visited.Contains(neighbour)) continue;

                    if (!IsNeighbour(neighbour, node.Item1)) continue;

                    if (neighbour == endGene) return node.Item2 + 1;

                    queue.Enqueue((neighbour, node.Item2 + 1));
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// https://leetcode.com/problems/implement-trie-prefix-tree
    /// </summary>
    public class Trie
    {
        private class Node()
        {
            public Dictionary<char, Node> Children = [];

            public int Weight;

            public int ChildWeights;
        }

        private readonly Node root = new();

        public Trie() { }

        private void Insert(string word, Node node, int index)
        {
            node.Weight++;

            if (index >= word.Length) return;

            if (!node.Children.TryGetValue(word[index], out Node child))
            {
                child = new Node();
                node.Children.Add(word[index], child);
            }
            node.ChildWeights++;

            Insert(word, child, index + 1);
        }

        public void Insert(string word)
        {
            Insert(word, root, 0);
        }

        private bool StartsWith(string prefix, out Node node)
        {
            int index = 0;
            node = root;

            while (index < prefix.Length)
            {
                if (!node.Children.TryGetValue(prefix[index], out node))
                    return false;

                index++;
            }

            return true;
        }

        public bool Search(string word)
        {
            var result = StartsWith(word, out Node node);

            return result && node.Weight > node.ChildWeights;
        }

        public bool StartsWith(string prefix)
        {
            return StartsWith(prefix, out _);
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/design-add-and-search-words-data-structure
    /// </summary>
    public class WordDictionary
    {
        private readonly Node root = new();

        private class Node()
        {
            public Dictionary<char, Node> Children = [];

            public bool IsLeaf;
        }

        public WordDictionary() { }

        public void AddWord(string word)
        {
            var node = root;

            foreach (var c in word)
            {
                if (!node.Children.TryGetValue(c, out var child))
                {
                    child = new Node();
                    node.Children.Add(c, child);
                }

                node = child;
            }

            node.IsLeaf = true;
        }

        public bool Search(string word)
        {
            static bool Search(string word, Node node, int index)
            {
                if (index >= word.Length) return node.IsLeaf;

                var found = false;

                if (word[index] != '.')
                {
                    if (!node.Children.TryGetValue(word[index], out node))
                        return false;

                    found = Search(word, node, index + 1);
                }
                else
                {
                    foreach (var child in node.Children)
                    {
                        if (!found)
                            found |= Search(word, child.Value, index + 1);
                    }
                }

                return found;
            }

            return Search(word, root, 0);
        }
    }

    /// <summary>
    /// https://leetcode.com/problems/letter-combinations-of-a-phone-number/
    /// </summary>
    public static IList<string> LetterCombinations(string digits)
    {
        var dict = new Dictionary<char, string>
        {
            {'2', "abc"},
            {'3', "def"},
            {'4', "ghi"},
            {'5', "jkl"},
            {'6', "mno"},
            {'7', "pqrs"},
            {'8', "tuv"},
            {'9', "wxyz"},
        };

        var output = new List<string>();
        var strBuilder = new StringBuilder();

        void BackTrack(int index)
        {
            if (index == digits.Length)
            {
                output.Add(strBuilder.ToString());
                return;
            }

            foreach (var c in dict[digits[index]])
            {
                strBuilder.Append(c);
                BackTrack(index + 1);
                strBuilder.Remove(strBuilder.Length - 1, 1);
            }
        }

        if (!string.IsNullOrEmpty(digits))
            BackTrack(0);

        return output;
    }

    /// <summary>
    /// https://leetcode.com/problems/combinations
    /// </summary>
    public static IList<IList<int>> Combine(int n, int k)
    {
        var ans = new List<IList<int>>();
        List<int> list = [];

        void BackTrack(int index)
        {
            if (list.Count == k)
            {
                ans.Add([.. list]);
                return;
            }

            for (int i = index; i <= n; i++)
            {
                list.Add(i);
                BackTrack(i + 1);
                list.RemoveAt(list.Count - 1);
            }
        }

        BackTrack(1);

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/permutations
    /// </summary>
    public static IList<IList<int>> Permute(int[] nums)
    {
        var ans = new List<IList<int>>();
        var visited = new int[nums.Length];
        var list = new List<int>();

        void BackTrack(int level)
        {
            if (list.Count == nums.Length)
            {
                ans.Add([.. list]);
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (visited[i] == 1) continue;

                list.Add(nums[i]);
                visited[i] = 1;

                BackTrack(level + 1);

                list.RemoveAt(list.Count - 1);
                visited[i] = 0;
            }
        }

        BackTrack(0);

        return ans;
    }

    /// <summary>
    /// https://leetcode.com/problems/combination-sum
    /// </summary>
    public static IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        var ans = new List<IList<int>>();
        var list = new List<int>();
        int sum = 0;

        void BackTrack(int level)
        {
            if (sum >= target)
            {
                if (sum == target)
                {
                    ans.Add([.. list]);
                }
                return;
            }

            for (int i = level; i < candidates.Length; i++)
            {
                sum += candidates[i];
                list.Add(candidates[i]);

                BackTrack(i);

                sum -= candidates[i];
                list.RemoveAt(list.Count - 1);
            }
        }

        BackTrack(0);

        return ans;
    }
}
