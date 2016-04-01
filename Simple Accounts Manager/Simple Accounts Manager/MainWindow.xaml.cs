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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using static Simple_Accounts_Manager.AddAccountWin;
namespace Simple_Accounts_Manager
{
    public partial class MainWindow : MetroWindow
    {
        private bool cancel = true;
        public static MainWindow Instance;
        public bool needsSave = false;
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            dataGrid.ItemsSource = Manager.accounts;
            updateData();
        }
        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            if(AddAccountWin.Instance == null)
            {
                AddAccountWin.Instance = new AddAccountWin();
                AddAccountWin.Instance.Show();
                AddAccountWin.Instance.Closed += (o, a) => { AddAccountWin.Instance = null; };
            }
            else if(AddAccountWin.Instance != null && !AddAccountWin.Instance.IsActive)
            AddAccountWin.Instance.Activate();
        }
        public void updateData()
        {
            dataGrid.Items.Refresh();
        }
        public async void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            Manager.SaveToFile();
            await this.ShowMessageAsync("Saved",Manager.accounts.Count + " accounts succesfully saved!");
        }

        private async void onClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cancel && needsSave)
            e.Cancel = cancel;
            MetroDialogSettings settings = new MetroDialogSettings();
            settings.AffirmativeButtonText = "Yes & exit";
            settings.NegativeButtonText = "No & exit";
            settings.FirstAuxiliaryButtonText = "Cancel";
            MessageDialogResult result =  await this.ShowMessageAsync("Are you sure you want to quit?", "Save your accounts now?", MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary,settings);
            if(result == MessageDialogResult.Affirmative)
            {
                Manager.SaveToFile();
                cancel = false;
                this.Close();
            }
            else if(result == MessageDialogResult.Negative)
            {
                cancel = false;
                this.Close();
            }
            else
                cancel = true;
        }

        private void DeleteAccount(object sender, RoutedEventArgs routedEventArgs)
        {
            Manager.accounts.Remove((Account) dataGrid.SelectedItem);
            needsSave = true;
            dataGrid.Items.Refresh();
        }

        private void EditAccount(object sender, RoutedEventArgs routedEventArgs)
        {
            if (EditAccountWin.Instance == null)
            {

                EditAccountWin.Instance = new EditAccountWin((Account) dataGrid.SelectedItem);
                EditAccountWin.Instance.Show();
                EditAccountWin.Instance.Closed += (o, a) => { EditAccountWin.Instance = null; };
            }
            else if (EditAccountWin.Instance != null && !EditAccountWin.Instance.IsActive)
                EditAccountWin.Instance.Activate();
        }
        private void GeneratePassword_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            if (PwGenWin.Instance == null)
            {
                PwGenWin.Instance = new PwGenWin();
                PwGenWin.Instance.Show();
                PwGenWin.Instance.Closed += (o, a) => { PwGenWin.Instance = null; };
            }
            else if (PwGenWin.Instance != null && !PwGenWin.Instance.IsActive)
                PwGenWin.Instance.Activate();
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            Manager.LoadFromFile();
        }

        public void editAccount(Account acc)
        {
            Account acc2;
            Account selected = (Account)dataGrid.SelectedItem;
            acc2 = Manager.accounts.ElementAt(Manager.accounts.FindIndex(a => a == selected));
            acc2.username = acc.username;
            acc2.password = acc.password;
            acc2.location = acc.location;

        }
    }
}
