using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output {
    public class CharacterRankingDTO {
        public virtual ICollection<CharacterInfoRankDTO> Ranking { get; set; }

        public CharacterRankingDTO() {
            Ranking = new HashSet<CharacterInfoRankDTO>();
        }
    }
}