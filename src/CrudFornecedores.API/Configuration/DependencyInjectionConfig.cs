using CrudFornecedores.API.Extensions;
using CrudFornecedores.Domain.Intefaces;
using CrudFornecedores.Domain.Notificacoes;
using CrudFornecedores.Domain.Services;
using CrudFornecedores.Infra.Context;
using CrudFornecedores.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CrudFornecedores.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<FornecedoresProdutosContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            // USER
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}