using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class Character
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        public int HealthPoints { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int TotalXP { get; set; }

        [Required]
        public int Gold { get; set; }
    }
}
