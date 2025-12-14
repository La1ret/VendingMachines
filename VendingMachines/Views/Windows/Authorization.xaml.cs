using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VendingMachines.Helpers;
using VendingMachines.ViewModels;
using VendingMachines.Services.Interfaces;

namespace VendingMachines.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml, подключение AuthorizationViewModel.cs
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization(AuthorizationViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

    }
}
