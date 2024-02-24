using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Entities;
using Tyl.LondonStockExchange.Core.Services;

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
            Price = 4.0m,
            Shares = 10
        };
        var mockTradeRepo = Substitute.For<IBaseRepository<Trade>>();
        mockTradeRepo.Add(Arg.Any<Trade>()).Returns(expectedTrade);
        var sut = new TradeService(mockTradeRepo);

        var result = sut.CreateTrade(expectedTrade);

        mockTradeRepo.Received().Add(Arg.Is<Trade>(i => i.Ticker == expectedTrade.Ticker));
        result.Should().BeEquivalentTo(expectedTrade);
    }
    
    [Fact]
    public void CreateTrade_WhenRepoThrowsException_ShouldThrowException()
    {
        var trade = new Trade()
        {
            Id = Guid.NewGuid(),
            Ticker = "APPL",
            BrokerId = "TestBroker1",
            Price = 4.0m,
            Shares = 10
        };
        var mockTradeRepo = Substitute.For<IBaseRepository<Trade>>();
        mockTradeRepo.Add(Arg.Any<Trade>()).ThrowsForAnyArgs(_ => new Exception());
        
        var sut = new TradeService(mockTradeRepo);

        sut.Invoking(i => i.CreateTrade(trade))
            .Should().Throw<Exception>();
    }
}