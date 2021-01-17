using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class SaveQuiz_QuestionDTO
    {
        [Required]
        [MaxLength(512)]
        public string Text { get; set; }

        public virtual ICollection<SaveQuiz_AnswerDTO> Answers { get; set; }

        public SaveQuiz_QuestionDTO()
        {
            Answers = new HashSet<SaveQuiz_AnswerDTO>();
        }
    }
}
