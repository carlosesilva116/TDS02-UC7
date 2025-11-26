using apiAutenticacao.Data;
using apiAutenticacao.Models;
using apiAutenticacao.Models.DTO;
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

        public async Task<String> Login(LoginDTO dadosUsuario)
        {

            Usuario? usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == dadosUsuario.Email);

            if (usuarioEncontrado != null)
            {
                // Verifica se a senha fornecida corresponde à senha armazenada 
                bool isValidPassword = Verify(dadosUsuario.Senha, usuarioEncontrado.Senha);

                if (isValidPassword)
                {
                    return ("Login realizado com sucesso");
                }

                return ("Email ou senha incorretos");


            }

            return ("Usuário não encontrado!");
        }


    }

}

