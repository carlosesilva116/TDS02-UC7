namespace apiAutenticacao.Models.Response
{
    public class ResponseAlteraSenha
    {
        public bool Erro { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;


    }
}
