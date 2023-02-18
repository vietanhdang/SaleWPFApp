using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for ProductManagement.xaml
    /// </summary>
    public partial class ProductManagement : Window, IActivable
    {
        private readonly IProductRepository productRepository;
        private readonly NavigationService navigationService;
        private List<Product> productList = new List<Product>();

        public ProductManagement(
            NavigationService _navigationService,
            IProductRepository _productRepository
        )
        {
            InitializeComponent();
            productRepository = _productRepository;
            navigationService = _navigationService;
            HandleBeforeLoad();
        }

        private void HandleBeforeLoad()
        {
            LoadProductList(); // Load product list when the window is loaded
            LoadProfileInSession();
            TestComboBox.SelectedIndex = 0; // default search by id
        }

        /// <summary>
        /// Check bussiness action of user
        /// </summary>
        private void LoadProfileInSession()
        {
            try
            {
                var adminProperty = Application.Current.Properties["Admin"];
                if (adminProperty == null)
                {
                    actionCRUD.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Profile");
            }
        }

        /// <summary>
        /// This method is called when the window is loaded and activated
        /// <summary>
        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        private Product GetProductObject()
        {
            Product product = null;
            try
            {
                product = new Product
                {
                    ProductId = String.IsNullOrEmpty(txtProductId.Text)
                        ? 0
                        : int.Parse(txtProductId.Text),
                    ProductName = txtProductName.Text,
                    CategoryId = int.Parse(txtCategoryId.Text),
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.Parse(txtUnitStock.Text)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get product");
            }
            return product;
        }

        /// <summary>
        /// This method is used to update the grid view
        /// </summary>
        private void UpdateGridView()
        {
            this.lvProducts.ItemsSource = null;
            this.lvProducts.ItemsSource = this.productList;
        }

        /// <summary>
        /// This method is used to load the product list
        /// </summary>
        private void LoadProductList()
        {
            this.productList = productRepository.GetAllProducts();
            this.UpdateGridView();
        }

        /// <summary>
        /// This method is used to click the load button
        /// </summary>
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadProductList();
        }

        /// <summary>
        /// This method is used to validate the product information
        /// </summary>
        private bool ValidateProductInfo()
        {
            if (
                String.IsNullOrEmpty(txtProductName.Text)
                || String.IsNullOrEmpty(txtCategoryId.Text)
                || String.IsNullOrEmpty(txtWeight.Text)
                || String.IsNullOrEmpty(txtUnitPrice.Text)
                || String.IsNullOrEmpty(txtUnitStock.Text)
            )
            {
                MessageBox.Show("Please fill all information");
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method is used to click the insert button
        /// </summary>
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateProductInfo())
                {
                    Product product = GetProductObject();
                    product.ProductId = 0;
                    productRepository.CreateProduct(product);
                    this.productList.Add(product);
                    this.UpdateGridView();
                    MessageBox.Show($"{product.ProductName} insert succesfully", "Insert Product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert product");
            }
        }

        /// <summary>
        /// This method is used to click the update button
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateProductInfo())
                {
                    Product product = GetProductObject();
                    productRepository.UpdateProduct(product);
                    this.productList[
                        this.productList.FindIndex(x => x.ProductId == product.ProductId)
                    ] = product;
                    this.UpdateGridView();
                    MessageBox.Show($"{product.ProductName} update succesfully", "Update Product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update product");
            }
        }

        /// <summary>
        /// This method is used to click the delete button
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var productId = txtProductId.Text;
                if (String.IsNullOrEmpty(productId))
                {
                    MessageBox.Show("Please choose product to delete");
                }
                else
                {
                    var Result = MessageBox.Show(
                        "Are you sure to delete?",
                        "Confirm",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                    );
                    if (Result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var product = GetProductObject();
                            productRepository.DeleteProduct(
                                this.productList.FirstOrDefault(
                                    x => x.ProductId == GetProductObject().ProductId
                                )
                            );
                            this.productList.Remove(
                                this.productList.FirstOrDefault(
                                    x => x.ProductId == product.ProductId
                                )
                            );
                            this.UpdateGridView();
                            MessageBox.Show(
                                $"{product.ProductName} delete succesfully",
                                "Delete Product"
                            );
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Delete product");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Can not delete product because it is used in order",
                    "Delete product"
                );
            }
        }

        /// <summary>
        /// This method is handle open member management window
        /// </summary>
        private async void btnMemberManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var isClosed = await navigationService.ShowDialogAsync<MemberManagement>();
            if (!isClosed == true)
            {
                this.Show();
            }
        }

        /// <summary>
        /// This method is handle open order management window
        /// </summary>
        private async void btnOrderManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var isClosed = await navigationService.ShowDialogAsync<OrderManagement>();
            if (!isClosed == true)
            {
                this.Show();
            }
        }

        /// <summary>
        /// This method is handle logout
        /// </summary>
        private async void btnLogout_Click(object sender, RoutedEventArgs e)
        {  
            // remove application cache
            Application.Current.Properties.Remove("Member");
            Application.Current.Properties.Remove("Admin");
            this.Hide();
            var isClosed = await navigationService.ShowDialogAsync<Login>();
            if (!isClosed == true)
            {
                this.Close();
            }
        }

        /// <summary>
        /// This method is used to click the search button
        /// </summary>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var searchValue = txtSearch.Text;
                if (String.IsNullOrEmpty(searchValue))
                {
                    MessageBox.Show("Please enter search value");
                }
                else
                {
                    var searchType = TestComboBox.SelectedIndex;
                    int enumSearch = 1; // default search by product id
                    switch (searchType)
                    {
                        case 1:
                            enumSearch = 2;
                            searchValue = searchValue.ToLower();
                            break;
                        case 2:
                            enumSearch = 3;
                            break;
                        case 3:
                            enumSearch = 4;
                            break;
                        default:
                            enumSearch = 1;
                            break;
                    }
                    this.productList = productRepository.SearchProduct(searchValue, enumSearch);
                    this.UpdateGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search product");
            }
        }

        /// <summary>
        /// This method is used to reset the search value when change the search type
        /// </summary>
        private void cbxChange(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
        }
    }
}
