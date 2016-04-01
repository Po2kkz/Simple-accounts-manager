using MahApps.Metro.Controls;
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
using System.Security.Cryptography;
using Demot.RandomOrgApi;
namespace Simple_Accounts_Manager
{
    /// <summary>
    /// Interaction logic for PwGenWin.xaml
    /// </summary>
    public partial class PwGenWin : MetroWindow
    {
        public static PwGenWin Instance;
        public static RandomOrgApiClient RandomOrgClient = new RandomOrgApiClient("02c13539-6e02-47cd-b613-433cc7279bf7");
        String letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuwxyz";
        String numbers = "0123456789";
        String specialCharacters = "!#$%&/()='+?*,.;:-_";
        

        public PwGenWin()
        {
            Instance = this;
            InitializeComponent();
        }

        private void PwGenBtn(object sender, RoutedEventArgs e)
        {
            String password = "";
            String pool = "";
            int n = Int32.Parse(textBox.Text);

            if (checkBox.IsChecked.Value)
            {
                pool += letters;
            }
            if (checkBox2.IsChecked.Value)
            {
                pool += numbers;
            }
            if(checkBox3.IsChecked.Value)
            {
                pool += specialCharacters;
            }

            if(checkBox1.IsChecked.Value)
            {
                Response response;
                int[] i;
                response = RandomOrgClient.GenerateIntegers(n, 0, pool.Length - 1);
                i = response.Integers;
                for (int k = 0; k < n; k++)
                {
                    password += pool.ElementAt(i[k]);
                }
            }
            else
            {
                Random random = new Random();
                for (int k = 0;k< n;k++)
                {
                    password += pool.ElementAt(random.Next(0, pool.Length));
                }

            }
            textBox1.Text = password;
            Console.WriteLine(password);
        }
    }
}
