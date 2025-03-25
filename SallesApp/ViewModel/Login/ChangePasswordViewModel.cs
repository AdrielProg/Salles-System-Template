
using System.ComponentModel.DataAnnotations;

namespace SallesApp.ViewModel.Login;

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Informe o email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "As senhas não conferem")]
        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsChangePasswordValid() => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword);
    }
