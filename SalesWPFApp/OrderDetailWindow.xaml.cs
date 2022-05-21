using BusinessObject;
using BusinessObject.BusinessObject;
using DataAccess.Repository;
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

namespace SalesWPFApp {
    public partial class OrderDetailWindow : Window {
        IOrderRepository orderRepository;
        bool isEdit;
        public OrderDetailWindow(Order _order) {
            InitializeComponent();
            orderRepository = new OrderRepository();
            isEdit = false;
            btnDelete.Visibility = Visibility.Hidden;
            if (_order != null) {
                txtOrderId.Text = _order.OrderId.ToString();
                txtMemberId.Text = _order.MemberId.ToString();
                txtOrderDate.Text = _order.OrderDate.ToString();
                txtRequiredDate.Text = _order.RequiredDate.ToString();
                txtShippedDate.Text = _order.ShippedDate.ToString();
                txtFreight.Text = _order.Freight.ToString();
                isEdit = true;
                txtOrderId.IsEnabled = false;
                txtMemberId.IsEnabled = false;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        private Order GetOrder() {
            Order order = null;
            int id;
            try {
                if (isEdit) {
                    id = int.Parse(txtOrderId.Text);
                } else {
                    id = orderRepository.GetOrderList().Count() + 1;
                }
                order = new Order {
                    OrderId = id,
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = DateTime.Parse(txtOrderDate.Text),
                    ShippedDate = DateTime.Parse(txtShippedDate.Text),
                    RequiredDate = DateTime.Parse(txtRequiredDate.Text),
                    Freight = decimal.Parse(txtFreight.Text)
                };
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Get Order");
            }
            return order;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            try {
                Order order = GetOrder();
                if (isEdit) {
                    orderRepository.Update(order);
                    Close();
                    MessageBox.Show($"{order.OrderId} is updated successfully.", "Update Order");
                    return;
                }
                orderRepository.Create(order);
                Close();
                MessageBox.Show($"{order.OrderId} is inserted successfully.", "Insert Order");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Insert Order");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            try {
                Order order = GetOrder();
                orderRepository.Delete(order);
                Close();
                MessageBox.Show($"{order.OrderId} is deleted successfully.", "Delete Order");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Delete Order");
            }
        }
    }
}
