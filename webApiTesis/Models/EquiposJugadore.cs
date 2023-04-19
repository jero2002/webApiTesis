using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class EquiposJugadore
{
    public int IdEquipoJugador { get; set; }

    public int IdEquipo { get; set; }

    public int IdJugador { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    public virtual Jugadore IdJugadorNavigation { get; set; } = null!;
}
