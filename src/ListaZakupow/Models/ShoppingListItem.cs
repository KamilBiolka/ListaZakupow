using System;
using System.Collections.Generic;

namespace ListaZakupow.Models;

public partial class ShoppingListItem
{
    public int IdPozycji { get; set; }

    public int IdListy { get; set; }

    public int IdProduktu { get; set; }

    public int? Ilosc { get; set; }

    public virtual ShoppingList IdListyNavigation { get; set; } = null!;

    public virtual Product IdProduktuNavigation { get; set; } = null!;
}
