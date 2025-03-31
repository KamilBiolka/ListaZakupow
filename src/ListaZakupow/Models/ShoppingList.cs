using System;
using System.Collections.Generic;

namespace ListaZakupow.Models;

public partial class ShoppingList
{
    public int IdListy { get; set; }

    public int IdUzytkownika { get; set; } 

    public string NazwaListy { get; set; } = null!;

    public virtual User IdUzytkownikaNavigation { get; set; } = null!;

    public virtual ICollection<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();
}
