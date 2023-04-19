using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class Jugadore
{
    public int IdJugador { get; set; }

    public string Nombre { get; set; } = null!;

    public long Celular { get; set; }

    public int Edad { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdPosicion { get; set; }

    public int IdEstadoJ { get; set; }

    public int IdProvincia { get; set; }

    public int IdGenero { get; set; }

    public virtual ICollection<EquiposJugadore> EquiposJugadores { get; } = new List<EquiposJugadore>();

    public virtual EstadoJugador IdEstadoJNavigation { get; set; } = null!;

    public virtual Genero IdGeneroNavigation { get; set; } = null!;

    public virtual Posicione IdPosicionNavigation { get; set; } = null!;

    public virtual Provincia IdProvinciaNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
