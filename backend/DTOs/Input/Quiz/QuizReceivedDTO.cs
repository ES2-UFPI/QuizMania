using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class QuizReceivedDTO
    {
        public virtual ICollection<QuestionReceivedDTO> Questions { get; set; }

        public QuizReceivedDTO()
        {
            Questions = new HashSet<QuestionReceivedDTO>();
        }
    }
}
