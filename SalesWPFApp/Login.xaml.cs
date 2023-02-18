using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly NavigationService _navigationService;
        private readonly IMemberRepository _memberRepository;

        public Login(NavigationService navigationService, IMemberRepository memberRepository)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPass.Text;
            var a = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("account");
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                if (email == a["email"] && password == a["password"])
                {
                    Application.Current.Properties["Admin"] = "true";

                }
                else
                {
                    var member = _memberRepository.GetMemberByEmailAndPassword(email, password);
                    if (member != null)
                    {
                        // lưu thông tin member vào session
                        Application.Current.Properties["Member"] = JsonConvert.SerializeObject(
                            member
                        );
                    }
                    else
                    {
                        MessageBox.Show("Email or password is incorrect");
                        return;
                    }
                }
                this.Hide();
                var isClosed = await _navigationService.ShowDialogAsync<ProductManagement>();
                if (!isClosed == true)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter email and password");
                return;
            }
        }
    }
}
