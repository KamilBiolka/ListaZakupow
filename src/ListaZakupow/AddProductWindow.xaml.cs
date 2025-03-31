using ListaZakupow.Models;
using System;
using System.Linq;
using System.Windows;

namespace ListaZakupow
{
    public partial class AddProductWindow : Window
    {
        private readonly ProductService _productService;
        private readonly ShoppingListService _shoppingListService;
        private int _selectedShoppingListId;

        public AddProductWindow(int shoppingListId)
        {
            InitializeComponent();
            _productService = new ProductService();
            _shoppingListService = new ShoppingListService();
            _selectedShoppingListId = shoppingListId;
            
        }


        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var productName = txtProductName.Text;
            var productCategory = txtProductCategory.Text;
            var productPriceText = txtProductPrice.Text;

            if (string.IsNullOrWhiteSpace(productName) || string.IsNullOrWhiteSpace(productPriceText))
            {
                MessageBox.Show("Wypełnij wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(productPriceText, out decimal price))
            {
                MessageBox.Show("Wprowadź poprawną cenę.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var product = new Product
            {
                NazwaProduktu = productName,
                Kategoria = productCategory,
                Cena = price
            };

            try
            {
                _productService.AddProduct(productName, productCategory, price, _selectedShoppingListId);
                MessageBox.Show("Produkt został dodany!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); 

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}