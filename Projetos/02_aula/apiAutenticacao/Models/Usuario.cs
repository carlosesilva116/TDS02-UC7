namespace apiAutenticacao.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; } = true;


        public Usuario()
        {
            DataCadastro = DateTime.Now;
            Ativo = true;
        }

        //exemplo tualizado public string senha { get; set; }
        //public int getId()
        //{
        //    return Id;
        //}



    }
}
