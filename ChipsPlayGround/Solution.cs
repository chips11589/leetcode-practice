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
                                discountedPrice -= (discountedPrice * discountLookup[products[i][j]].Item2 / 100);
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
                        && (i - (balanceCount + 1) * 2) is int oppositeIndex && oppositeIndex > -1 && s[i] == s[oppositeIndex])
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
                output += (charCounts[i].count * (charCounts[i].count + 1) / 2);
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
    }
}
