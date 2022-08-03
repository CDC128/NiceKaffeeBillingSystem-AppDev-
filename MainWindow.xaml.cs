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
using MySql.Data.MySqlClient;

namespace NiceKaffee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionQuery conn = new ConnectionQuery();
        public string uuid { get; set; }
        public MainWindow(string UID)
        {
            InitializeComponent();
            PageContent.Content = new HomeViewMenu();
            uuid = UID;
        }

        private void ReportsBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = new Reports();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = new HomeViewMenu();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginPage login = new LoginPage();
            login.Show();
            this.Close();

        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = new Settings(uuid);         
        }
    }
}
