using System.ComponentModel.DataAnnotations;

namespace LoginSeguroMvc.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o usuário.")]
    [StringLength(80)]
    public string Usuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;
}
