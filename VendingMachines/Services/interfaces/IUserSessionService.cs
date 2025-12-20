using VendingMachines.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachines.Services.Interfaces
{
    public interface IUserSessionService
    {
        User CurrentUser { get; set; }
        void StartSession(User user);
        void CreateGuestSession(); 
    }
}
