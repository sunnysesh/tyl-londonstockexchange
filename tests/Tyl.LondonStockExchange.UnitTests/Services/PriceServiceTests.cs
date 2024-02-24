using FluentAssertions;
using NSubstitute;
using Tyl.LondonStockExchange.Core.Entities;
using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Services;

namespace Tyl.LondonStockExchange.UnitTests.Services;

public class PriceServiceTests
{
    [Fact]
    public async Task GetPricesViaTicker_WhenTickerExists_ShouldReturnCorrectTotal()
    {
        var expectedTrade = new Trade()
        {
            Id = Guid.NewGuid(),
            BrokerId = "testBrokerId123",
            Price = 4.50m,
            Shares = 50,
            Ticker = "APPL"
        };
        var mockTradeRepo = Substitute.For<IBaseRepository<Trade>>();
        mockTradeRepo.Get(Arg.Any<string>()).Returns(new List<Trade>(){expectedTrade});
        var sut = new PriceService(mockTradeRepo);

        var result = await sut.GetPricesViaTicker(new []{expectedTrade.Ticker});

        result.Should().HaveCount(1);
        result.Select(i => i.TotalValue).Sum().Should().Be(expectedTrade.Price);
    }
    
    [Fact]
    public async Task GetPricesViaTicker_WhenMultipleTickersExists_ShouldReturnCorrectTotals()
    {
        string[] expectedTickers = ["APPL", "MSFT"];
        var expectedTrades = new List<Trade>()
        {
            new Trade()
            {
                Id = Guid.NewGuid(),
                BrokerId = "testBrokerId123",
                Price = 4.50m,
                Shares = 50,
                Ticker = expectedTickers[0]
            },
            new Trade()
            {
                Id = Guid.NewGuid(),
                BrokerId = "testBrokerId123",
                Price = 16.22m,
                Shares = 6,
                Ticker = expectedTickers[0]
            },
            new Trade()
            {
                Id = Guid.NewGuid(),
                BrokerId = "testBrokerId456",
                Price = 50.0m,
                Shares = 100,
                Ticker = expectedTickers[1]
            }
        };
        var mockTradeRepo = Substitute.For<IBaseRepository<Trade>>();
        mockTradeRepo.Get(Arg.Any<string>()).Returns(expectedTrades);
        var sut = new PriceService(mockTradeRepo);
        
        var result = await sut.GetPricesViaTicker(new []{expectedTickers[0]});
        
        result.Should().HaveCount(1);
        result
            .Where(i => i.Ticker == expectedTickers[0])
            .Select(i => i.TotalValue)
            .Sum()
            .Should().Be(expectedTrades[0].Price + expectedTrades[1].Price);
    }
    
    [Fact]
    public async Task GetAllPrices_WhenTickersExist_ShouldReturnCorrectTotals()
    {
        string[] expectedTickers = ["APPL", "MSFT"];
        var expectedTrades = new List<Trade>()
        {
            new Trade()
            {
                Id = Guid.NewGuid(),
                BrokerId = "testBrokerId123",
                Price = 4.50m,
                Shares = 50,
                Ticker = expectedTickers[0]
            },
            new Trade()
            {
                Id = Guid.NewGuid(),
                BrokerId = "testBrokerId123",
                Price = 16.22m,
                Shares = 6,
                Ticker = expectedTickers[0]
            },
            new Trade()
            {
                Id = Guid.NewGuid(),
                BrokerId = "testBrokerId456",
                Price = 50.0m,
                Shares = 100,
                Ticker = expectedTickers[1]
            }
        };
        var mockTradeRepo = Substitute.For<IBaseRepository<Trade>>();
        mockTradeRepo.Get(Arg.Any<string>()).Returns(expectedTrades);
        var sut = new PriceService(mockTradeRepo);
        
        var result = await sut.GetAllPrices();
        
        result.Should().HaveCount(2);
        result
            .Where(i => i.Ticker == expectedTickers[0])
            .Select(i => i.TotalValue)
            .Sum()
            .Should().Be(expectedTrades[0].Price + expectedTrades[1].Price);
        
        result
            .Where(i => i.Ticker == expectedTickers[1])
            .Select(i => i.TotalValue)
            .Sum()
            .Should().Be(expectedTrades[2].Price);
    }

    [Fact]
    public async Task GetPricesViaTicker_WhenTickerDoesntExist_ShouldReturnNull()
    {
        var expectedTrade = new Trade()
        {
            Id = Guid.NewGuid(),
            BrokerId = "testBrokerId123",
            Price = 4.50m,
            Shares = 50,
            Ticker = "APPL"
        };
        var mockTradeRepo = Substitute.For<IBaseRepository<Trade>>();
        mockTradeRepo.Get(Arg.Any<string>()).Returns(new List<Trade>(){expectedTrade});
        var sut = new PriceService(mockTradeRepo);

        var result = await sut.GetPricesViaTicker(new []{"MSFT"});

        result.Should().BeEmpty();
        
    }
}