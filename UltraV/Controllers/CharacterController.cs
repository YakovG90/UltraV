using System;
using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
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

        [HttpGet("/GetFromAPI")]
        public async Task<IActionResult> GetGuildMember()
        {
            return this.Ok(await this.service.GetGuildMembers());
        }

        [HttpGet("image/{publicId}")]
        public async Task<IActionResult> GetMemberPicture([FromRoute] string publicId)
        {
            Guid.TryParse(publicId, out var publicIdentifier);

            var imageBytes = Convert.FromBase64String(await this.service.GetMemberPicture(publicIdentifier));
            
            return this.File(imageBytes, "image/jpg");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuildMembers()
        {
            var characterData = await this.service.GetAllGuildMembers();

            foreach (var character in characterData)
            {
                var file = File(character.PictureData, "image/jpg");
                character.Picture = file;
            }
            
            return this.Ok(characterData);
        }
    }
}