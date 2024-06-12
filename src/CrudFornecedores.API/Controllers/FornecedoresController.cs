
using AutoMapper;
using CrudFornecedores.API.DTO;
using CrudFornecedores.API.Extensions;
using CrudFornecedores.Domain.Intefaces;
using CrudFornecedores.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudFornecedores.API.Controllers
{
    [Authorize]
    [Route("api/fornecedores")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository, 
                                      IMapper mapper, 
                                      IFornecedorService fornecedorService,
                                      INotificador notificador, 
                                      IEnderecoRepository enderecoRepository) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
            _enderecoRepository = enderecoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorDTO>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<FornecedorDTO>>(await _fornecedorRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>> ObterPorId(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null) return NotFound();

            return fornecedor;
        }

        [ClaimsAuthorize("Fornecedor","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<FornecedorDTO>> Adicionar(FornecedorDTO fornecedorDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedorDTO));

            return CustomResponse(fornecedorDTO);
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>> Atualizar(Guid id, FornecedorDTO fornecedorDTO)
        {
            if (id != fornecedorDTO.Id)
            {
                NotificarErro("O id informado é diferente do que foi passado na query");
                return CustomResponse(fornecedorDTO);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorDTO));

            return CustomResponse(fornecedorDTO);
        }

        [ClaimsAuthorize("Fornecedor", "Remover")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>> Excluir(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            await _fornecedorService.Remover(id);

            return CustomResponse(fornecedorViewModel);
        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<EnderecoDTO> ObterEnderecoPorId(Guid id)
        {
            return _mapper.Map<EnderecoDTO>(await _enderecoRepository.ObterPorId(id));
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoDTO enderecoDTO)
        {
            if (id != enderecoDTO.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(enderecoDTO);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoDTO));

            return CustomResponse(enderecoDTO);
        }

        [HttpGet("fornecedores-produtos-endereco/{id}")]
        private async Task<FornecedorDTO> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorDTO>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

        [HttpGet("fornecedores-endereco/{id}")]
        private async Task<FornecedorDTO> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorDTO>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }
    }
}