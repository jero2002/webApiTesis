using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] HashContrasena { get; set; } = null!;

    public int? IdEquipo { get; set; }

    public int? IdJugador { get; set; }

    public int IdRol { get; set; }

    public virtual Equipo? IdEquipoNavigation { get; set; }

    public virtual Jugadore? IdJugadorNavigation { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<UsuariosNotificacione> UsuariosNotificaciones { get; } = new List<UsuariosNotificacione>();
}
