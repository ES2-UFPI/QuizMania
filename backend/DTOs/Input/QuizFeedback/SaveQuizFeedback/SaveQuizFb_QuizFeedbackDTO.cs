using QuizMania.WebAPI.DTOs;
using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class SaveQuizFb_QuizFeedbackDTO
    {
        public long CharacterId { get; set; }

        public long QuizId { get; set; }

        public ICollection<SaveQuizFb_QuestionAnswerDTO> QuestionAnswers { get; set; }

        public SaveQuizFb_QuizFeedbackDTO()
        {
            QuestionAnswers = new HashSet<SaveQuizFb_QuestionAnswerDTO>();
        }
    }
}
