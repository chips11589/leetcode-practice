using ChipsPlayGround;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ChipsTest
{
    [TestClass]
    public class SolutionTests
    {
        [TestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new int[] { 5, 6, 7, 1, 2, 3, 4 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 2, new int[] { 6, 7, 1, 2, 3, 4, 5 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 1, new int[] { 7, 1, 2, 3, 4, 5, 6 })]
        [DataRow(new int[] { -1, -100, 3, 99 }, 2, new int[] { 3, 99, -1, -100 })]
        [DataRow(new int[] { 1 }, 0, new int[] { 1 })]
        [DataRow(new int[] { 1 }, 1, new int[] { 1 })]
        [DataRow(new int[] { 1, 2 }, 1, new int[] { 2, 1 })]
        [DataRow(new int[] { 1, 2 }, 2, new int[] { 1, 2 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6 }, 2, new int[] { 5, 6, 1, 2, 3, 4 })]
        public void Rotate(int[] input, int k, int[] expected)
        {
            Solution.Rotate(input, k);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], input[i]);
            }
        }

        [TestMethod]
        [DataRow(new int[] { 7, 1, 5, 3, 6, 4 }, 7)]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, 4)]
        [DataRow(new int[] { 7, 6, 4, 3, 1 }, 0)]
        [DataRow(new int[] { 6, 1, 3, 2, 4, 7 }, 7)]
        [DataRow(new int[] { 2, 4, 1 }, 2)]
        [DataRow(new int[] { 2, 1, 2, 0, 1 }, 2)]
        [DataRow(new int[] { 1, 9, 6, 9, 1, 7, 1, 1, 5, 9, 9, 9 }, 25)]
        public void MaxProfit(int[] prices, int profit)
        {
            Assert.AreEqual(profit, Solution.MaxProfit(prices));
        }

        [TestMethod]
        [DataRow(new int[] { 1, 2, 2, 1 }, new int[] { 2, 2 }, new int[] { 2, 2 })]
        [DataRow(new int[] { 4, 9, 5 }, new int[] { 9, 4, 9, 8, 4 }, new int[] { 4, 9 })]
        public void Intersect(int[] nums1, int[] nums2, int[] expected)
        {
            Solution.Intersect(nums1, nums2).Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        [DataRow(new int[] { 1, 3 }, new int[] { 2 }, 2)]
        [DataRow(new int[] { 1, 2 }, new int[] { 3, 4 }, 2.5)]
        [DataRow(new int[] { 3, 5, 10, 11, 17 }, new int[] { 9, 13, 20, 21, 23, 27 }, 13)]
        [DataRow(new int[] { 2, 3, 5, 8 }, new int[] { 10, 12, 14, 16, 18, 20 }, 11)]
        public void FindMedianSortedArrays(int[] nums1, int[] nums2, double expected)
        {
            Solution.FindMedianSortedArrays(nums1, nums2).Should().Be(expected);
        }

        [TestMethod]
        public void CountkSpikes()
        {
            //Solution.CountkSpikes(new List<int> { 1, 3, 2, 5, 4 }, 1);
            //Solution.CountkSpikes(new List<int> { 1,2,8,3,7,5,4 }, 2);
            Solution.CountkSpikes(new List<int> { 1, 2, 7, 3, 8, 5, 4 }, 2);
        }

        [TestMethod]
        public void FindMaxProducts()
        {
            //Solution.FindMaxProducts(new List<int> { 2, 9, 4, 7, 5, 2 });
            Solution.FindMaxProducts(new List<int> { 25, 26, 45, 22, 31, 47, 29, 47, 2, 25, 25 });
        }

        [TestMethod]
        [DataRow(new string[] { "dd", "aa", "bb", "dd", "aa", "dd", "bb", "dd", "aa", "cc", "bb", "cc", "dd", "cc" })]
        public void LongestPalindrome(string[] words)
        {
            Solution.LongestPalindrome(words).Should().Be(22);
        }

        [TestMethod]
        [DataRow(new int[] { 1, 20, 10, 1, 12, 40, 10, 8 }, 69)]
        [DataRow(new int[] { 3, 2, 3, 4, 12, 4, 4, 20 }, 38)]
        [DataRow(new int[] { 3, 12, 3, 4, 12, 4, 4, 20 }, 44)]
        public void FindLargestAmountToRob(int[] input, int expected)
        {
            Solution.FindLargestAmountToRob(input).Should().Be(expected);
        }
    }
}
