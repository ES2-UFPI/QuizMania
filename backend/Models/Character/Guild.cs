using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizMania.WebAPI.Models
{
    public class Guild
    {
        public Guild()
        {
            Members = new HashSet<Character>();
        }
        
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        public virtual ICollection<Character> Members { get; set; }
    }
}
