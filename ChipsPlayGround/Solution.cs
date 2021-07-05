using System;

namespace ChipsPlayGround
{
    public class Solution
    {
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
    }
}
