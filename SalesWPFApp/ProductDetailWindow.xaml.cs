using BusinessObject.BusinessObject;
using DataAccess.Repository;
using System;
using System.Linq;
using System.Windows;

namespace SalesWPFApp {
    public partial class ProductDetailWindow : Window {
        IProductRepository productRepository;
        bool isEdit;
        public ProductDetailWindow(Product _product) {
            InitializeComponent();
            productRepository = new ProductRepository();
            isEdit = false;
            btnDelete.Visibility = Visibility.Hidden;
            cbCategoryId.ItemsSource = productRepository.GetCategoryList();
            cbCategoryId.SelectedIndex = 0;
            if (_product != null) {
                txtProductId.Text = _product.ProductId.ToString();
                cbCategoryId.SelectedIndex = productRepository.GetCategoryList().ToList().FindIndex(i => i == _product.CategoryId);
                txtProductName.Text = _product.ProductName.ToString();
                txtWeight.Text = _product.Weight.ToString();
                txtUnitPrice.Text = _product.UnitPrice.ToString();
                txtUnitsInStock.Text = _product.UnitslnStock.ToString();
                isEdit = true;
                txtProductId.IsEnabled = false;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        private Product GetProduct() {
            Product product = null;
            try {
                product = new Product {
                    ProductId = int.Parse(txtProductId.Text),
                    CategoryId = int.Parse(cbCategoryId.Text),
                    ProductName = txtProductName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitslnStock = int.Parse(txtUnitsInStock.Text),
                };
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Get Product");
            }
            return product;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            try {
                Product product = GetProduct();
                if (isEdit) {
                    productRepository.Update(product);
                    Close();
                    MessageBox.Show($"{product.ProductName} is updated successfully.", "Update Product");
                    return;
                }
                productRepository.Create(product);
                Close();
                MessageBox.Show($"{product.ProductName} is added successfully.", "Insert Product");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Insert Product");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            try {
                Product product = GetProduct();
                productRepository.Delete(product);
                Close();
                MessageBox.Show($"{product.ProductName} is deleted successfully.", "Delete Product");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Delete Product");
            }
        }
    }
}
