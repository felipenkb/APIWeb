using Microsoft.EntityFrameworkCore;
using WebAPI.DataBase.ModelsData;

namespace WebAPI.DataBase.ApiData
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        #region DbSets
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Conta> Contas { get; set; }

        #endregion
    }
}
