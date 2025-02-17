﻿namespace Project.Product.Domain.Trademarks
{
    public interface ITrademarkRepository
    {
        Task<IEnumerable<TrademarkInfo>> GetTrademark(int? id);
        Task CreateTrademark(TrademarkInfo trademark);
        Task UpdateTrademark(TrademarkInfo trademark);
        Task DeleteTrademark(TrademarkInfo trademark);
        Task ReactiveTrademark(TrademarkInfo trademark);
        Task<TrademarkInfo> CheckTrademarkName(string name);
    }
}
