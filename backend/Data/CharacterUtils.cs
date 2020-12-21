using System;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public static class CharacterUtils
    {
        public const float LevelExperienceConst = 0.14f;

        public static void GetLevelExperience(int totalXP, out int level, out int currentLvlXP)
        {
            level = (int)Math.Floor(LevelExperienceConst * Math.Sqrt(totalXP)) + 1;
            currentLvlXP = Math.Max(0, (int)(totalXP - Math.Pow((level - 1) / LevelExperienceConst, 2)));
        }
    }
}
