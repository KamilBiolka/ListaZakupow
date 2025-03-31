using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ListaZakupow;

namespace ListaZakupow
{
    
    public partial class MainWindow : Window
    {
        private readonly UserService _userService;
        private readonly ShoppingListService _shoppingListService;
        private readonly ProductService _productService;
        private int _selectedUserId;
        private int _selectedShoppingListId;


        public MainWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            _shoppingListService = new ShoppingListService();
            _productService = new ProductService();
            RefreshUserList();
            LoadProducts();
            
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Wypełnij wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _userService.AddUser(username, email);
                MessageBox.Show("Użytkownik dodany!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshUserList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void BtnRefreshList_Click(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
        }

        
        private void RefreshUserList()
        {
            lstUsers.Items.Clear();
            var users = _userService.GetUsers();

            foreach (var u in users)
            {
                lstUsers.Items.Add($"{u.IdUzytkownika}: {u.NazwaUzytkownika} ({u.Email})");
            }
        }


        private void LstUsers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedItem == null) return;

            var selectedItem = lstUsers.SelectedItem.ToString();
            string[] parts = selectedItem.Split(':');
            if (parts.Length > 1 && int.TryParse(parts[0], out int userId))
            {
                _selectedUserId = userId;
                RefreshShoppingLists(); 
            }
        }


        private void BtnAddShoppingList_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Wybierz pierwsze użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string listName = txtShoppingListName.Text;
            if (string.IsNullOrWhiteSpace(listName))
            {
                MessageBox.Show("Podaj nazwę listy zakupów.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _shoppingListService.AddShoppingList(_selectedUserId, listName);
                MessageBox.Show("Lista zakupów dodana!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshShoppingLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void RefreshShoppingLists()
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Wybierz użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            lstShoppingLists.Items.Clear();
            var shoppingLists = _shoppingListService.GetShoppingLists(_selectedUserId);

            foreach (var list in shoppingLists)
            {
                lstShoppingLists.Items.Add($"{list.IdListy}: {list.NazwaListy}");
            }
        }

        

        private void BtnAddProducts_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Wybierz użytkownika i listę.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (lstShoppingLists.SelectedItem != null)
            {
                var selectedShoppingList = lstShoppingLists.SelectedItem.ToString();
                string[] parts = selectedShoppingList.Split(':');
                if (parts.Length > 1 && int.TryParse(parts[0], out int shoppingListId))
                {
                    var addProductWindow = new AddProductWindow(shoppingListId);
                    addProductWindow.Show();
                    

                }
            }
            
        }

        private void LoadProducts()
        {
            try
            {
                
                var products = _productService.GetProducts();

                if (products != null && products.Count > 0)
                {
                    lvProducts.ItemsSource = products; 
                }
                else
                {
                    MessageBox.Show("Brak produktów w bazie danych.", "Brak produktów", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

}