using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class CharacterItemsDTO
    {
        public CharacterItemsDTO()
        {
            Items = new HashSet<ItemQuantityDTO>();
        }

        public long CharacterId { get; set; }

        public virtual ICollection<ItemQuantityDTO> Items { get; set; }
    }
}