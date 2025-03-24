using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SallesApp.ViewModel.Login;

    public class LoginViewModel
    {
    [Required(ErrorMessage = "Informe o nome do usuário")]
    [DataType(DataType.EmailAddress)]
    [DisplayName("Email")]
    public string Email { get; set; }

    [Required(ErrorMessage ="Informe a senha")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Confirme a senha")]
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirme a senha")]
    public string ConfirmPassword { get; set; }
    public string ReturnUrl { get; set; }
}

