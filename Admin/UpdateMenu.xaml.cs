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
    /// Interaction logic for UpdateMenu.xaml
    /// </summary>
    public partial class UpdateMenu : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public UpdateMenu()
        {
            InitializeComponent();
            conn.OpenConnection();
            MySqlDataReader Categories = conn.DataReader("SELECT * FROM category");
            while (Categories.Read())
            {
                CategoriesCB.Items.Add(new ComboBoxItem { Content=Categories.GetString(1), Tag=Categories.GetString(0),});
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

        private void UpdateMenuBtn1_Click(object sender, RoutedEventArgs e)
        {
            string categ = ((ComboBoxItem)this.CategoriesCB.SelectedItem).Tag.ToString();
            try
            {
                FileInfo fi = new FileInfo(ItemImageUpload.FileName);
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("UPDATE items SET itemName=@name, Category=@categ, Price=@price, Image=@image WHERE idItems=@id",conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", ItemName.Text);
                cmd.Parameters.AddWithValue("@price", Price.Text);
                cmd.Parameters.AddWithValue("@categ", categ);
                cmd.Parameters.AddWithValue("@image", ItemNo.Text + "_" + ItemName.Text.Replace(' ', '_') + fi.Extension);
                cmd.Parameters.AddWithValue("@id", ItemNo.Text);
                cmd.ExecuteNonQuery();

                System.IO.File.Copy(ItemImageUpload.FileName, Environment.CurrentDirectory + "/Assets/Items/" + ItemNo.Text + "_" + ItemName.Text.Replace(' ', '_') + fi.Extension, true);
                
                MessageBox.Show("Item Updated Successfully!");

                conn.CloseConnection();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ItemNo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT items.itemName, items.Category, items.Price, items.Image " +
                        "FROM items WHERE idItems= @id", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", ItemNo.Text);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                ItemName.Text = reader.GetString(0);
                CategoriesCB.SelectedIndex = reader.GetInt32(1)-1;
                Price.Text = reader.GetString(2);
                ItemImage.Content = Environment.CurrentDirectory + "/Assets/Items/" + reader.GetString(3);
            }
            catch (Exception ex)
            {
                ItemName.Text = null;
                CategoriesCB.SelectedIndex = -1;
                Price.Text = null;
                ItemImage.Content = null;
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
