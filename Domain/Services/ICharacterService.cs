using System.Collections.Generic;

namespace Domain.Services
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface ICharacterService
    {
        Task<GuildViewModel> GetGuildMembers();

        Task<List<CharacterViewModel>> GetAllGuildMembers();

        Task<string> GetMemberPicture(Guid publicId);
    }
}