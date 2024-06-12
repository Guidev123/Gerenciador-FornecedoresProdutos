using CrudFornecedores.API.DTO;
using CrudFornecedores.API.Extensions;
using CrudFornecedores.Domain.Intefaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CrudFornecedores.API.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        public AuthController(INotificador notificador,
                               SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<AppSettings> appSettings) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserDTO registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            /* CRIANDO USUARIO */ 
            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(JwtGenerator());
            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);


            /* FAZENDO LOGIN DO USUARIO COM:                      EMAIL            SENHA         PERSISTENTE E BLOQUEAR POR TENTATIVAS INVALIDAS */
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded) return CustomResponse(JwtGenerator());
            
            if(result.IsLockedOut)
            {
                NotificarErro("O usuario foi temporariamente bloqueado por tentativas inválidas ");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuario ou senha incorretos");
            return CustomResponse(loginUser);
        }

        private string JwtGenerator()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret); // GERANDO CHAVE CRIPTOGRAFADA COM BASE NO NOSSO SECRET

            // CRIANDO TOKEN
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            // SERIALIZANDO O JWT EM UM JWT COMPACT SERIALIZATION FORM, PARA FICAR COMPATIVEL COM O PADRAO DA WEB
            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }
    }
}
