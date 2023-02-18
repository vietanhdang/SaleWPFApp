using BusinessObject.Models;
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
    /// <summary>
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Window
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMemberRepository memberRepository;
        private List<Order> orderList = new List<Order>();

        public OrderManagement(
            IOrderRepository _orderRepository,
            IMemberRepository _memberRepository
        )
        {
            orderRepository = _orderRepository;
            memberRepository = _memberRepository;
            InitializeComponent();
            LoadOrderList();
        }

        /// <summary>
        /// This method is called when the window is loaded and activated
        /// <summary>
        public Task ActivateAsync(object parameter)
        {
            var a = parameter;
            return Task.CompletedTask;
        }

        /// <summary>
        /// This method is used to update the grid view
        /// </summary>
        private void UpdateGridView(List<Order> orders = null)
        {
            lvOrders.ItemsSource = null;
            if (orders != null)
            {
                lvOrders.ItemsSource = orders;
            }
            else
            {
                lvOrders.ItemsSource = orderList;
            }
        }

        /// <summary>
        /// This method is used to load the order list
        /// </summary>
        private void LoadOrderList()
        {
            this.orderList = orderRepository.GetAllOrders();
            this.UpdateGridView();
        }

        /// <summary>
        /// This method is used to get order object
        /// </summary>
        private Order GetOrderObject()
        {
            Order order = null;
            try
            {
                order = new Order
                {
                    OrderId = String.IsNullOrEmpty(txtOrderId.Text)
                        ? 0
                        : Convert.ToInt32(txtOrderId.Text),
                    OrderDate = Convert.ToDateTime(txtOrderDate.Text),
                    RequiredDate = Convert.ToDateTime(txtRequiredDate.Text),
                    ShippedDate = Convert.ToDateTime(txtShippedDate.Text),
                    Freight = Convert.ToDecimal(txtFreight.Text),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return order;
        }

        /// <summary>
        /// This method is used to refresh the form
        /// </summary>
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            this.LoadOrderList();
            cbxEndDate.Text = ""; // reset the date range
            cbxStartDate.Text = ""; // reset the date range
        }

        /// <summary>
        /// This method is used to add a new order
        /// </summary>
        private void btnInsert_Click(object sender, RoutedEventArgs e) { }

        /// <summary>
        /// This method is used to update an existing order
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e) { }

        /// <summary>
        /// This method is used to delete an existing order
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e) { }

        /// <summary>
        /// This method is to view details of an order
        /// </summary>
        private void btnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            // lấy ra command parameter
            var order = (sender as Button).CommandParameter as Order;
            // hiển thị thông tin chi tiết của order detail lên MessageBox
            var orderDetails = orderRepository.GetOrderDetails(order.OrderId);
            MessageBox.Show(
                string.Join(Environment.NewLine, orderDetails.Select(x => x.Product.ProductName)),
                "Order Details of Order " + order.OrderId
            );
        }

        /// <summary>
        /// View the detail of the order
        /// </summary>
        private void ViewOrderDetail(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var order = (Order)lvOrders.SelectedItem;
                if (order != null)
                {
                    // hiển thị thông tin chi tiết của order detail lên MessageBox
                    var orderDetails = orderRepository.GetOrderDetails(order.OrderId);
                    int no = 0;
                    var customOrderDetails = orderDetails.Select(
                        x =>
                            new
                            {
                                No = ++no,
                                ProductName = x.Product.ProductName,
                                UnitPrice = x.UnitPrice,
                                Quantity = x.Quantity,
                                Discount = x.Discount,
                                Total = x.UnitPrice * x.Quantity,
                            }
                    );
                    ;
                    lvOrderDetails.ItemsSource = customOrderDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "View Order Detail");
            }
        }

        /// <summary>
        /// This method is used to search an order
        /// </summary>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var startDate = cbxStartDate.SelectedDate;
                var endDate = cbxEndDate.SelectedDate;
                if (startDate != null && endDate != null)
                {
                    var listOrderFilter = orderRepository.GetOrdersByDateRange(
                        startDate.Value,
                        endDate.Value
                    );
                    this.UpdateGridView(listOrderFilter);
                }
                else
                {
                    MessageBox.Show("Please select start date and end date");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Order");
            }
        }

        /// <summary>
        /// Back to the main menu
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
