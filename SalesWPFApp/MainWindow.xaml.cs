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

namespace SalesWPFApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_openMembers_Click(object sender, RoutedEventArgs e)
        {
            MemberWindow windowMembers = new();
            windowMembers.Show();
        }

        private void btn_openProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow windowProducts = new();
            windowProducts.Show();
        }

        private void btn_openOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow windowOrders = new();
            windowOrders.Show();
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow windowLogin = new();
            windowLogin.Show();
            Close();
        }

        private void btn_openReport_Click(object sender, RoutedEventArgs e)
        {
            StatisticWindow windowReport = new();
            windowReport.Show();
        }
    }
}
