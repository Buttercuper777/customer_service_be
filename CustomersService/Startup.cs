using CustomersService.Core;
using CustomersService.Core.Services;
using CustomersService.DataAccess;
using CustomersService.Middleware;
using CustomersService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CustomersService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            const string connectionString = "Host=localhost;Port=5432;Database=requisites;Username=mac;Password=root;";
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            services.AddDbContext<DataContext>(options =>
            {
                options.
                    UseNpgsql(builder.ConnectionString,
                        assembly => assembly.MigrationsAssembly("CustomersService"));
            });
            
            services.AddSingleton<IRequestHandlingService, RequestHandlingService>();
            
            services.AddTransient<IDbRepository, DbRepository>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IRequisitesService, RequisitesService>();
            
            services.AddControllers(); 
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}