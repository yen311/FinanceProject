
using api.Dtos.Stock;
using api.Models;

namespace api.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateStock(Stock stock);
        Task<Stock?> UpdateStock(int id, UpdateStockDto updateStockDto);
        Task<Stock?> DeleteStock(int id);
    }
}