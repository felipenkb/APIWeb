using APIWeb.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using APIWeb.Interfaces.Repository;
using APIWeb.Models.Responses;
using APIWeb.Models.Cliente;
using Microsoft.AspNetCore.Authorization;
using APIWeb.Models.Constants;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ClienteController : Controller
    {
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Policy = Policies.HorarioComercial)]
        [SwaggerResponse(200, "Successful operation", Type = typeof(ClienteDTO))]
        [SwaggerResponse(400, "Failed operation", Type = typeof(FailedBaseResponse))]
        [SwaggerOperation(Summary = "", Description = "", Tags = new[] { "CLIENTE" })]
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            FailedBaseResponse retornofailed = new ();
            try
            {
                var get = await _repository.GetAll();
                if (get != null) return Ok(get);
                
                return BadRequest(retornofailed);
            }
            catch (Exception) { throw; }
        }

        [Authorize(Roles = "Admin")]
        [SwaggerResponse(200, "Successful operation", Type = typeof(BaseResponse))]
        [SwaggerResponse(400, "Failed operation", Type = typeof(FailedBaseResponse))]
        [SwaggerOperation(Summary = "", Description = "", Tags = new[] { "CLIENTE" })]
        [HttpPost]
        public async Task<IActionResult> CriarCliente(CreateClienteModel model)
        {
            var retornosuccess = new BaseResponse();
            var retornofailed = new  FailedBaseResponse();
            try
            {
                var add = await _repository.Create(model);
                if (add) return Ok(retornosuccess);

                return BadRequest(retornofailed);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}