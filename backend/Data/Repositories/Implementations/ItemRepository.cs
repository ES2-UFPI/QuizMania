using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class ItemRepository : BaseRepository, IItemAsyncRepository
    {
        public ItemRepository(DatabaseContext context) : base(context) { }
        
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
