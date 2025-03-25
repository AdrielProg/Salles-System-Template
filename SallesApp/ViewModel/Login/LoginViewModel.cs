using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SallesApp.ViewModel.Login;

    public class LoginViewModel
    {
    [Required(ErrorMessage = "Informe seu nome:")]
    [DisplayName("Nome Completo")]
    public string Name  { get; set; }

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

    public string GetFirstName() => Name.Split(' ')[0]?? "";

    public bool IsLoginValid() => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
    public bool IsRegisterValid () => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) 
                                    && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && Password == ConfirmPassword;
}

