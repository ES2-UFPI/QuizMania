using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.DTOs.Input
{
    public class SaveQuestion_QuestionDTO
    {
        public long QuizId { get; set; }

        public long CharacterId { get; set; }

        [Required]
        [MaxLength(512)]
        public string Text { get; set; }

        public virtual ICollection<SaveQuestion_AnswerDTO> Answers { get; set; }

        public SaveQuestion_QuestionDTO()
        {
            Answers = new HashSet<SaveQuestion_AnswerDTO>();
        }

    }
}
