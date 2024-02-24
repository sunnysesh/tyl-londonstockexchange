using FluentValidation;
using Tyl.LondonStockExchange.Core.Models;

namespace Tyl.LondonStockExchange.Core.Validators;

public class TradeValidator : AbstractValidator<TradeRequestModel>
{
    public TradeValidator()
    {
        RuleFor(trade => trade.Ticker)
            .NotEmpty();
        RuleFor(trade => trade.Price)
            .GreaterThan(0)
            .PrecisionScale(100, 2, false);
        RuleFor(trade => trade.Shares)
            .GreaterThan(0);
        RuleFor(trade => trade.BrokerId)
            .NotEmpty();
    }
}