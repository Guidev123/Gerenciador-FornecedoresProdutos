using CrudFornecedores.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CrudFornecedores.Domain.Intefaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}