using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class Posicione
{
    public int IdPosicion { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Jugadore> Jugadores { get; } = new List<Jugadore>();
}
