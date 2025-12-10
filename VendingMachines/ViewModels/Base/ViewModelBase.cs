using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Web;

namespace VendingMachines.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyCanged([CallerMemberName] string PropertyName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null) 
        { 
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyCanged(PropertyName);
            return true;
        }

        private bool _Disposed;

        public void Dispodse() 
        {
            Dispodse(true);
        }

        protected virtual void Dispodse(bool Disposing) 
        {
            if(Disposing || _Disposed) return;
            _Disposed = true;
            // осовобождение управляемых ресов
        }
    }
}
