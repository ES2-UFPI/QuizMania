using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class ItemQuantityDTO
    {
        public ItemInfoDTO Item { get; set; }

        public int Quantity { get; set; }
    }
}
