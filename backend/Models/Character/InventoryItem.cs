using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class InventoryItem
    {
        public InventoryItem() { }

        public InventoryItem(ItemInfo item, int quantity, bool isEquipped)
        {
            Item = item;
            Quantity = quantity;
            IsEquipped = isEquipped;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public Character Character { get; set; }

        [Required]
        public ItemInfo Item { get; set; }

        [Required]
        public bool IsEquipped { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}