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
    /// Interaction logic for ViewUser.xaml
    /// </summary>
    public partial class ViewUser : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public ViewUser()
        {
            InitializeComponent();
            try
            {

                conn.OpenConnection();
                MySqlDataReader results =
                    conn.DataReader("SELECT UID, username, role, Contact " +
                    "FROM user ");

                while (results.Read())
                {
                    Users newitem = new Users();
                    newitem.UserId = results.GetString(0);
                    newitem.UserName = results.GetString(1);
                    newitem.Role = results.GetString(2);
                    newitem.Cnum = results.GetString(3);

                    ItemList.Items.Add(newitem);

                }

            }
            catch (Exception ex)
            {

            }
        }
        public class Users
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Role { get; set; }
            public string Cnum { get; set; }

        }
    }
}
