using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class CharacterItemsDTO
    {
        public CharacterItemsDTO()
        {
            Items = new HashSet<ItemInfoDTO>();
        }

        public long Id { get; set; }

        public virtual ICollection<ItemInfoDTO> Items { get; set; }
    }
}