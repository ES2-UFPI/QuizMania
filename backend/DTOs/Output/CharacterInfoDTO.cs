using AutoMapper;
using QuizMania.WebAPI.Models;
using QuizMania.WebAPI.Services;

namespace QuizMania.WebAPI.DTOs
{
    public class CharacterInfoDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int HealthPoints { get; set; }

        public int Level { get; set; }

        public int TotalXP { get; set; }

        public int CurrentLevelXP { get; set; }

        public int Gold { get; set; }
    }
}