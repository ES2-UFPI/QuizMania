using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class CharacterItemsDTO
    {
        public CharacterItemsDTO()
        {
            Items = new HashSet<ItemInfoDTO>();
        }

        public long CharacterId { get; set; }

        public virtual ICollection<ItemInfoDTO> Items { get; set; }
    }
}