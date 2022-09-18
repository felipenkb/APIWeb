using APIWeb.Models.DTO;
using WebAPI.DataBase.ModelsData;
using WebAPI.Models.ClienteModel;

namespace WebAPI.Interfaces
{
    public interface IClienteRepository
    {
        Task<bool> Create(CreateClienteModel model);
        Task<List<ClienteDTO>> GetAll();
    }
}
