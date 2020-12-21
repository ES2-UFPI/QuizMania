using QuizMania.WebAPI.Models;
using AutoMapper;

namespace QuizMania.WebAPI.DTOs
{
    public class CharacterReadDTO
    {
        public class CurrentXPResolver : IValueResolver<Character, CharacterReadDTO, int>
        {
            public int Resolve(Character source, CharacterReadDTO destination, int destMember, ResolutionContext context)
            {
                CharacterUtils.GetLevelExperience(source.TotalXP, out _, out var currentXP);
                return currentXP;
            }
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public int HealthPoints { get; set; }

        public int Level { get; set; }

        public int TotalXP { get; set; }

        public int CurrentLevelXP { get; set; }

        public int Gold { get; set; }
    }
}
