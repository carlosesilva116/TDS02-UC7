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

        public AuthService(AppDbContext context)
        {
            _context = context;

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
                        Mensagem = "Login realizado com sucesso",
                        Usuario = usuarioEncontrado
                    };

                }

                return new ResponseLogin
                {
                    Erro = true,
                    Mensagem = "Login não realizado. Email ou senha incorretos",
                    Usuario = null
                };




            }

            return new ResponseLogin
            {
                Erro = true,
                Mensagem = "Usuário não encontrado!",
            };


        }


    }

}

