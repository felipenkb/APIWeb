using Microsoft.EntityFrameworkCore;
using APIWeb.DataBase.ModelsData;

namespace APIWeb.DataBase.ApiData
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
