using APIWeb.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using APIWeb.Interfaces;
using APIWeb.Models.Responses;
using APIWeb.Models.Cliente;

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