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
using System.Data;
using MySql.Data.MySqlClient;

namespace NiceKaffee
{
    /// <summary>
    /// Interaction logic for Receipt.xaml
    /// </summary>
    public partial class Receipt : Window
    {
        public DataTable ReceiptData { get; set; }
        public decimal AmntReceived { get; set; }
        public int TransactionID { get; set; }
        public int CashierID { get; set; }
        
        ConnectionQuery conn = new ConnectionQuery();
        public Receipt()
        {
            InitializeComponent();
        }

        private void CloseCommand(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReceiptOrders_Loaded(object sender, RoutedEventArgs e)
        {
            if (TransactionID != null)
            {
                conn.OpenConnection();
                ReceiptData = conn.ShowDataInGridView("SELECT orders.ItemId, items.itemName,orders.Quantity,orders.Price FROM " +
                    "orders JOIN items on orders.ItemId = items.idItems " +
                    "WHERE TransactionNum = " + TransactionID.ToString());
                conn.CloseConnection();
            }
            TransIDTxtBlk.Text = "Transaction #: " + TransactionID.ToString();
            conn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT user.username, transactions.date FROM transactions JOIN user on transactions.CashierName=user.UID WHERE idTransactions=@id ", conn.conn);
            cmd.Parameters.AddWithValue("@id", TransactionID);
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            CnameTxtBlk.Text = reader[0].ToString();
            DateTxtBlk.Text = reader[1].ToString();

            int x=0;
            foreach (DataRow row in ReceiptData.Rows){
                TextBlock qty = new TextBlock();
                qty.Text = row[3].ToString();
                qty.HorizontalAlignment = HorizontalAlignment.Center;
                QtySP.Children.Add(qty);

                TextBlock name = new TextBlock();
                name.Text = row[1].ToString();
                name.HorizontalAlignment = HorizontalAlignment.Center;
                name.TextWrapping = TextWrapping.Wrap;
                NameSP.Children.Add(name);

                TextBlock price = new TextBlock();
                price.Text = row[2].ToString();
                price.HorizontalAlignment = HorizontalAlignment.Center;
                PriceSP.Children.Add(price);

                x++;
            }
            decimal AmntPayable = ReceiptData.AsEnumerable().Sum(row => Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Price"]));
            TotalPriceTB.Text = AmntPayable.ToString("#.00");
            AmntPaidTB.Text = AmntReceived.ToString("#.00");
            ChangeTB.Text = (AmntReceived- AmntPayable).ToString("#.00");
        }

    }
}
