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
    /// Interaction logic for ProductReport.xaml
    /// </summary>
    public partial class ProductReport : Page
    {
        ConnectionQuery conn = new ConnectionQuery();
        public ProductReport()
        {
            conn.OpenConnection();
            ProductReporttbl = conn.ShowDataInGridView("SELECT itemName, SUM(Quantity) as Popularity from orders JOIN items on orders.ItemId=items.idItems GROUP by ItemId ORDER BY Popularity DESC LIMIT 5");
            conn.CloseConnection();

            int[] ySales = new int[ProductReporttbl.Rows.Count];
            string[] yNames = new string[ProductReporttbl.Rows.Count];

            for (int i = 0; i < ProductReporttbl.Rows.Count; i++)
            {
                ySales[i] = Int32.Parse(ProductReporttbl.Rows[i][1].ToString());
                yNames[i] = ProductReporttbl.Rows[i][0].ToString();
            }

            Sales_values.AddRange(ySales);
            Name_values.AddRange(yNames);

            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Sales",
                    Values = Sales_values
                }
            };

            BarLabels = yNames;
            BarFormatter = value => value.ToString("N");

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> BarFormatter { get; set; }
        public DataTable ProductReporttbl { get; set; }
        public ChartValues<int> Sales_values = new ChartValues<int> { };
        public ChartValues<string> Name_values = new ChartValues<string> { };
    }
}
