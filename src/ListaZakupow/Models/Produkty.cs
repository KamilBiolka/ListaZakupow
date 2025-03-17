using System;
using System.Collections.Generic;

namespace ListaZakupow.Models;

public partial class Produkty
{
    public int IdProduktu { get; set; }

    public string NazwaProduktu { get; set; } = null!;

    public string? Kategoria { get; set; }

    public decimal? Cena { get; set; }

    public virtual ICollection<PozycjeListyZakupow> PozycjeListyZakupows { get; set; } = new List<PozycjeListyZakupow>();
}
