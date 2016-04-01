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
    public partial class EditAccountWin : MetroWindow
    {
        public static EditAccountWin Instance;
        public EditAccountWin(Account acc)
        {
            InitializeComponent();
            Instance = this;
        }

        private void AAbutton1_Click(object sender, RoutedEventArgs e)
        {

            Account selected = new Account();
            if (textBox2.Text != null)
                selected.username = textBox2.Text;
            if (textBox12.Text != null)
                selected.password = textBox12.Text;
            if (locationTxtBox2.Text != null)
                selected.location = locationTxtBox2.Text;
            MainWindow.Instance.editAccount(selected);
            MainWindow.Instance.updateData();
            MainWindow.Instance.needsSave = true;
        }
    }
}
