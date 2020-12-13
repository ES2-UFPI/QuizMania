using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class Answer
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        [MaxLength(256)]
        public string Text { get; set; }
    }
}
