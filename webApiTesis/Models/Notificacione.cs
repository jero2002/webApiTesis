using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class Notificacione
{
    public int IdNotificacion { get; set; }

    public int IdTipo { get; set; }

    public string? Descripcion { get; set; }

    public virtual TipoNotificacione IdTipoNavigation { get; set; } = null!;

    public virtual ICollection<UsuariosNotificacione> UsuariosNotificaciones { get; } = new List<UsuariosNotificacione>();
}
