using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    public string Nombre { get; set; } = null!;

    public long Celular { get; set; }

    public int TorneoGanado { get; set; }

    public string Entrenador { get; set; } = null!;

    public int IdEstadoE { get; set; }

    public int IdGeneroE { get; set; }

    public int IdProvincia { get; set; }

    public virtual ICollection<EquiposJugadore> EquiposJugadores { get; } = new List<EquiposJugadore>();

    public virtual EstadoEquipo IdEstadoENavigation { get; set; } = null!;

    public virtual GenerosEquipo IdGeneroENavigation { get; set; } = null!;

    public virtual Provincia IdProvinciaNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
