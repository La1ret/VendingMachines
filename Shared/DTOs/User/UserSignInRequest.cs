using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachines.Shared.DTOs.User
{
    public class UserSignInRequest
    {
        [Required(ErrorMessage = "Имя пользователя (логин) обязательно!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен!"),
        MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов!")]
        public string Password { get; set; }
    }
}
