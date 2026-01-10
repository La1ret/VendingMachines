using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachines.Shared.DTOs.User
{
    internal class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Пароль обязателен!"),
        MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвержение пароля обязательно!"), 
        MinLength(6, ErrorMessage = "Подтверждённый пароль не может быть короче 6 символов"),
        Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
