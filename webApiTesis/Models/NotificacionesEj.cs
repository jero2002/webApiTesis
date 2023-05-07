using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class NotificacionesEj
{
    public int IdNotificacionEj { get; set; }

    public int IdTipo { get; set; }

    public int IdEquipo { get; set; }

    public int IdJugador { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    public virtual Jugadore IdJugadorNavigation { get; set; } = null!;

    public virtual TipoNotificacione IdTipoNavigation { get; set; } = null!;
}
