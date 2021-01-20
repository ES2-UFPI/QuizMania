using QuizMania.WebAPI.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class ItemInfoDTO
    { 
        public string Name { get; set; }

        public int Cost { get; set; }

        [JsonIgnore]
        public SlotType ST { get; set; }

        public string SlotType => ST.ToString();

        public int MaxQuantity { get; set; }

        public ICollection<EffectBaseInfoDTO> Effects { get; set; }

        public ItemInfoDTO()
        {
            Effects = new HashSet<EffectBaseInfoDTO>();
        }
    }
}
