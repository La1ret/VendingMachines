using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachines.Domain.Models
{
    public class Role
    {
        // Уникальный идентификатор роли (Primary Key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        // Системное имя роли (используется в коде)
        [Required(ErrorMessage = "Системное имя роли обязательно!")]
        [MaxLength(50)]
        public string SystemName { get; set; }

        // Отображаемое имя роли (для интерфейса пользователя)
        [Required(ErrorMessage = "Отображаемое имя роли обязательно!")]
        [MaxLength(100)]
        public string DisplayName { get; set; }

        // Описание роли
        public string Description { get; set; }

        // Навигационное свойство: список пользователей, принадлежащих этой роли
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
