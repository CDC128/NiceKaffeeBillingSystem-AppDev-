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
    /// Interaction logic for AddMenu.xaml
    /// </summary>
    public partial class AddMenu : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public AddMenu()
        {
            InitializeComponent();
            conn.OpenConnection();
            MySqlDataReader Categories = conn.DataReader("SELECT * FROM category");
            while (Categories.Read())
            {
                CategoriesCB.Items.Add(new ComboBoxItem { Content = Categories.GetString(1), Tag = Categories.GetString(0), });
            }
            conn.CloseConnection();
            ItemImageUpload = Image();
        }
        public OpenFileDialog ItemImageUpload { get; set; }

        public OpenFileDialog Image()
        {
            OpenFileDialog img = new OpenFileDialog();

            return img;
        }

        private void AddMenuBtn1_Click(object sender, RoutedEventArgs e)
        {
            string categ = ((ComboBoxItem)this.CategoriesCB.SelectedItem).Tag.ToString();
            try
            {
                FileInfo fi = new FileInfo(ItemImageUpload.FileName);
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO items(itemName,Category,Price) VALUES" +
                    "(@name,@categ,@price)", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", ItemName.Text);
                cmd.Parameters.AddWithValue("@price", Price.Text);
                cmd.Parameters.AddWithValue("@categ", categ);
                cmd.ExecuteNonQuery();
                MySqlCommand cmd2 = new MySqlCommand("UPDATE items SET Image = @image WHERE idItems = @id");
                cmd.Parameters.Clear();
                cmd2.Parameters.AddWithValue("@image", cmd.LastInsertedId.ToString() + "_" + ItemName.Text.Replace(' ', '_') + fi.Extension);
                cmd2.Parameters.AddWithValue("@id", cmd.LastInsertedId);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Item Created Successfully!");
                conn.CloseConnection();

                System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "/Assets/Items/");

                System.IO.File.Copy(ItemImageUpload.FileName, Environment.CurrentDirectory + "/Assets/Items/" + cmd.LastInsertedId.ToString() + "_" + ItemName.Text.Replace(' ', '_') + fi.Extension, true);
                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ItemImage_Click(object sender, RoutedEventArgs e)
        {
            ItemImageUpload.Filter = "Image files| *.jpg;*.png";
            ItemImageUpload.Multiselect = false;
            if (ItemImageUpload.ShowDialog() == true)
            {
                ItemImage.Content = ItemImageUpload.FileName;
            }
        }
    }
}
