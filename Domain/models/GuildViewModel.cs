using System.Collections.Generic;

namespace Domain.Models
{
    public class GuildViewModel
    {
        public ICollection<MemberViewModel> Members { get; set; }
    }

    public class MemberViewModel
    {
        public CharacterViewModel Character { get; set; }
        
        public int Rank { get; set; }
    }
}