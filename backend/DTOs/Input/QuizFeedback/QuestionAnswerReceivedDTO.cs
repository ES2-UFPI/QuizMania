using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class QuestionAnswerReceivedDTO
    {
        public long QuestionId { get; set; }

        public ICollection<long> ChosenAnswerIds { get; set; }

        public QuestionAnswerReceivedDTO()
        {
            ChosenAnswerIds = new HashSet<long>();
        }
    }
}
