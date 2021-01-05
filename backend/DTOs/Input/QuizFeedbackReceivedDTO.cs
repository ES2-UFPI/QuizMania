using QuizMania.WebAPI.DTOs.Input;
using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs
{
    public class QuizFeedbackReceivedDTO
    {
        public long CharacterId { get; set; }

        public long QuizId { get; set; }

        public ICollection<QuestionAnswerReceivedDTO> QuestionAnswers { get; set; }

        public QuizFeedbackReceivedDTO()
        {
            QuestionAnswers = new HashSet<QuestionAnswerReceivedDTO>();
        }
    }
}
