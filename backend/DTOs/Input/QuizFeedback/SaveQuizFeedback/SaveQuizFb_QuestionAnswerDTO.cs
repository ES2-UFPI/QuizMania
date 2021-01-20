using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class SaveQuizFb_QuestionAnswerDTO
    {
        public long QuestionId { get; set; }

        public ICollection<long> ChosenAnswerIds { get; set; }

        public SaveQuizFb_QuestionAnswerDTO()
        {
            ChosenAnswerIds = new HashSet<long>();
        }
    }
}
