using BusinessObject.BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows;
using System.Windows.Input;

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
            MemberDetailWindow memberDialog = new(null);
            memberDialog.ShowDialog();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void lvMembers_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            Member mem = (Member)lvMembers.SelectedItem;
            MemberDetailWindow memberDialog = new(mem);
            memberDialog.ShowDialog();
        }

        private void btnSearchById_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int ID = Int32.Parse(idTextbox.Text);
                lvMembers.ItemsSource = memberRepository.GetMemberById(ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member By ID");
            }

        }

        private void btnSearchByEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string email = emailTextbox.Text;
                lvMembers.ItemsSource = memberRepository.GetMemberByEmail(email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member By Name");
            }
        }
    }
}
