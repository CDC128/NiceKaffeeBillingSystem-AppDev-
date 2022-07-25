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
using System.IO;
using Microsoft.Win32;

namespace NiceKaffee.Admin
{
    /// <summary>
    /// Interaction logic for UpdateUser.xaml
    /// </summary>
    public partial class UpdateUser : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public UpdateUser()
        {
            InitializeComponent();
            UserImageUpload = Image();
        }
        public OpenFileDialog UserImageUpload { get; set; }

        public OpenFileDialog Image()
        {
            OpenFileDialog img = new OpenFileDialog();

            return img;
        }

        private void UpdateMenuBtn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(UserImageUpload.FileName);
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("UPDATE user SET username=@name, password=sha1(@password), role=@role, Contact=@contact, Image=@image WHERE UID=@id", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", UserName.Text);
                cmd.Parameters.AddWithValue("@password", Password.Password);
                cmd.Parameters.AddWithValue("@role", Role.Text);
                cmd.Parameters.AddWithValue("@contact", Contact.Text);
                cmd.Parameters.AddWithValue("@image", UserName.Text + "_" + Role.Text + fi.Extension);
                cmd.Parameters.AddWithValue("@id", UID.Text);
                cmd.ExecuteNonQuery();


                System.IO.File.Copy(UserImageUpload.FileName, Environment.CurrentDirectory + "/Assets/Users/" + UserName.Text + "_" + Role.Text + fi.Extension, true);

                MessageBox.Show("User Updated Successfully!");
                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UID_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT user.Image,user.username,user.role,user.Contact " +
                        "FROM user WHERE UID= @id", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", UID.Text);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                UserImg.Content = Environment.CurrentDirectory + "/Assets/Users/" + reader.GetString(0); ;
                UserName.Text = reader.GetString(1);
                Role.Text = reader.GetString(2);
                Contact.Text = reader.GetString(3);
            }
            catch (Exception ex)
            {
                UserImg.Content = null;
                UserName.Text = null;
                Role.Text = null;
                Contact.Text = null;
            }
        }

        private void UserImg_Click(object sender, RoutedEventArgs e)
        {
            UserImageUpload.Filter = "Image files| *.jpg;*.png";
            UserImageUpload.Multiselect = false;
            if (UserImageUpload.ShowDialog() == true)
            {
                UserImg.Content = UserImageUpload.FileName;
            }
        }
    }
}
