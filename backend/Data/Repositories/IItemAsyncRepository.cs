using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public interface IItemAsyncRepository
    {
        Task<IEnumerable<ItemInfo>> GetAllItemsAsync();
        Task<ItemInfo> GetItemAsync(string name);
    }
}
