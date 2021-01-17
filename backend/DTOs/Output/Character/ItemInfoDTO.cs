using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class ItemInfoDTO
    {
        public string Name { get; set; }

        public int Cost { get; set; }

        public ICollection<EffectBaseInfoDTO> Effects { get; set; }
    }
}
