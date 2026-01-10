using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachines.Domain.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string hashedPassword);
    }
}
