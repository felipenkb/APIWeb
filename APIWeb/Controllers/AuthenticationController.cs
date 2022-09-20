using APIWeb.Interfaces.Service;
using APIWeb.Models.Constants;
using APIWeb.Models.Identity;
using APIWeb.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthenticationController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthenticationController(IIdentityService identityService) => _identityService = identityService;

        [Authorize(Roles = Roles.Admin)]
        [SwaggerResponse(200, "Successful operation", Type = typeof(BaseResponse))]
        [SwaggerResponse(400, "Failed operation", Type = typeof(FailedBaseResponse))]
        [SwaggerOperation(Summary = "", Description = "", Tags = new[] { "AUTHENTICATION" })]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UsuarioCadastroRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (user.Role != Roles.Admin)
               return BadRequest();

            var result = await _identityService.CadastrarUsuario(user);
            if (result.Success)
                return Ok(result);
            else if (result.Errors.Count > 0)
                return BadRequest(result);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [SwaggerResponse(200, "Successful operation", Type = typeof(BaseResponse))]
        [SwaggerResponse(400, "Failed operation", Type = typeof(FailedBaseResponse))]
        [SwaggerOperation(Summary = "", Description = "", Tags = new[] { "AUTHENTICATION" })]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioLoginRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _identityService.LoginUsuario(user);
            if (result.Success)
                return Ok(result);

            return Unauthorized(result);
        }
    }
}
