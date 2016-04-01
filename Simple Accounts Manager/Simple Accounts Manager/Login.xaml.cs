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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public static bool isFirstTime;
        public static bool waitForButton = false;
        public Login()
        {
            InitializeComponent();
            if(!Manager.isFirstTimeJoin())
            {
                label1.Visibility = Visibility.Collapsed;
                passwordBox1.Visibility = Visibility.Collapsed;
                button.Content = "Login";
            }
            else
            {
                waitForButton = true;
            }
        }

        void openMainWindow()
        {
            if (MainWindow.Instance == null)
            {
                MainWindow.Instance = new MainWindow();
                MainWindow.Instance.Show();
                MainWindow.Instance.Closed += (o, a) => { MainWindow.Instance = null; };
            }
            else if (MainWindow.Instance != null && !MainWindow.Instance.IsActive)
                MainWindow.Instance.Activate();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            bool isValid = true;
            foreach (char c in passwordBox.Password)
            {
                if (c.Equals(char.MinValue))
                {
                    i++;
                }
            }
            if(i>=1)
            {

                label2.Content = "Password can't contain a null character (\\ + 0) ";
                isValid = false;
            }
            if (!passwordBox.Password.Equals(passwordBox1.Password) && waitForButton)
            {
                label2.Content = "Passwords don't match!";
            }
            if (passwordBox.Password.Equals(passwordBox1.Password) && waitForButton && isValid)
            {
                Manager.user.password = Manager.Encrypt(passwordBox1.Password, passwordBox1.Password );
                Manager.WriteToUserFile();
                openMainWindow();
                this.Close();
            }
            if (!waitForButton)
            {
                String password = Manager.Decrypt(Manager.ReadPasswordFromUserData(), passwordBox.Password);
                Manager.user.Decryptedpassword = password;

                if (password.Equals(passwordBox.Password))
                {
                    openMainWindow();
                    this.Close();
                }
                else
                {
                    label2.Content = "Incorrect password!";
                }
            }
        }
    }
}
