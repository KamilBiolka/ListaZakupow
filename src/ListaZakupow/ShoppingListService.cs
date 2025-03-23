using ListaZakupow.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaZakupow
{
    public class ShoppingListService
    {
        private readonly ShoppingListContext _context;

        public ShoppingListService()
        {
            _context = new ShoppingListContext();
        }

        
        public void AddShoppingList(int userId, string listName)
        {
            var user = _context.Uzytkownicy.FirstOrDefault(u => u.IdUzytkownika == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var shoppingList = new ListyZakupow
            {
                IdUzytkownika = userId,
                NazwaListy = listName
            };

            _context.ListyZakupow.Add(shoppingList);
            _context.SaveChanges();
        }

        
        public List<ListyZakupow> GetShoppingLists(int userId)
        {
            return _context.ListyZakupow.Where(l => l.IdUzytkownika == userId).ToList();
        }
    }
}