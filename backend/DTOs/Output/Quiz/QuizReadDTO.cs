using System.Collections.Generic;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class QuizReadDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public CharacterInfoDTO Owner { get; set; }
        public virtual ICollection<QuestionReadDTO> Questions { get; set; }

        public QuizReadDTO()
        {
            Questions = new HashSet<QuestionReadDTO>();
        }
    }
}
