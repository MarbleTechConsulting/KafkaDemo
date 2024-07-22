namespace KafkaRecords
{
    public record TradeInfo(string TradeName, int TradeAmount, int TradePrice);

    public class TradeInfoBuilder
    {
        private static string? _tradeName;
        private static int? _tradeAmount;
        private static int? _tradePrice;

        public TradeInfoBuilder WithTradeName(string tradename)
        {
            _tradeName = tradename;
            return this;
        }
        public TradeInfoBuilder WithTradeAmount(int tradeAmount)
        {
            _tradeAmount = tradeAmount;
            return this;
        }
        public TradeInfoBuilder WithTradePrice(int tradePrice)
        {
            _tradePrice = tradePrice;
            return this;
        }

        public TradeInfo Build() => new(
            TradeName: _tradeName ?? string.Empty,
            TradeAmount: _tradeAmount.GetValueOrDefault(),
            TradePrice: _tradePrice.GetValueOrDefault());
    }
}
