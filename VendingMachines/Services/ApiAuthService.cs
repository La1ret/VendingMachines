using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.WPF.Services.IServices;

namespace VendingMachines.WPF.Services
{
    internal class ApiAuthService : IApiAuthService
    {
        private bool _isLocked;

        public bool IsLocked
        {
            get => _isLocked;
        }
    }
}
