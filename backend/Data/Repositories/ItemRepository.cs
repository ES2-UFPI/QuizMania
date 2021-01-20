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

        public async Task<IEnumerable<ItemInfo>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<ItemInfo> GetItemAsync(string name)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.Name == name);
        }
    }
}
