using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Http;

namespace UltraV.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/{apiVersion}/[controller]")]
    public class CharacterController : Controller
    {
        private readonly ICharacterService service;

        public CharacterController(ICharacterService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetGuildMember()
        {
            return this.Ok(await this.service.GetGuildMembers());
        }
    }
}