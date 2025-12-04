using apiAutenticacao.Data;
using apiAutenticacao.Models;
using apiAutenticacao.Models.DTO;
using apiAutenticacao.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt;


namespace apiAutenticacao.Services
{
    public class AuthService
    {
        // Implementação dos métodos de autenticação e autorização

        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config, AppDbContext context)
        {
            _context = context;
            _config = config;

        }

        public async Task<ResponseLogin> Login(LoginDTO dadosUsuario)
        {

            Usuario? usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == dadosUsuario.Email);

            if (usuarioEncontrado != null)
            {
                // Verifica se a senha fornecida corresponde à senha armazenada 
                bool isValidPassword = Verify(dadosUsuario.Senha, usuarioEncontrado.Senha);

                if (isValidPassword)
                {
                    return new ResponseLogin
                    {
                        Erro = false,
                        Message = "Login realizado com sucesso",
                        Usuario = usuarioEncontrado
                    };

                }

                return new ResponseLogin
                {
                    Erro = true,
                    Message = "Login não realizado. Email ou senha incorretos",
                    Usuario = null
                };




            }

            return new ResponseLogin
            {
                Erro = true,
                Message = "Usuário não encontrado!",
            };


        }


        public async Task<ResponseCadastro> CadastrarUsuarioAsync(CadastroUsuarioDTO dadosUsuarioCadastro)
        {
            Usuario? usuarioExistente = await _context.Usuarios.
                FirstOrDefaultAsync(u => u.Email == dadosUsuarioCadastro.Email);

            if (usuarioExistente != null)
            {
                return new ResponseCadastro
                {
                    Erro = true,
                    Message = "Este email já está cadastrado no sistema."
                };

            }

            Usuario usuario = new Usuario
            {
                Nome = dadosUsuarioCadastro.Nome,
                Email = dadosUsuarioCadastro.Email,
                Senha = HashPassword(dadosUsuarioCadastro.Senha),
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new ResponseCadastro
            {
                Erro = false,
                Message = "Usuário cadastrado com sucesso!",
                Usuario = usuario

            };

        }


        public async Task<ResponseAlteraSenha> AlterarSenhaAsync
            (AlterarSenhaDTO dadosAlterarSenha)
        {
            Usuario? usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dadosAlterarSenha.Email);
            if (usuario == null)
            {
                return new ResponseAlteraSenha
                {
                    Erro = true,
                    Message = "Email não encontrado."
                };
            }

            bool isValidPassword = Verify(dadosAlterarSenha.SenhaAtual, usuario.Senha);

            if (!isValidPassword)
            {
                return new ResponseAlteraSenha
                {
                    Erro = true,
                    Message = "Senha atual incorreta."
                };
            }

            usuario.Senha = HashPassword(dadosAlterarSenha.NovaSenha);


            //_context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return new ResponseAlteraSenha
            {
                Erro = false,
                Message = _config["Mensagens:SenhaAlteradaSucesso"]

            };


        }

    }

}

