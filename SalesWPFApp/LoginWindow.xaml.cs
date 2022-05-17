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
namespace SalesWPFApp
{
    public partial class LoginWindow : Window
    {
        IMemberRepository memberRepository;

        public LoginWindow()
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            errEmail.Text = "";
            errPassword.Text = "";

            if (txtEmail.Text.Length == 0)
            {
                errEmail.Text = "Email cannot be empty.";
                txtEmail.Focus();
                return;
            }

            if (txtPassword.Password.Length == 0)
            {
                errPassword.Text = "Password cannot be empty.";
                txtPassword.Focus();
                return;
            }

            string email = txtEmail.Text;
            string pwd = txtPassword.Password;

            if (memberRepository.IsAdminLogin(email, pwd))
            {
                MainWindow mainWindow = new();
                mainWindow.Show();
                Close();
            }
            else
            {
                Member member = memberRepository.GetMemberLogin(email, pwd);
                if (member != null)
                {
                    // MainWindowMember memWindow = new MainWindowMember(member);
                    // memWindow.lbTitle.Content = "Welcome, " + member.Email.ToString();
                    // memWindow.Show();
                    // Close();
                    Console.WriteLine("here");
                }
                else
                {
                    errPassword.Text = "Email or password is incorrect.";
                }
            }
        }
    }
}
