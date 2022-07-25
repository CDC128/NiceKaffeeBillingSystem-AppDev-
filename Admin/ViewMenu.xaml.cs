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
    /// Interaction logic for ViewMenu.xaml
    /// </summary>
    public partial class ViewMenu : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public ViewMenu()
        {
            InitializeComponent();
            try
            {
                
                conn.OpenConnection();
                MySqlDataReader results =
                    conn.DataReader("SELECT items.idItems, items.itemName, category.Category, items.Price " +
                    "FROM items JOIN category on items.Category = category.idCategory ");
 
                while (results.Read())
                {
                    Items newitem = new Items();
                    newitem.ItemId = results.GetString(0);
                    newitem.ItemName = results.GetString(1);
                    newitem.Category =  results.GetString(2);
                    newitem.Price = results.GetString(3);

                    ItemList.Items.Add(newitem);

                }

            }
            catch(Exception ex)
            {

            }
        }
        public class Items
        {
            public string ItemId { get; set; }
            public string ItemName { get; set; }
            public string Category { get; set; }
            public string Price { get; set; }

        }
    }
}
