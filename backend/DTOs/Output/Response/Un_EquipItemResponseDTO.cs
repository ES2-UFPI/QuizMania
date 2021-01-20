using QuizMania.WebAPI.DTOs.Input;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class Un_EquipItemResponseDTO
    {
        public enum RequestResult
        {
            BadRequest,
            Success,
            CharacterNotFound,
            InventoryWithoutItem
        }

        internal RequestResult _result { get; set; }
        public string Result => _result.ToString();
        public Un_EquipItemRequestDTO Request { get; set; }
    }
}
