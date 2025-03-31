using ListaZakupow.Models;
using Microsoft.EntityFrameworkCore;
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
            var user = _context.Users.FirstOrDefault(u => u.IdUzytkownika == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var shoppingList = new ShoppingList
            {
                IdUzytkownika = userId,
                NazwaListy = listName
            };

            _context.ShoppingLists.Add(shoppingList);
            _context.SaveChanges();
        }

        
        public List<ShoppingList> GetShoppingLists(int userId)
        {
            return _context.ShoppingLists.Where(l => l.IdUzytkownika == userId).ToList();
        }

        public List<Product> GetProductsByShoppingListId(int shoppingListId)
        {
           
            var shoppingListItems = _context.ShoppingListItems
                .Where(item => item.IdListy == shoppingListId)
                .Include(item => item.IdProduktuNavigation) 
                .ToList();

            
            return shoppingListItems.Select(item => item.IdProduktuNavigation).ToList();
        }
    }
}