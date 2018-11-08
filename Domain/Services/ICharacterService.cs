using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICharacterService
    {
        Task<string> GetGuildMembers();
    }
}