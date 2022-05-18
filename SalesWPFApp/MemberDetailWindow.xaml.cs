using BusinessObject.BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows;


namespace SalesWPFApp {
    public partial class MemberDetailWindow : Window {
        IMemberRepository memberRepository;
        bool isEdit;
        public MemberDetailWindow(Member _member) {
            InitializeComponent();
            memberRepository = new MemberRepository();
            isEdit = false;
            btnDelete.Visibility = Visibility.Hidden;
            if (_member != null) {
                txtMemberId.Text = _member.MemberId.ToString();
                txtEmail.Text = _member.Email.ToString();
                txtCompanyName.Text = _member.CompanyName.ToString();
                txtCity.Text = _member.City.ToString();
                txtCountry.Text = _member.Country.ToString();
                txtPassword.Text = _member.Password.ToString();
                isEdit = true;
                txtMemberId.IsEnabled = false;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        private Member GetMember() {
            Member member = null;
            try {
                member = new Member {
                    MemberId = int.Parse(txtMemberId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text
                };
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Get Member");
            }
            return member;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            try {
                Member member = GetMember();
                if (isEdit) {
                    memberRepository.Update(member);
                    Close();
                    MessageBox.Show($"{member.Email} is updated successfully.", "Update Member");
                    return;
                }
                memberRepository.Create(member);
                Close();
                MessageBox.Show($"{member.Email} is added successfully.", "Insert Member");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Insert Member");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            try {
                Member member = GetMember();
                memberRepository.Delete(member);
                Close();
                MessageBox.Show($"{member.Email} is deleted successfully.", "Delete Member");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Delete Member");
            }
        }
    }
}
