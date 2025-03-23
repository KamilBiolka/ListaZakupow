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
            var produkt = new Produkty
            {
                NazwaProduktu = nazwa,
                Kategoria = kategoria,
                Cena = cena
            };

            _context.Produkty.Add(produkt);
            _context.SaveChanges();
        }

        public List<Produkty> GetProducts()
        {
            return _context.Produkty.ToList();
        }
    }
}
