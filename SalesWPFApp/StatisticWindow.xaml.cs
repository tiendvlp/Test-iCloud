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
using DataAccess.Repository;

namespace SalesWPFApp {
    public partial class StatisticWindow : Window {

        IOrderRepository orderRepository;

        public StatisticWindow() {
            InitializeComponent();
            orderRepository = new OrderRepository();
        }

        private void btn_viewSales_Click(object sender, RoutedEventArgs e) {
            if (dp_start.Text.Length == 0) {
                MessageBox.Show("Please enter start date.", "View Report");
            } else if (dp_end.Text.Length == 0) {
                MessageBox.Show("Please enter end date.", "View Report");
            } else {
                try {
                    DateTime start = DateTime.Parse(dp_start.Text);
                    DateTime end = DateTime.Parse(dp_end.Text);
                    var result = orderRepository.ViewReport(start, end);
                    if (result.Any()) {
                        lvSales.ItemsSource = result;
                    } else {
                        MessageBox.Show("There is no matched record.", "View Report");
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "View Report");
                }   
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
