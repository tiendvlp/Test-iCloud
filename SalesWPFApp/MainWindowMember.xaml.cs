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
using DataAccess;
using BusinessObject;
using System.Diagnostics;
using BusinessObject.BusinessObject;

namespace SalesWPFApp {
    public partial class MainWindowMember : Window {
        IOrderRepository orderRepository;
        IMemberRepository memberRepository;
        Member member;
        public MainWindowMember(Member _member) {
            InitializeComponent();
            orderRepository = new OrderRepository();
            memberRepository = new MemberRepository();
            member = _member;
            txtEmail.Text = member.Email;
            txtCompanyName.Text = member.CompanyName;
            txtCity.Text = member.City;
            txtCountry.Text = member.Country;
            txtPassword.Password = member.Password;
            lvOrderHistory.ItemsSource = orderRepository.GetOrdersByMemberId(member.MemberId);
        }

        private void lvOrderHistory_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Order order = (Order)lvOrderHistory.SelectedItem;
            lvOrderDetail.ItemsSource = order.OrderDetails;
            lvOrderDetail.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            string email = txtEmail.Text;
            string companyName = txtCompanyName.Text;
            string city = txtCity.Text;
            string country = txtCountry.Text;
            string pwd = txtPassword.Password;
            Member mem = new Member() {
                MemberId = member.MemberId,
                Email = email,
                CompanyName = companyName,
                City = city,
                Country = country,
                Password = pwd
            };
            try {
                memberRepository.Update(mem);
                MessageBox.Show("Your profile is updated successfully.", "Update Profile");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Update Profile");
            }
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e) {
            LoginWindow loginWindow = new();
            loginWindow.Show();
            Close();
        }
    }
}
