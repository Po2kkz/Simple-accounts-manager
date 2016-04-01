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

namespace Simple_Accounts_Manager
{
    /// <summary>
    /// Interaction logic for AddAccountWin.xaml
    /// </summary>
    public partial class AddAccountWin : MetroWindow
    {
        public static AddAccountWin Instance;
        public AddAccountWin()
        {
            InitializeComponent();
            Instance = this;
        }

        private void AAbutton1_Click(object sender, RoutedEventArgs e)
        {
            String username = textBox.Text;
            String password = textBox1.Text;
            String location = locationTxtBox.Text;
            Manager.accounts.Add(new Account(username,password,location));
            MainWindow.Instance.updateData();
            MainWindow.Instance.needsSave = true;
        }
    }
}
