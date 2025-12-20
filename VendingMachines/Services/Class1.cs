using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VendingMachines.Services
{
    public class Class1
    {

        public async static void Button_Click()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://localhost:5001/api/test/hello");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(content); // Выведет JSON от сервера
                    }
                    else
                    {
                        MessageBox.Show("Ошибка сервера: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось достучаться до API: " + ex.Message);
                }
            }
        }
    }
}
