using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class ItemInfoDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public int MaxQuantity { get; set; }

        public ICollection<EffectBaseInfoDTO> Effects { get; set; }
    }
}
