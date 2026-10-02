using System;
using System.Collections.Generic;
using CodilityPractice;
using Xunit;

namespace CodingTest.SolutionTests;

public class SolutionBTests
{
    [Fact]
    public void ProcessOddsFeed_ReturnsMarketsOverTheOddsLimitInSortedUniqueOrder()
    {
        string[] feed =
        {
            "MatchB,Moneyline,6.0,2024-01-01T00:00:00Z",
            "MatchA,Total,5.1,2024-01-01T00:00:00Z",
            "MatchB,Moneyline,7.0,2024-01-01T00:00:01Z",
            "MatchC,Spread,5.0,2024-01-01T00:00:00Z"
        };

        List<string> result = Solutions.SolutionB.ProcessOddsFeed(feed, 5m, 30);

        Assert.Equal(new[] { "MatchA:Total", "MatchB:Moneyline" }, result);
    }

    [Fact]
    public void ProcessOddsFeed_DoesNotMarkMarketForExactlyThreeChangesInWindow()
    {
        string[] feed =
        {
            Entry("MatchA", "Moneyline", 2.0m, 0),
            Entry("MatchA", "Moneyline", 2.1m, 1),
            Entry("MatchA", "Moneyline", 2.2m, 2),
            Entry("MatchA", "Moneyline", 2.3m, 3)
        };

        List<string> result = Solutions.SolutionB.ProcessOddsFeed(feed, 10m, 10);

        Assert.Empty(result);
    }

    [Fact]
    public void ProcessOddsFeed_MarksMarketForMoreThanThreeChangesWithinWindow()
    {
        string[] feed =
        {
            Entry("MatchA", "Moneyline", 2.0m, 0),
            Entry("MatchA", "Moneyline", 2.1m, 1),
            Entry("MatchA", "Moneyline", 2.2m, 2),
            Entry("MatchA", "Moneyline", 2.3m, 3),
            Entry("MatchA", "Moneyline", 2.4m, 4)
        };

        List<string> result = Solutions.SolutionB.ProcessOddsFeed(feed, 10m, 10);

        Assert.Equal(new[] { "MatchA:Moneyline" }, result);
    }

    [Fact]
    public void ProcessOddsFeed_UsesRollingWindowForChangeCount()
    {
        string[] feed =
        {
            Entry("MatchA", "Moneyline", 2.0m, 0),
            Entry("MatchA", "Moneyline", 2.1m, 2),
            Entry("MatchA", "Moneyline", 2.2m, 4),
            Entry("MatchA", "Moneyline", 2.3m, 6),
            Entry("MatchA", "Moneyline", 2.4m, 8)
        };

        List<string> result = Solutions.SolutionB.ProcessOddsFeed(feed, 10m, 3);

        Assert.Empty(result);
    }

    [Fact]
    public void ProcessOddsFeed_IgnoresMalformedAndNonPositiveOddsEntries()
    {
        string[] feed =
        {
            Entry("MatchA", "Moneyline", 2.0m, 0),
            "malformed",
            Entry("MatchA", "Moneyline", 0m, 1),
            Entry("MatchA", "Moneyline", 2.0m, 2),
            Entry("MatchA", "Moneyline", 2.1m, 3),
            Entry("MatchA", "Moneyline", 2.2m, 4),
            Entry("MatchA", "Moneyline", 2.3m, 5)
        };

        List<string> result = Solutions.SolutionB.ProcessOddsFeed(feed, 10m, 10);

        Assert.Empty(result);
    }

    [Fact]
    public void ProcessOddsFeed_IgnoresOutOfOrderEntries()
    {
        string[] feed =
        {
            Entry("MatchA", "Moneyline", 2.0m, 0),
            Entry("MatchA", "Moneyline", 2.1m, 1),
            Entry("MatchA", "Moneyline", 99.0m, 0)
        };

        List<string> result = Solutions.SolutionB.ProcessOddsFeed(feed, 10m, 10);

        Assert.Empty(result);
    }

    [Fact]
    public void ProcessOddsFeed_ReturnsEmptyForNullOrEmptyFeed()
    {
        Assert.Empty(Solutions.SolutionB.ProcessOddsFeed(null, 10m, 10));
        Assert.Empty(Solutions.SolutionB.ProcessOddsFeed(Array.Empty<string>(), 10m, 10));
    }

    private static string Entry(string matchId, string marketType, decimal odds, int secondsAfterStart) =>
        $"{matchId},{marketType},{odds},2024-01-01T00:00:{secondsAfterStart:00}Z";
}
