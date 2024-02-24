using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Services;
using Tyl.LondonStockExchange.Infrastructure.Entities;
using Tyl.LondonStockExchange.Infrastructure.Repositories;

namespace Tyl.LondonStockExchange.UnitTests.Services;

public class TradeServiceTests
{
    [Fact]
    public void CreateTrade_WhenValidTradeCreated_ShouldCallAndSaveInRepo()
    {
        var expectedTrade = new Trade()
        {
            Id = Guid.NewGuid(),
            Ticker = "APPL",
            BrokerId = "TestBroker1",
            Price = 4em,
            Shares = 10
        };
        var mockTradeRepo = Substitute.For<TradeRepository>();
        mockTradeRepo.Add(Arg.Any<Trade>()).Returns(expectedTrade);
        var sut = new TradeService(mockTradeRepo);

        sut.CreateTrade(expectedTrade);

        mockTradeRepo.Received().Add(
            Arg.Is<Trade>(i => i.Id == expectedTrade.Id
                               && i.Ticker == expectedTrade.Ticker));
        mockTradeRepo.Transactions.Should().Contain(expectedTrade);
    }
    
    [Fact]
    public void CreateTrade_WhenExistingTradeCreated_ShouldBeSavedInRepo()
    {
        var mockTradeRepo = Substitute.For<TradeRepository>();
        mockTradeRepo.Transactions.Add(new Trade()
        {
            Id = Guid.NewGuid(),
            Ticker = "APPL",
            BrokerId = "TestBroker1",
            Price = 8em,
            Shares = 35
        });
        
        var newTrade = new Trade()
        {
            Id = Guid.NewGuid(),
            Ticker = "APPL",
            BrokerId = "TestBroker1",
            Price = 4em,
            Shares = 10
        };
        mockTradeRepo.Add(Arg.Any<Trade>()).Returns(newTrade);
        
        var sut = new TradeService(mockTradeRepo);

        sut.CreateTrade(newTrade);

        mockTradeRepo.Transactions.Should().HaveCount(2);
        mockTradeRepo.Transactions.Last().Should().BeEquivalentTo(newTrade);
    }
    
    [Fact]
    public void CreateTrade_WhenRepoThrowsException_ShouldThrowException()
    {
        var trade = new Trade()
        {
            Id = Guid.NewGuid(),
            Ticker = "APPL",
            BrokerId = "TestBroker1",
            Price = 4em,
            Shares = 10
        };
        var mockTradeRepo = Substitute.For<TradeRepository>();
        mockTradeRepo.Add(Arg.Any<Trade>()).ThrowsForAnyArgs(_ => new Exception());
        
        var sut = new TradeService(mockTradeRepo);

        sut.Invoking(i => i.CreateTrade(trade))
            .Should().Throw<Exception>();
    }
}