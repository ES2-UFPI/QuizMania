using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Output {
    public class CharacterRankingDTO {
        public virtual ICollection<CharacterInfoDTO> Ranking { get; set; }

        public CharacterRankingDTO() {
            Ranking = new HashSet<CharacterInfoDTO>();
        }
    }
}