using System;
using System.Threading.Tasks;
using CrudFornecedores.Domain.Intefaces;
using CrudFornecedores.Domain.Models;
using CrudFornecedores.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CrudFornecedores.Infra.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(FornecedoresProdutosContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }
    }
}