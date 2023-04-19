using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class EstadoEquipo
{
    public int IdEstadoE { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Equipo> Equipos { get; } = new List<Equipo>();
}
