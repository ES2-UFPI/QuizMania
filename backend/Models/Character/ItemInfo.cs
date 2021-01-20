using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public enum SlotType
    {
        Face,
        Head,
        Neck,
        Arm,
        Hand,
        Leg,
        Hair,
        Pant,
        Shirt,
        Shoe
    }
}

namespace QuizMania.WebAPI.Models 
{
    public sealed class ItemInfo 
    {
        public ItemInfo() 
        {
            Effects = new HashSet<EffectBase>();
        }

        [Key]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public SlotType SlotType { get; set; }

        [Required]
        public int MaxQuantity { get; set; }

        [Required]
        public ICollection<EffectBase> Effects { get; set; }
    }
}