using api.Data;
using api.Dtos.Stock;
using api.Interface;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDbContext context, IStockRepository repository)
        {
            _context = context;
            _stockRepository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockRepository.GetAllAsync();
            var stocksDto = stocks.Select(stock => stock.ToStockDto());
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            var stockDto = stock.ToStockDto();
            return Ok(stockDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto)
        {
            var stock = createStockDto.ToStockFromCreateDto();
            var createdStock = await _stockRepository.CreateStock(stock);
            return CreatedAtAction(nameof(GetStock), new { id = createdStock.Id }, createdStock.ToStockDto());

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var stock = await _stockRepository.UpdateStock(id, updateStockDto);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteStock(id);
            if (stock == null)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}