
using System.ComponentModel.DataAnnotations;

namespace Pokedex.ViewModels;

public class LoginVM
{
    [Display(Name = "Email ou Nome de Usuário", Prompt = "Email ou Nome de Usuário")]
    [Required(ErrorMessage = "Informe o Email ou Nome de Usuário.")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Senha", Prompt = "Senha")]
    [Required(ErrorMessage = "Informe sua Senha.")]
    public string Senha { get; set; }

    [Display(Name = "Manter Conectado?")]
    public bool Lembrar { get; set; } = false;
    
    public string UrlRetorno { get; set; }
}
