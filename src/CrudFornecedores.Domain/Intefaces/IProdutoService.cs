using CrudFornecedores.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CrudFornecedores.Domain.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}