using ChipsPlayGround;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChipsTest
{
    [TestClass]
    public class SolutionTest
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
            expected.Should().BeEquivalentTo(Solution.Intersect(nums1, nums2));
        }
    }
}
