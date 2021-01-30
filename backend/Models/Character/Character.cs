using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class Character
    {
        private const float LevelExperienceConst = 0.14f;

        public Character()
        {
            TotalXP = 0;
            Quizzes = new HashSet<Quiz>();
            QuizFeedbacks = new HashSet<QuizFeedback>();
            Items = new HashSet<InventoryItem>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        
        public int GuildId { get; set; }

        [Required]
        public int HealthPoints { get; set; }

        public int Level => (int) Math.Floor(LevelExperienceConst * Math.Sqrt(TotalXP)) + 1; 

        public int CurrentLevelXP => Math.Max(0, (int)(TotalXP - Math.Pow((Level - 1) / LevelExperienceConst, 2)));

        [Required]
        public int TotalXP { get; set; }

        [Required]
        public int Gold { get; set; }

        public Guild Guild { get; set; }

        [Required]
        public virtual ICollection<Quiz> Quizzes { get; set; }

        [Required]
        public virtual ICollection<QuizFeedback> QuizFeedbacks { get; set; }

        [Required]
        public virtual ICollection<GoldExpense> GoldExpenses { get; set; }

        [Required]
        public virtual ICollection<InventoryItem> Items { get; set; }
    }
}
