using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    public class AuthenticationViewModel
    {
        public LoginViewModel LoginModel { get; set; }
        public SignupViewModel SignUpModel { get; set; }
    }
}
