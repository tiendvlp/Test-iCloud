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
using BusinessObject;
using BusinessObject.BusinessObject;
using DataAccess.Repository;

namespace SalesWPFApp {
    public partial class OrdersWindow : Window {
        IOrderRepository orderRepository;
        public OrdersWindow() {
            InitializeComponent();
            orderRepository = new OrderRepository();
            LoadOrderList();
        }

        public void LoadOrderList() {
            lvOrders.ItemsSource = orderRepository.GetOrderList();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e) {
            try {
                LoadOrderList();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load Order List");
            }
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            WindowOrderDetail orderDialog = new(null);
            orderDialog.ShowDialog();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void lvOrders_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            try {
                Order order = (Order)lvOrders.SelectedItem;
                if (order != null) {
                    lvOrderDetails.ItemsSource = order.OrderDetails;
                    lvOrderDetails.Items.Refresh();
                } else {
                    WindowOrderDetail orderDialog = new(null);
                    orderDialog.ShowDialog();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "View Orders");
            }
        }

        private void lvOrders_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Order prod = (Order)lvOrders.SelectedItem;
            WindowOrderDetail orderDialog = new(prod);
            orderDialog.ShowDialog();
        }
    }
}
