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
using MySql.Data.MySqlClient;
using System.Windows.Navigation;

namespace NiceKaffee
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        ConnectionQuery conn = new ConnectionQuery();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT role,UID FROM user where username=@username and password=sha1(@password)", conn.conn);
                cmd.Parameters.AddWithValue("@username", UsernameTxt.Text);
                cmd.Parameters.AddWithValue("@password", PasswordTxt.Password);
                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                MessageBox.Show(reader[0].ToString() + " Logged In"); 
                if(reader[0].ToString() == "Admin") {
                    MainWindow home = new MainWindow();
                    home.Show();
                }
                else
                {
                    CashierMainWindow cwindow = new CashierMainWindow(UsernameTxt.Text, reader[0].ToString(), reader[1].ToString());
                    cwindow.Show();
                }  
                this.Close();
                conn.CloseConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection !" + ex);
            }
        }
    }
}
