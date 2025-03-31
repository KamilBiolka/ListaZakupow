using ListaZakupow.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaZakupow
{
    public class ProductService
    {
        private readonly ShoppingListContext _context;

        public ProductService()
        {
            _context = new ShoppingListContext();
        }

        public void AddProduct(string productName, string productCategory, decimal price, int shoppingListId)
        {
            
            var shoppingList = _context.ShoppingLists.FirstOrDefault(sl => sl.IdListy == shoppingListId);
            if (shoppingList == null)
            {
                throw new Exception("Lista zakupów nie istnieje.");
            }

            var product = new Product
            {
                NazwaProduktu = productName,
                Kategoria = productCategory,
                Cena = price
            };

            _context.Products.Add(product);
            _context.SaveChanges(); 

            var shoppingListItem = new ShoppingListItem
            {
                IdListy = shoppingListId,
                IdProduktu = product.IdProduktu
            };

            _context.ShoppingListItems.Add(shoppingListItem); 
            _context.SaveChanges(); 
        }

        public List<Product> GetProducts()
        {
            
            return _context.Products.ToList();
        }

        internal void AddProduct(string productName, decimal price, int selectedShoppingListId)
        {
            throw new NotImplementedException();
        }
    }
}
