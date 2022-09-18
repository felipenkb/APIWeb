using APIWeb.Models.DTO;
using WebAPI.DataBase.ApiData;
using WebAPI.DataBase.ModelsData;
using WebAPI.Interfaces;
using WebAPI.Models.ClienteModel;

namespace WebAPI.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApiContext _context;

        public ClienteRepository(ApiContext context){ _context = context; }

        public async Task<bool> Create(CreateClienteModel model)
        {
            try
            {
                var db = new Cliente()
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    Documento = model.Documento,
                    DataDeNascimento = model.DataNascimento
                };

                _context.Add(db);
                _context.SaveChanges();
                return true;
            }
            catch (Exception){ return false; }
        }

        public async Task<List<ClienteDTO>> GetAll()
        {
            try
            {
                var clientes = _context.Clientes.ToList();
                if (clientes.Count > 0)
                {
                    List<ClienteDTO> list = new();

                    foreach (var item in clientes)
                    {
                        ClienteDTO result = new()
                        {
                            Id = item.Id,
                            Nome = item.Nome,
                            Documento = item.Documento,
                            DataDeNascimento = item.DataDeNascimento.ToString("dd/MM/yyyy HH:mm:ss")
                        };

                        list.Add(result);
                    }
                    return list;
                }

                return null;
            }
            catch (Exception){ return null;}
        }
    }
}
