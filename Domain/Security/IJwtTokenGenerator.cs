using System;
using System.Collections.Generic;
using System.Text;
using VendingMachines.Domain.Models;

namespace VendingMachines.Domain.Security
{
    public interface IJwtTokenGenerator 
    {
        string GenerateToken(User user, string roleSustemName, DateTime exireDate);
    }
}
