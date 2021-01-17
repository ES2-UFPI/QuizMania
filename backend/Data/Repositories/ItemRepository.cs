using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class ItemRepository : IItemAsyncRepository
    {
        private readonly DatabaseContext _context;

        public ItemRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemAsync(long id)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
