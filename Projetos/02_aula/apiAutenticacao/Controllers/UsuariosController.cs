using apiAutenticacao.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiAutenticacao.Data;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt;
using apiAutenticacao.Models.DTO;
using apiAutenticacao.Services;
using apiAutenticacao.Models.Response;

namespace apiAutenticacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public UsuariosController(AppDbContext context, AuthService authService)
        {

            _context = context;
            _authService = authService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastarUsuariosAsync([FromBody] CadastroUsuarioDTO dadosUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario? usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dadosUsuario.Email);

            if (usuarioExistente != null)
            {
                return BadRequest(new { Erro = true, messangem = "Já existe um usuário cadastrado com esse email." });
            }

            Usuario usuario = new Usuario
            {
                Nome = dadosUsuario.Nome,
                Email = dadosUsuario.Email,
                Senha = HashPassword(dadosUsuario.Senha),
                ConfirmacaoSenha = HashPassword(dadosUsuario.ConfirmacaoSenha)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                erro = false,
                mensagem = "Usuário cadastrado com sucesso",
                usuario = new

                {
                    id = usuario.Id,
                    nome = usuario.Nome,
                    email = usuario.Email
                }
            })
            ;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dadosUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseLogin response = await _authService.Login(dadosUsuario);

            if (response.Erro)
            {
                return BadRequest( response.Mensagem );
            }
            return Ok(response );


        }




    }

}