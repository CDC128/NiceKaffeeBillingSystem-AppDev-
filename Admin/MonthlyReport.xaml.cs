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
using System.Data;
using LiveCharts.Wpf;
using LiveCharts;
namespace NiceKaffee.Admin
{
    /// <summary>
    /// Interaction logic for MonthlyReport.xaml
    /// </summary>
    public partial class MonthlyReport : Page
    {
        ConnectionQuery conn = new ConnectionQuery();

        public MonthlyReport()
        {

            conn.OpenConnection();
            MonthlyReporttbl = conn.ShowDataInGridView("SELECT 	MONTH(Date) as MonthNum, SUM(TotalPrice) as Total, COUNT(idTransactions) as Transactions FROM transactions GROUP BY MonthNum");
            conn.CloseConnection();

            double[] ySales = new double[MonthlyReporttbl.Rows.Count];
            int[] yTrans = new int[MonthlyReporttbl.Rows.Count];

            for (int i = 0; i < MonthlyReporttbl.Rows.Count; i++)
            {
                ySales[i] = double.Parse(MonthlyReporttbl.Rows[i][1].ToString());
                yTrans[i] = int.Parse(MonthlyReporttbl.Rows[i][2].ToString());
            }

            Sales_values.AddRange(ySales);
            Transaction_values.AddRange(yTrans);

            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title="Sales",
                    Values = Sales_values
                }
            };
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Transactions",
                Values = Transaction_values
            });

            BarLabels = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            BarFormatter = value => value.ToString("N");

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> BarFormatter { get; set; }
        public DataTable MonthlyReporttbl { get; set; }
        public ChartValues<double> Sales_values = new ChartValues<double> { };
        public ChartValues<int> Transaction_values = new ChartValues<int> { };
    }
    

}
