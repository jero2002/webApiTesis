using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class Provincia
{
    public int IdProvincia { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Equipo> Equipos { get; } = new List<Equipo>();

    public virtual ICollection<Jugadore> Jugadores { get; } = new List<Jugadore>();
}
