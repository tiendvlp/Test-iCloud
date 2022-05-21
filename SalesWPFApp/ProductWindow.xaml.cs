using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BusinessObject.BusinessObject;
using DataAccess.Repository;

namespace SalesWPFApp {
    public partial class ProductWindow : Window {
        IProductRepository productRepository;
        public ProductWindow() {
            InitializeComponent();
            productRepository = new ProductRepository();
            LoadProductList();
        }

        public void LoadProductList() {
            lvProducts.ItemsSource = productRepository.GetProductList();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e) {
            try {
                LoadProductList();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e) {
            ProductDetailWindow ProductDialog = new(null);
            ProductDialog.ShowDialog();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void lvProducts_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            Product prod = (Product)lvProducts.SelectedItem;
            ProductDetailWindow ProductDialog = new(prod);
            ProductDialog.ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e) {
            if (txtSearch.Text != null) {
                try {
                    IEnumerable<Product> result = productRepository.GetProductByKeyword(txtSearch.Text); 
                    if (!result.Any()) {
                        MessageBox.Show("There is no matched record.", "Search Product");
                    } else {
                        lvProducts.ItemsSource = result;
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Search Product");
                }
            } else {
                LoadProductList();
            }
        }
    }
}
