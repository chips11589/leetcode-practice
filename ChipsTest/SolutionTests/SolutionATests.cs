using System;
using System.Collections.Generic;
using System.Linq;
using CodilityPractice;
using Xunit;

namespace CodingTest.SolutionTests;

public class SolutionATests
{
    [Fact]
    public void ProcessTransactions_ReturnsAccountWhenDailyTotalExceedsLimit()
    {
        string[] transactions =
        {
            "1001,6.25,2024-03-15",
            "1001,4.00,2024-03-15"
        };

        List<int> result = Solutions.SolutionA.ProcessTransactions(transactions, 10m);

        Assert.Equal(new[] { 1001 }, result);
    }

    [Fact]
    public void ProcessTransactions_DoesNotReturnAccountWhenDailyTotalEqualsLimit()
    {
        string[] transactions =
        {
            "1001,6.25,2024-03-15",
            "1001,3.75,2024-03-15"
        };

        List<int> result = Solutions.SolutionA.ProcessTransactions(transactions, 10m);

        Assert.Empty(result);
    }

    [Fact]
    public void ProcessTransactions_AggregatesByAccountAndCalendarDay()
    {
        string[] transactions =
        {
            "1001,6,2024-03-15",
            "1001,5,2024-03-15",
            "1002,6,2024-03-15",
            "1002,6,2024-03-16",
            "1003,11,2024-03-16"
        };

        List<int> result = Solutions.SolutionA.ProcessTransactions(transactions, 10m);

        Assert.Equal(new[] { 1001, 1003 }, result.OrderBy(accountId => accountId));
    }

    [Fact]
    public void ProcessTransactions_SkipsMalformedRecordsAndContinues()
    {
        string[] transactions =
        {
            "invalid-account,20,2024-03-15",
            "1001,not-an-amount,2024-03-15",
            "1001,20,not-a-date",
            "1001,20,2024-03-15,extra-field",
            "1002,12,2024-03-15",
            "1003,12,2024-03-15 18:30:00",
            "1004,12,2024-03-15"
        };

        List<int> result = Solutions.SolutionA.ProcessTransactions(transactions, 10m);

        Assert.Equal(new[] { 1002, 1004 }, result.OrderBy(accountId => accountId));
    }

    [Fact]
    public void ProcessTransactions_ReturnsEmptyWhenThereAreNoTransactions()
    {
        List<int> result = Solutions.SolutionA.ProcessTransactions(Array.Empty<string>(), 10m);

        Assert.Empty(result);
    }
}
