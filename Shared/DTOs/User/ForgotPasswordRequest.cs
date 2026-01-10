using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachines.Shared.DTOs.User
{
    public class ForgotPasswordRequest
    {
        [Required (ErrorMessage = "Логин обязателен!")]
        public string Login { get; set; }

        [Required (ErrorMessage = "Почта обязательна!"), EmailAddress]
        public string Email { get; set; }
    }
}
