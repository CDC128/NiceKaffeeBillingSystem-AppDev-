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
namespace NiceKaffee
{
    /// <summary>
    /// Interaction logic for TransactionLogin.xaml
    /// </summary>
    public partial class TransactionLogin : Window
    {
        ConnectionQuery conn = new ConnectionQuery();
        public string toDelete;
        public TransactionLogin(string toDeleteSource)
        {
            InitializeComponent();
            toDelete = toDeleteSource;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            conn.OpenConnection();
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT role,UID FROM user where username=@username and password=sha1(@password) and role='Admin'", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@username", UsernameTxt.Text);
                cmd.Parameters.AddWithValue("@password", PasswordTxt.Password);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader[0].ToString() == "Admin")
                {
                    MySqlCommand cmd2 = new MySqlCommand("DELETE FROM transactions where idTransactions = @id", conn.conn);
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("@id", toDelete);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully");
                }
                this.Close();
                reader.Close();
                conn.CloseConnection();
            }
            catch(Exception es)
            {
                MessageBox.Show(es.Message);
            }
            }
    }
}
