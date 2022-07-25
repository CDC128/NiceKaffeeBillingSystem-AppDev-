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

namespace NiceKaffee.Admin
{
    /// <summary>
    /// Interaction logic for DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public DeleteUser()
        {
            InitializeComponent();
            conn.Connection();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT user.Image,user.username,user.role " +
                        "FROM user WHERE UID= @id", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", UserSearch.Text);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                //UserImg.Text = reader.GetString(0);
                UserName.Content = reader.GetString(1);
                Role.Content = reader.GetString(2);
                ImageBrush testpic = new ImageBrush();
                //string imgsrc = "pack://application:,,," + reader.GetString(0);
                testpic.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory+ "/Assets/Users/"+reader.GetString(0)));
                UserImage.Background = testpic;

            }
            catch (Exception ex)
            {
                UserName.Content = null;
                Role.Content = null;
                UserImage.Background = null;
            }
        }

        private void DeleteMenuBtn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM user where UID = @id", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", UserSearch.Text);
                cmd.ExecuteNonQuery();
                conn.CloseConnection();
                MessageBox.Show("Deleted Successfully");
                conn.CloseConnection();
                UserSearch.Text = null;
                UserName.Content = null;
                Role.Content = null;
                UserImage.Background = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
