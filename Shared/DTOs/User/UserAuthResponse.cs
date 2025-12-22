using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachines.Shared.DTOs.User
{
    public class UserAuthResponse
    {
        public string Token;
        public DateTime ExpiryDate;
        public string FullName { get; set; }
        public string RoleSystemName {  get; set; }
        public bool IsGuest { get; set; }
    }
}
