using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public interface IItemAsyncRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemAsync(long id);
    }
}
