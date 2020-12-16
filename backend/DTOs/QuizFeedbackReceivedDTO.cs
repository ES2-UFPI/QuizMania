using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs
{
    public class QuizFeedbackReceivedDTO
    {
        public long QuizId { get; set; }

        public ICollection<QuestionAnswerDTO> QuestionAnswers { get; set; }

        public QuizFeedbackReceivedDTO()
        {
            QuestionAnswers = new HashSet<QuestionAnswerDTO>();
        }
    }
}
