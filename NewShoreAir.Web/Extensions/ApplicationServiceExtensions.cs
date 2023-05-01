using Microsoft.OpenApi.Models;
using NewShore.Bussines;
using NewShore.DataAccess;

namespace NewShoreAir.Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });



        }


        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"NewShoreAir {groupName}",
                    Version = groupName,
                    Description = "NewShoreAir API",
                    Contact = new OpenApiContact
                    {
                        Name = "Oscar ALfaro",
                        Email = "oalfaro@outlook.com",
                    }
                });
                //options.OperationFilter<AddRequiredHeaderParameter>();
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

            });
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightDataAcces, FlightDataAcces>();
            services.AddScoped<IFlightBussines, FlightBussines>();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddLogging().AddLog4Net();


            //string connectionString = configuration.GetConnectionString("cnSqlite");
            //services.AddDbContext<BattleOfMonstersContext>(options =>
            //    options.UseSqlite(connectionString, b => b.MigrationsAssembly("API")));
        }
    }
}
