using API.Settings.Infrastructure.Internal;

using Core.Entities.Users;

using Domain.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sql.DataAccess;

using SQL.DataAccess.Users;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
           => this._configuration = configuration;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureServices(IServiceCollection services)
             => services
                 .AddSingleton(_ => this._configuration)
                 .Apply(this.RegisterSettings)
                 .Apply(this.RegisterGateways)
                 .Apply(this.RegisterInteractors)
                 .AddControllers()
                 .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment hostEnvironment)
            => appBuilder
                .UseHttpsRedirection()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());

        private IServiceCollection RegisterSettings(IServiceCollection services)
           => services
               .AddSingleton<ISqlDbSettings>(new SqlDbSettings { ConnectionString = this._configuration.GetConnectionString("SqlDB") });

        private IServiceCollection RegisterGateways(IServiceCollection services)
            => services
                .AddScoped<IUsersGateway, UsersRepository>();

        private IServiceCollection RegisterInteractors(IServiceCollection services)
            => services
                .AddScoped<IUsersBoundary, UsersInteractor>();
    }
}
