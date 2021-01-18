using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class ItemQuantity
    {
        public ItemQuantity() { }

        public ItemQuantity(ItemInfo item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        [Key]
        public long Id { get; set; }

        public virtual ItemInfo Item { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}