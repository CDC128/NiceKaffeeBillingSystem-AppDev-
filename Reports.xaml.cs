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

namespace NiceKaffee
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
            ReportsTitle.Text = "Monthly Summary";
            ReportsContent.Content = new Admin.MonthlyReport();
        }

        private void MonthlySummaryBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportsTitle.Text = "Monthly Summary";
            ReportsContent.Content = new Admin.MonthlyReport();
        }

        private void ProductReportBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportsTitle.Text = "Product Report";
            ReportsContent.Content = new Admin.ProductReport();
        }
    }
}
