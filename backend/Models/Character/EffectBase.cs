using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class EffectBase
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
