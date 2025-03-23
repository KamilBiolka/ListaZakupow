using ListaZakupow.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaZakupow
{

    public class ShoppingListItemService
    {
        private readonly ShoppingListContext _context;

        public ShoppingListItemService()
        {
            _context = new ShoppingListContext();
        }

        public void AddProductToList(int idListy, int idProduktu, int ilosc)
        {
            var pozycja = new PozycjeListyZakupow
            {
                IdListy = idListy,
                IdProduktu = idProduktu,
                Ilosc = ilosc
            };

            _context.PozycjeListyZakupow.Add(pozycja);
            _context.SaveChanges();
        }

        public List<PozycjeListyZakupow> GetItemsByList(int idListy)
        {
            return _context.PozycjeListyZakupow.Where(p => p.IdListy == idListy).ToList();
        }
    }
}
