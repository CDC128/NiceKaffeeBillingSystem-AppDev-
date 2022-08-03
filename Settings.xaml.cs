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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public string uuid { get; set; }
        public Settings(string UID)
        {
            InitializeComponent();
            uuid = UID;
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            if (PwordPbox.Password != RePwordPbox.Password)
                MessageBox.Show("New Password do not match!");
            if (PwordPbox.Password == RePwordPbox.Password && PwordOld!= null)
            {
                try
                {
                    conn.OpenConnection();
                    MySqlCommand cmd = new MySqlCommand("UPDATE user SET password=sha1(@password) WHERE UID=@id and password=sha1(@oldpw)", conn.conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@password", PwordPbox.Password);
                    cmd.Parameters.AddWithValue("@id", Int32.Parse(uuid));
                    cmd.Parameters.AddWithValue("@oldpw", PwordOld.Password);
                    cmd.ExecuteNonQuery();
                    conn.CloseConnection();
                    MessageBox.Show("Password Succesfully Changed!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
