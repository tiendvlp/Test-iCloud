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
    public partial class MemberWindow : Window {
        IMemberRepository memberRepository;

        public MemberWindow() {
            InitializeComponent();
            memberRepository = new MemberRepository();
            LoadMemberList();
        }

        public void LoadMemberList() {
            lvMembers.ItemsSource = memberRepository.GetMembersList();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e) {
            try {
                LoadMemberList();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load Member List");
            }
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            WindowMemberDetail memberDialog = new(null);
            memberDialog.ShowDialog();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void lvMembers_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            Member mem = (Member)lvMembers.SelectedItem;
            WindowMemberDetail memberDialog = new(mem);
            memberDialog.ShowDialog();
        }
    }
}
