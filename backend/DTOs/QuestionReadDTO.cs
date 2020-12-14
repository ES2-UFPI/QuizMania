using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs
{
    public class QuestionReadDTO
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<AnswerReadDTO> Answers { get; set; }

        public QuestionReadDTO()
        {
            Answers = new HashSet<AnswerReadDTO>();
        }
    }
}
