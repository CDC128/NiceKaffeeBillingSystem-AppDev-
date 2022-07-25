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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        ConnectionQuery conn = new ConnectionQuery();

        public AddUser()
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
                MySqlCommand cmd = new MySqlCommand("INSERT INTO user(username,password,role,Contact,Image) VALUES" +
                    "(@name,sha1(@pword),@role,@cnum,@image)", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", Username.Text);
                cmd.Parameters.AddWithValue("@pword", Password.Password);
                cmd.Parameters.AddWithValue("@role", Role.Text);
                cmd.Parameters.AddWithValue("@cnum", CNum.Text);
                cmd.Parameters.AddWithValue("@image", Username.Text + "_" + Role.Text + fi.Extension);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Created Successfully!");
                conn.CloseConnection();


                System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "/Assets/Users/");
                

                System.IO.File.Copy(UserImageUpload.FileName, Environment.CurrentDirectory + "/Assets/Users/" +Username.Text + "_" + Role.Text + fi.Extension, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UserImage_Click(object sender, RoutedEventArgs e)
        {
            
            //pack://application:,,,

            UserImageUpload.Filter = "Image files| *.jpg;*.png";
            UserImageUpload.Multiselect = false;
            if(UserImageUpload.ShowDialog()== true)
            {
                UserImage.Content = UserImageUpload.FileName;
            }
            
        }
    }
}
