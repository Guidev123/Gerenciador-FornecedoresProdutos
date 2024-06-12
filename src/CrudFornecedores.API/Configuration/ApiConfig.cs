using CrudFornecedores.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CrudFornecedores.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FornecedoresProdutosContext>(
                  options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerConfig();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        public static void UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}