using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class CharacterItemsDTO
    {
        public long CharacterId { get; set; }

        public virtual ICollection<InventoryItemDTO> Items { get; set; }

        public CharacterItemsDTO()
        {
            Items = new HashSet<InventoryItemDTO>();
        }
    }
}