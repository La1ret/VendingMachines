using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachines.WPF.Services.IServices
{
    internal interface IApiAuthService
    {
        bool IsLocked { get; }
    }
}
