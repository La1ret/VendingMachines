using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VendingMachines.Domain.Models
{
    public class User 
    {
        // Уникальный идентификатор пользователя (Primary Key)
        [Key]
        public int UserId { get; set; }

        // Имя пользователя (логин)
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        // Хэш пароля
        [Required]
        public string PasswordHash { get; set; }

        // Полное имя пользователя
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        // Внешний ключ
        public int RoleId { get; set; }

        // Навигационное свойство
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
