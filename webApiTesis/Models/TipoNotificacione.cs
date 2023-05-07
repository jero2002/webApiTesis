using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class TipoNotificacione
{
    public int IdTipo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Notificacione> Notificaciones { get; } = new List<Notificacione>();

    public virtual ICollection<NotificacionesEj> NotificacionesEjs { get; } = new List<NotificacionesEj>();
}
