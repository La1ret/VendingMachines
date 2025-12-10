using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VendingMachines.Service
{
    internal class NavigationManager
    {
        public static Frame MainFrame { get; set; }

        internal static void ChangeWindow(string windowType, Window currentWindow)
        {
            Window newWindow = null;
            if (windowType.ToLower() == "main")
                newWindow = new MainWindow();
            else if (windowType.ToLower() == "authorization")
                newWindow = new View.Windows.Authorization();
            else return;

            if (newWindow != null)
            {
                newWindow.Show();

                if (currentWindow != null)
                    currentWindow.Close();
            }
        }
    }
}
