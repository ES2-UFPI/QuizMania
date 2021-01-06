using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs
{
    public class QuizReadDTO
    {
        public long Id { get; set; }
        public virtual ICollection<QuestionReadDTO> Questions { get; set; }

        public QuizReadDTO()
        {
            Questions = new HashSet<QuestionReadDTO>();
        }
    }
}
