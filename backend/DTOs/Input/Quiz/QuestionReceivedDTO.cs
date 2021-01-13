using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class QuestionReceivedDTO
    {
        public string Text { get; set; }
        public virtual ICollection<AnswerReceivedDTO> Answers { get; set; }
        public QuestionReceivedDTO()
        {
            Answers = new HashSet<AnswerReceivedDTO>();
        }
    }
}
