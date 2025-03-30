using System;
using System.Collections.Generic;

namespace ListaZakupow.Models;

public partial class User
{
    public int IdUzytkownika { get; set; }

    public string NazwaUzytkownika { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();
}
