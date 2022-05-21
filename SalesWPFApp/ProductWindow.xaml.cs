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

        private void btnSearchID_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != null)
            {
                try
                {
                    int ID = Int32.Parse(txtSearch.Text);
                    IEnumerable<Product> result = productRepository.GetProductByID(ID);
                    if (!result.Any())
                    {
                        MessageBox.Show("There is no matched record.", "Search Product By ID");
                    }
                    else
                    {
                        lvProducts.ItemsSource = result;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search Product By ID");
                }
            }
            else
            {
                LoadProductList();
            }
        }

        private void btnSearchUnitPrice_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != null)
            {
                try
                {
                    decimal unitPrice = Decimal.Parse(txtSearch.Text);
                    IEnumerable<Product> result = productRepository.GetProductByUnitPrice(unitPrice);
                    if (!result.Any())
                    {
                        MessageBox.Show("There is no matched record.", "Search Product By Unit Price");
                    }
                    else
                    {
                        lvProducts.ItemsSource = result;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search Product By Unit Price");
                }
            }
            else
            {
                LoadProductList();
            }
        }

        private void btnSearchUnitInStock_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != null)
            {
                try
                {
                    int unitsInStock = Int32.Parse(txtSearch.Text);
                    IEnumerable<Product> result = productRepository.GetProductByUnitInStock(unitsInStock);
                    if (!result.Any())
                    {
                        MessageBox.Show("There is no matched record.", "Search Product By unitsInStock");
                    }
                    else
                    {
                        lvProducts.ItemsSource = result;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search Product By unitsInStock");
                }
            }
            else
            {
                LoadProductList();
            }

        }
    }
}
