using BusinessObject.Models;
using DataAccess.Repository;
using Newtonsoft.Json;
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
    /// <summary>
    /// Interaction logic for ProfileManagement.xaml
    /// </summary>
    public partial class ProfileManagement : Window
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly NavigationService _navigationService;
        private Member _member;

        public ProfileManagement(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IMemberRepository memberRepository,
            NavigationService navigationService
        )
        {
            InitializeComponent();
            _orderRepository = orderRepository;
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            _navigationService = navigationService;
            HandleBeforeLoad();
        }

        private void HandleBeforeLoad()
        {
            LoadProfileInSession();
            LoadOrderList();
        }

        /// <summary>
        /// This method is called when the window is loaded and activated
        /// <summary>
        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Load the profile of the member in Application
        /// </summary>
        /// <returns></returns>
        private void LoadProfileInSession()
        {
            try
            {
                var memberProperty = Application.Current.Properties["Member"];
                if (memberProperty != null)
                {
                    var member = JsonConvert.DeserializeObject<Member>(memberProperty.ToString());
                    txtEmail.Text = member.Email;
                    txtCompany.Text = member.CompanyName;
                    txtCity.Text = member.City;
                    txtPassword.Text = member.Password;
                    txtCountry.Text = member.Country;
                    _member = member;
                }
                else
                {
                    MessageBox.Show("You need to login first", "Login");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Profile");
            }
        }

        /// <summary>
        /// This method is used to Get the profile object
        /// </summary>
        private Member GetProfileObject()
        {
            try
            {
                Member member = new Member
                {
                    Email = txtEmail.Text,
                    CompanyName = txtCompany.Text,
                    City = txtCity.Text,
                    Password = txtPassword.Text,
                    Country = txtCountry.Text
                };
                return member;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Profile");
            }
            return null;
        }

        /// <summary>
        /// Validate the profile of the member
        /// </summary>
        /// <returns></returns>
        private bool ValidateProfile()
        {
            try
            {
                if (
                    String.IsNullOrEmpty(txtEmail.Text)
                    || String.IsNullOrEmpty(txtCompany.Text)
                    || String.IsNullOrEmpty(txtCity.Text)
                    || String.IsNullOrEmpty(txtPassword.Text)
                    || String.IsNullOrEmpty(txtCountry.Text)
                )
                {
                    MessageBox.Show("Please fill all the fields", "Validate Profile");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validate Profile");
            }
            return true;
        }

        /// <summary>
        /// Update the profile of the member
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateProfile())
                {
                    return;
                }
                else
                {
                    Member member = GetProfileObject();
                    member.MemberId = _member.MemberId;
                    _memberRepository.UpdateMember(member);
                    Application.Current.Properties["Member"] = JsonConvert.SerializeObject(member);
                    LoadProfileInSession();
                    MessageBox.Show("Your profile was update succesfully", "Update Profile");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update profile");
            }
        }

        /// <summary>
        /// View the list of the orders
        /// </summary>
        private void LoadOrderList()
        {
            try
            {
                var orders = _orderRepository.GetOrdersOfUser(_member.MemberId);
                if (orders != null)
                {
                    lstOrders.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Order List");
            }
        }

        /// <summary>
        /// View the detail of the order
        /// </summary>
        private void ViewOrderDetail(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var order = (Order)lstOrders.SelectedItem;
                if (order != null)
                {
                    // hiển thị thông tin chi tiết của order detail lên MessageBox
                    var orderDetails = _orderRepository.GetOrderDetails(order.OrderId);
                    lstOrderDetail.ItemsSource = orderDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "View Order Detail");
            }
        }
    }
}
