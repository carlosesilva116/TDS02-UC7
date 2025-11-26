
namespace apiAutenticacao.Models.Response
{
    public class ResponseLogin
    {
        public bool Erro { get; set; }

        public string Mensagem { get; set; } = string.Empty;

        public Usuario? Usuario { get; set; }


    }
}
