using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMania.WebAPI.DTOs.Output
{
    public class GuildMembersDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CharacterInfoRankDTO> Members { get; set; }

        public GuildMembersDTO()
        {
            Members = new HashSet<CharacterInfoRankDTO>();
        }

    }
}
