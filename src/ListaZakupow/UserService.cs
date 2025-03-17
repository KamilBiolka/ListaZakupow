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
            var user = new Uzytkownicy
            {
                NazwaUzytkownika = nazwa,
                Email = email
            };

            _context.Uzytkownicy.Add(user);
            _context.SaveChanges();  
        }

        
        public List<Uzytkownicy> GetUsers()
        {
            return _context.Uzytkownicy.ToList();
        }

        
        public Uzytkownicy? GetUserById(int id)
        {
            return _context.Uzytkownicy.FirstOrDefault(u => u.IdUzytkownika == id);
        }
    }
}