using System.Collections.Generic;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class SaveQuiz_QuizDTO
    {
        public long OwnerId { get; set; }

        public virtual ICollection<SaveQuiz_QuestionDTO> Questions { get; set; }

        public SaveQuiz_QuizDTO()
        {
            Questions = new HashSet<SaveQuiz_QuestionDTO>();
        }
    }
}
