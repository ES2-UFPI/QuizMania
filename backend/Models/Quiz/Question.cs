using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizMania.WebAPI.Models
{
    public class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Quizzes = new HashSet<Quiz>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Text { get; set; }

        [Required]
        public virtual ICollection<Answer> Answers { get; set; }

        [Required]
        [JsonIgnore]
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}