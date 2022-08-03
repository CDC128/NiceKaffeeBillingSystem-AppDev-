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
using System.Data;
using System.Text.RegularExpressions;

namespace NiceKaffee
{
    /// <summary>
    /// Interaction logic for CashierMainWindow.xaml
    /// </summary>
    public partial class CashierMainWindow : Window
    {
        public string uname { get; set; }
        public string urole { get; set; }
        public string uuid { get; set; }

        ConnectionQuery conn = new ConnectionQuery();
        
        public CashierMainWindow(string username, string role, string UID)
        {
            InitializeComponent();
            dtOrdersTable = OrdersTable();
            UnameLbl.Content = username;
            RoleLbl.Content = role;
            uname = username;
            urole = role;
            uuid = UID;
            conn.OpenConnection();
            MySqlDataReader reader = conn.DataReader("SELECT Image FROM user WHERE UID = " + uuid.ToString());
            reader.Read();
            Image dp = new Image();
            dp.Source = (new BitmapImage(new Uri(Environment.CurrentDirectory + "/Assets/Users/" + reader[0].ToString())));
            ProfilePicUser.Child = dp;
            conn.CloseConnection();
        }
        public DataTable dtOrdersTable { get; set; }

        public DataTable OrdersTable() {
            DataTable orders = new DataTable();
            orders.Columns.Add("ID");
            orders.Columns.Add("Name");
            orders.Columns.Add("Quantity");
            orders.Columns.Add("Price");
            return orders;
        }
    
        public class ShowItems
        {
            Grid Card = new Grid();
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();
            StackPanel ImageStack = new StackPanel();
            StackPanel DetailStack = new StackPanel();
            TextBlock ItemName = new TextBlock();
            Label Category = new Label();
            Label Price = new Label();
            Border ImageBorder = new Border();
            Button CompileButton = new Button();
            Image ImageSrc = new Image();
            public string imgsrc { get; set; }
            public string namelbl { get; set; }
            public string categlbl { get; set; }
            public string pricelbl { get; set; }
            public string itemid { get; set; }
            public Button compile()
            {
                ImageSrc.Source = (new BitmapImage(new Uri(Environment.CurrentDirectory + "/Assets/Items/" + imgsrc)));
                ImageSrc.Width.Equals(96);
                ImageSrc.Height.Equals(96);
                ImageBorder.Child = ImageSrc;
                ImageStack.Children.Add(ImageBorder);
                colDef1.Width = new GridLength(120);
                colDef2.Width = new GridLength(159);
                ImageStack.Margin = new Thickness(10);
                Card.ColumnDefinitions.Add(colDef1);
                Card.ColumnDefinitions.Add(colDef2);
                Grid.SetColumn(ImageStack, 0);
                ItemName.Text = namelbl;
                ItemName.TextWrapping = TextWrapping.WrapWithOverflow;
                ItemName.FontFamily = new FontFamily("Berlin Sans FB");
                ItemName.FontSize = 15;
                Category.Content = categlbl;
                Category.FontFamily = new FontFamily("Berlin Sans FB");
                Price.Content = pricelbl;
                DetailStack.Children.Add(ItemName);
                DetailStack.Children.Add(Category);
                DetailStack.Children.Add(Price);
                DetailStack.Margin = new Thickness(0, 10, 0, 10);
                Grid.SetColumn(DetailStack, 1);
                Card.Children.Add(ImageStack);
                Card.Children.Add(DetailStack);
                CompileButton.Content = Card;
                CompileButton.Background = Brushes.Transparent;
                CompileButton.BorderBrush = Brushes.Transparent;
                CompileButton.Tag = itemid;
                //CompileButton.Click += ItemBtn_Click;
                return CompileButton;
            }

            public void ItemBtn_Click(object sender, RoutedEventArgs e)
            { 
                MessageBox.Show("Item Clicked" + (sender as Button).Tag.ToString());
                
                
            }
            
        }
        
        private void AllMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintMenu(null);       
        }

        private void CoffeeBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintMenu("1");
        }

        private void NonCoffeeBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintMenu("2");
        }

        private void BlendedBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintMenu("3");
        }

        private void BreadBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintMenu("4");
        }

        private void PastaBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintMenu("6");
        }

        private void DessertBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintMenu("5");
        }
        public void PrintMenu(string categ)
        {

            try
            {
                MySqlDataReader results;
                results = null;
                conn.OpenConnection();
                if (categ == null)
                {
                    results =
                    conn.DataReader("SELECT items.idItems, items.itemName, category.Category, items.Price, items.Image " +
                    "FROM items JOIN category on items.Category = category.idCategory");
                }
                else
                {
                    results =
                        conn.DataReader("SELECT items.idItems, items.itemName, category.Category, items.Price, items.Image " +
                        "FROM items JOIN category on items.Category = category.idCategory WHERE items.Category LIKE " + categ);
                }
                int x = 0;
                int y = 0;
                ItemsGrid.Children.Clear();
                ColumnDefinition c1 = new ColumnDefinition();
                c1.Width = new GridLength(280, GridUnitType.Pixel);
                ColumnDefinition c2 = new ColumnDefinition();
                c2.Width = new GridLength(280, GridUnitType.Pixel);
                ItemsGrid.ColumnDefinitions.Add(c1);
                ItemsGrid.ColumnDefinitions.Add(c2);
                while (results.Read())
                {
                    ShowItems item1 = new ShowItems();
                    item1.imgsrc = results.GetString(4);
                    item1.namelbl = results.GetString(1);
                    item1.categlbl = results.GetString(2);
                    item1.pricelbl = results.GetString(3);
                    item1.itemid = results.GetString(0);
                    Border CompileBorder = new Border();
                    CompileBorder.CornerRadius = new CornerRadius(5);
                    CompileBorder.Margin = new Thickness(5, 5, 5, 5);
                    CompileBorder.Background = Brushes.White;
                    CompileBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(132, 60, 20));
                    CompileBorder.BorderThickness = new Thickness(0.3);
                    Button btn1 = item1.compile();
                    btn1.Click += AddOrders;
                    CompileBorder.Child = btn1;
                    Grid.SetRow(CompileBorder, x);
                    Grid.SetColumn(CompileBorder, y);
                    y++;
                    if (y == 2)
                    {
                        ItemsGrid.RowDefinitions.Add(new RowDefinition());
                        x++;
                        y = 0;
                    }
                    this.ItemsGrid.Children.Add(CompileBorder);

                }
                x = 0;
                conn.CloseConnection();
            }
            catch(Exception ex)
            {
                ItemsGrid.Children.Clear();
                MessageBox.Show(ex.Message);    
            }
        }
        float sum;
        private void AddOrders(object sender, RoutedEventArgs e)
        {
            conn.OpenConnection();
            MySqlDataReader results =
                    conn.DataReader("SELECT items.idItems, items.itemName, items.Price " +
                    "FROM items WHERE idItems="+(sender as Button).Tag.ToString()); 
            results.Read();
            /*OrderedItems newitem = new OrderedItems();
            newitem.ItemId = results.GetString(0);
            newitem.ItemName = results.GetString(1);
            newitem.Quantity = "1";
            newitem.Price = results.GestString(2);
            sum += results.GetFloat(2);
            OrderList.Items.Add(newitem);
            TotalBlock.Text = "Total: " + sum.ToString();*/


            DataRow dr = dtOrdersTable.Select("ID=" + results.GetString(0)).FirstOrDefault();
            if(dr != null)
            {
                dr[2] = Int32.Parse(dr[2].ToString()) + 1;
            }
            else
            {
                DataRow row = dtOrdersTable.NewRow();
                row[0] = results.GetString(0);
                row[1] = results.GetString(1);
                row[2] = 1;
                row[3] = results.GetFloat(2);
                dtOrdersTable.Rows.Add(row);
            }

            OrderList.ItemsSource = dtOrdersTable.AsDataView();
            TotalBlock.Text = "Total: " + dtOrdersTable.AsEnumerable().Sum(row => Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Price"])).ToString("#.00");


        }



        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintMenu(null);
        }

        private void AmntPaidTxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal totalamnt = decimal.Parse(dtOrdersTable.AsEnumerable().Sum(row => Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Price"])).ToString("#.00"));
            if (AmntPaidTxtBx.Text == "")
            {
                PrintReceiptBtn.IsEnabled = false;
            }
            else
            {
                PrintReceiptBtn.IsEnabled = true;
            }
        }

        private void PrintReceiptBtn_Click(object sender, RoutedEventArgs e)
        {
            int transactionID = 0;
            decimal totalamnt = dtOrdersTable.AsEnumerable().Sum(row => Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Price"]));
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO transactions(`CashierName`,`TotalPrice`,`AmountPaid`) VALUES " +
                    "(@name,@price,@paid)", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", Int32.Parse(uuid));
                cmd.Parameters.AddWithValue("@price", totalamnt);
                cmd.Parameters.AddWithValue("@paid", float.Parse(AmntPaidTxtBx.Text));
                cmd.ExecuteNonQuery();
                transactionID = Int32.Parse(cmd.LastInsertedId.ToString());
                foreach (DataRow row in dtOrdersTable.Rows)
                {
                    MySqlCommand ordcmd = new MySqlCommand("INSERT INTO orders(`TransactionNum`,`ItemId`,`Quantity`,`Price`) VALUES" +
                        "(@tranID,@item,@qty,@price)",conn.conn);
                    ordcmd.Parameters.Clear();
                    ordcmd.Parameters.AddWithValue("tranID", transactionID);
                    ordcmd.Parameters.AddWithValue("item", Int32.Parse(row[0].ToString()));
                    ordcmd.Parameters.AddWithValue("qty", Int32.Parse(row[2].ToString()));
                    ordcmd.Parameters.AddWithValue("price", Decimal.Parse(row[3].ToString()));
                    ordcmd.ExecuteNonQuery();
                }
                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            var newMyWindow2 = new Receipt();
            newMyWindow2.ReceiptData = dtOrdersTable;
            newMyWindow2.AmntReceived = decimal.Parse(AmntPaidTxtBx.Text);
            newMyWindow2.TransactionID = transactionID;
            newMyWindow2.Show();
            AmntPaidTxtBx.Clear();
            dtOrdersTable.Rows.Clear();
        }


        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void AmntPaidTxtBx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TransactionBtn_Click(object sender, RoutedEventArgs e)
        {
            TransactionsWindow TransacWin = new TransactionsWindow(uname,urole,uuid);
            TransacWin.Show();
            this.Close();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginPage login = new LoginPage();
            login.Show();
            this.Close();
        }
    }
}
