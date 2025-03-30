using ListaZakupow.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaZakupow
{
    public class UserService
    {
        private readonly ShoppingListContext _context;

        public UserService()
        {
            _context = new ShoppingListContext();  
        }

        
        public void AddUser(string nazwa, string email)
        {
            var user = new User
            {
                NazwaUzytkownika = nazwa,
                Email = email
            };

            _context.Users.Add(user);
            _context.SaveChanges();  
        }

        
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.IdUzytkownika == id);
        }
    }
}