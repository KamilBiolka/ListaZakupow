using System;
using System.Collections.Generic;

namespace ListaZakupow.Models;

public partial class ListyZakupow
{
    public int IdListy { get; set; }

    public int IdUzytkownika { get; set; }

    public string NazwaListy { get; set; } = null!;

    public virtual Uzytkownicy IdUzytkownikaNavigation { get; set; } = null!;

    public virtual ICollection<PozycjeListyZakupow> PozycjeListyZakupows { get; set; } = new List<PozycjeListyZakupow>();
}
