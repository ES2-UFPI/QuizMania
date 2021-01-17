using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models {
    public sealed class ItemInfo {
        public ItemInfo() {
            Effects = new HashSet<EffectBase>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public ICollection<EffectBase> Effects { get; set; }
    }
}