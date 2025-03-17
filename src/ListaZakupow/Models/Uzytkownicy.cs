using System;
using System.Collections.Generic;

namespace ListaZakupow.Models;

public partial class Uzytkownicy
{
    public int IdUzytkownika { get; set; }

    public string NazwaUzytkownika { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ListyZakupow> ListyZakupows { get; set; } = new List<ListyZakupow>();
}
