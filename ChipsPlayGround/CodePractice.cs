using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using CommandLine;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;

namespace CodilityPractice;

public class Solutions
{
    // Write a function Solution(string[] transactions, decimal dailyLimit) that processes a list of transaction records 
    // in the format "AccountID,Amount,YYYY-MM-DD". 
    // The function should return a list of Account IDs that exceeded the specified dailyLimit on any single calendar day. 
    // Malformed lines should be skipped safely without crashing.
    public class SolutionA
    {
        private record TransactionRecord(int AccountID, decimal Amount, DateTime TransactionDate);

        public static List<int> ProcessTransactions(string[] transactions, decimal dailyLimit)
        {
            if (transactions == null || transactions.Length == 0 || dailyLimit <= 0) return [];

            var accountAmountsOnDay = new Dictionary<(int AccountId, DateTime DateTime), decimal>();
            var exceededAccountIds = new HashSet<int>();

            foreach (var transaction in transactions)
            {
                bool isValid = ValidateTransaction(transaction);

                if (!isValid)
                {
                    Console.WriteLine($"Invalid line: {transaction}");
                    continue;
                }

                TransactionRecord record = ParseRecord(transaction);

                // Remove this shortcut if it's desirable to keep a complete record of `accountAmountsOnDay`
                if (exceededAccountIds.Contains(record.AccountID)) continue;

                decimal totalAccountAmountOnDay = AddAccountAmountOnDay(accountAmountsOnDay, record);

                if (totalAccountAmountOnDay > dailyLimit)
                {
                    exceededAccountIds.Add(record.AccountID);
                }
            }

            return exceededAccountIds.ToList();
        }

        private static decimal AddAccountAmountOnDay(
            Dictionary<(int AccountId, DateTime DateTime), decimal> accountAmountsOnDay,
            TransactionRecord record)
        {
            if (accountAmountsOnDay.ContainsKey((record.AccountID, record.TransactionDate)))
            {
                accountAmountsOnDay[(record.AccountID, record.TransactionDate)] += record.Amount;
            }
            else
            {
                accountAmountsOnDay[(record.AccountID, record.TransactionDate)] = record.Amount;
            }

            return accountAmountsOnDay[(record.AccountID, record.TransactionDate)];
        }

        private static TransactionRecord ParseRecord(string transaction)
        {
            var parts = transaction.Split(',');
            var accountId = int.Parse(parts[0]);
            var amount = decimal.Parse(parts[1]);
            var dateTime = DateTime.Parse(parts[2]);

            return new TransactionRecord(accountId, amount, dateTime);
        }

        private static bool ValidateTransaction(string transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction)) return false;

            var parts = transaction.Split(',');

            if (parts.Length != 3) return false;

            if (!int.TryParse(parts[0], out int _)) return false;

            if (!decimal.TryParse(parts[1], out decimal _)) return false;

            if (!DateTime.TryParseExact(
                parts[2], "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime _))
            {
                return false;
            }

            return true;
        }
    }

    // You are building a core ingestion service for a sports betting engine. 
    // The system receives a stream of live match updates formatted as strings: "MatchID,MarketType,Odds,TimestampISO".
    // Write a function ProcessOddsFeed(string[] feed, decimal maxAllowedOdds, int timeWindowSeconds) 
    // that processes incoming live odds and identifies volatile markets.
    // A market is considered volatile if:
    // The odds for a specific (MatchID, MarketType) exceed maxAllowedOdds at any point, OR
    // The odds for a specific (MatchID, MarketType) change more than 3 times within any rolling window of timeWindowSeconds.
    // Malformed lines, non-positive odds, or out-of-order timestamps should be safely ignored or handled without crashing. 
    // Return a sorted list of unique volatile market identifiers in the format "MatchID:MarketType".
    public class SolutionB
    {
        private record Feed(string MatchID, string MarketType, decimal Odds, DateTimeOffset TimestampISO);

        public static List<string> ProcessOddsFeed(string[] feed, decimal maxAllowedOdds, int timeWindowSeconds)
        {
            if (feed == null || feed.Length == 0) return [];

            var feedTimestampDict
                = new Dictionary<(string MatchID, string MarketType), List<(decimal Odds, DateTimeOffset Timestamp)>>();
            var volatileMarkets = new HashSet<string>();

            foreach (var entry in feed)
            {
                if (!TryParse(entry, out Feed record)) continue;

                var marketIdentifier = $"{record.MatchID}:{record.MarketType}";

                if (volatileMarkets.Contains(marketIdentifier)) continue;

                if (feedTimestampDict.ContainsKey((record.MatchID, record.MarketType)))
                {
                    var feedTimestamps = feedTimestampDict[(record.MatchID, record.MarketType)];

                    if (feedTimestamps.Last().Timestamp >= record.TimestampISO
                        || feedTimestamps.Last().Odds == record.Odds)
                    {
                        continue;
                    }
                    else if (feedTimestamps.Count < 4)
                    {
                        feedTimestamps.Add((record.Odds, record.TimestampISO));
                    }
                    else
                    {
                        // Check if the first event is within the rolling window of the new event
                        if (record.TimestampISO.AddSeconds(-timeWindowSeconds) <= feedTimestamps.First().Timestamp)
                        {
                            volatileMarkets.Add(marketIdentifier);
                        }
                        else
                        {
                            feedTimestamps.RemoveAt(0);
                            feedTimestamps.Add((record.Odds, record.TimestampISO));
                        }
                    }
                }
                else
                {
                    feedTimestampDict[(record.MatchID, record.MarketType)] = [(record.Odds, record.TimestampISO)];
                }

                if (record.Odds > maxAllowedOdds) volatileMarkets.Add(marketIdentifier);
            }

            return volatileMarkets.OrderBy(x => x).ToList();
        }

        private static bool TryParse(string entry, out Feed record)
        {
            record = null;

            if (string.IsNullOrWhiteSpace(entry)) return false;

            // string format: "MatchID,MarketType,Odds,TimestampISO"
            var parts = entry.Split(',');
        
            if (parts.Length != 4) return false;

            var matchID = parts[0];
            var marketType = parts[1];

            if (string.IsNullOrWhiteSpace(matchID)) return false;
            if (string.IsNullOrWhiteSpace(marketType)) return false;
            if (!decimal.TryParse(parts[2], out var odds)) return false;
            if (odds <= 0) return false;
            if (!DateTimeOffset.TryParse(parts[3], out var dateTime)) return false;

            record = new Feed(matchID, marketType, odds, dateTime);
            
            return true;
        }
    }
}