using System.ComponentModel.DataAnnotations;

namespace apiAutenticacao.Models.DTO
{
    public class AlterarEmailDTO
    {
        [Required(ErrorMessage = "O email atual é obrigatório.")]
        public string EmailAtual { get; set; } = string.Empty; 

        [Required(ErrorMessage = "O novo email é obrigatório.")]
        public string NovoEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação do novo email deve ser igual novo email.")]
        public string ConfirmacaoNovoEmail { get; set; } = string.Empty;


    }
}
