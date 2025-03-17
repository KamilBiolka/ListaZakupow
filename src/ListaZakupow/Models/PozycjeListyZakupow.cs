using System;
using System.Collections.Generic;

namespace ListaZakupow.Models;

public partial class PozycjeListyZakupow
{
    public int IdPozycji { get; set; }

    public int IdListy { get; set; }

    public int IdProduktu { get; set; }

    public int? Ilosc { get; set; }

    public virtual ListyZakupow IdListyNavigation { get; set; } = null!;

    public virtual Produkty IdProduktuNavigation { get; set; } = null!;
}
