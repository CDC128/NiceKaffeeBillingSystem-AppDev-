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
    /// Interaction logic for DeleteMenu.xaml
    /// </summary>
    public partial class DeleteMenu : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public DeleteMenu()
        {
            InitializeComponent();
            conn.Connection();
        }

        private void ItemSearch(object sender, KeyEventArgs e)
        {
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT items.idItems, items.itemName, category.Category, items.Price, items.Image " +
                    "FROM items JOIN category on items.Category = category.idCategory WHERE items.idItems=@id", conn.conn);
                cmd.Parameters.AddWithValue("@id", ItemNoSearch.Text);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                itemName.Content = reader.GetString(1);
                Category.Content = reader.GetString(2);
                Price.Content = reader.GetString(3);
                ImageBrush testpic = new ImageBrush();
                string imgsrc = "../Assets/items/" + reader.GetString(0) + "_" + reader.GetString(1).Replace(' ', '_') + ".png";
                testpic.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + "/Assets/Items/" + reader.GetString(4)));
                ItemImage.Background = testpic;
                
            }
            catch(Exception ex)
            {
                itemName.Content = null;
                Category.Content = null;
                Price.Content = null;
                ItemImage.Background = null;
            }
            

        }

        private void DeleteMenuBtn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM items where idItems = @id", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", ItemNoSearch.Text);
                cmd.ExecuteNonQuery();
                conn.CloseConnection();
                MessageBox.Show("Deleted Successfully");
                conn.CloseConnection();
                ItemNoSearch.Text = null;
                itemName.Content = null;
                Category.Content = null;
                Price.Content = null;
                ImageBrush testpic = new ImageBrush();
                testpic.ImageSource = null;
                ItemImage.Background = testpic ;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
