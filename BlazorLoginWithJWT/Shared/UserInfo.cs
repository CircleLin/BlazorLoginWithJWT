using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorLoginWithJWT.Shared
{
    public class UserInfo
    {
        [Required(ErrorMessage = "請填寫Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請填寫Password")]
        public string Password { get; set; }
    }
}