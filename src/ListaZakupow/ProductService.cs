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

        public void AddProduct(string nazwa, string kategoria, decimal cena)
        {
            var produkt = new Product
            {
                NazwaProduktu = nazwa,
                Kategoria = kategoria,
                Cena = cena
            };

            _context.Products.Add(produkt);
            _context.SaveChanges();
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
    }
}
