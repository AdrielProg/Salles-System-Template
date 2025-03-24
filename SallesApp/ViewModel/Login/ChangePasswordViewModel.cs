using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SallesApp.ViewModel.Login;

    public class ChangePasswordViewModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
