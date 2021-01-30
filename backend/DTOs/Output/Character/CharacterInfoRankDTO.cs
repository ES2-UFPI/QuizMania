using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class CharacterInfoRankDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int TotalXP { get; set; }

        public int CurrentLevelXP { get; set; }
    }
}
