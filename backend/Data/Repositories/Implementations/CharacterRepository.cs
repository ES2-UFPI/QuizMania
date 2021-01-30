using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class CharacterRepository : BaseRepository, ICharacterAsyncRepository
    {
        public CharacterRepository (DatabaseContext context) :base(context) { }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetCharacterAllAsync(long id)
        {
            return await _context.Characters.Include(c => c.Guild)
                                            .Include(c => c.Items).ThenInclude(i => i.Item)
                                            .Include(c => c.QuizFeedbacks).ThenInclude(qb => qb.Quiz)
                                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character> GetCharacterSimpleAsync(long id)
        {
            return await _context.Characters.Include(c => c.Guild).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character> GetCharacterItemsAsync(long id)
        {
            return await _context.Characters.Include(c => c.Items).ThenInclude(i => i.Item)
                                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ItemInfo>> GetAllItemsAsync()
        {
            return await _context.Items.Include(i => i.Effects).ToListAsync();
        }

        public async Task<IEnumerable<Guild>> GetAllGuildsAsync()
        {
            return await _context.Guilds.ToListAsync();
        }

        public async Task<Guild> GetGuildMembersAsync(long id)
        {
            return await _context.Guilds.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Character>> GetAllCharsWithoutGuildAsync()
        {
            return await _context.Characters.Where(c => c.Guild == null).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
