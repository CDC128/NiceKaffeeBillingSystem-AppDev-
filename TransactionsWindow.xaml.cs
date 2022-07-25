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
    /// Interaction logic for TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        public string uname { get; set; }
        public string urole { get; set; }
        public string uuid { get; set; }
        ConnectionQuery conn = new ConnectionQuery();
        public DataTable TransactionsTable { get; set; }
        public TransactionsWindow(string username, string role, string UID)
        {
            InitializeComponent();
            UnameLbl.Content = username;
            RoleLbl.Content = role;
            uname = username;
            urole = role;
            conn.OpenConnection();
            TransactionsTable = conn.ShowDataInGridView("SELECT idTransactions as ID, Date, TotalPrice as Price, AmountPaid as Paid FROM transactions");
            conn.CloseConnection();
            TransactionsDG.ItemsSource = TransactionsTable.DefaultView;
            uuid = UID;
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            var HistReceipt = new Receipt();
            HistReceipt.TransactionID = Int32.Parse(dataRowView[0].ToString());
            HistReceipt.Show();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM transactions where idTransactions = @id", conn.conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", IdDeleteTxt.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully");
                TransactionsTable = conn.ShowDataInGridView("SELECT idTransactions as ID, Date, TotalPrice as Price, AmountPaid as Paid FROM transactions");
                conn.CloseConnection();
                TransactionsDG.ItemsSource = TransactionsTable.DefaultView;
                IdDeleteTxt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {

            CashierMainWindow MenuWin = new CashierMainWindow(uname,urole,uuid);
            MenuWin.Show();
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
