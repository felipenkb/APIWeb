using APIWeb.Models.DTO;
using APIWeb.Models.Cliente;

namespace APIWeb.Interfaces
{
    public interface IClienteRepository
    {
        Task<bool> Create(CreateClienteModel model);
        Task<List<ClienteDTO>> GetAll();
    }
}
