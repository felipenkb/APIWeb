using APIWeb.DataBase.ApiData;
using APIWeb.DataBase.IdentityData;
using APIWeb.Interfaces.Repository;
using APIWeb.Interfaces.Service;
using APIWeb.Repository;
using APIWeb.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Extensions
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region DbContext
            services.AddDbContext<ApiContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddDbContext<IdentityDataContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            #endregion


            #region Identity
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();
            #endregion


            #region Scoped
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IIdentityService, IdentityServices>();
            #endregion
        }
    }
}
