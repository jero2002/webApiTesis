using System;
using System.Collections.Generic;

namespace webApiTesis.Models;

public partial class UsuariosNotificacione
{
    public int IdUsuarioNotificacion { get; set; }

    public int IdNotificacion { get; set; }

    public int IdUsuario { get; set; }

    public int IdEmisor { get; set; }

    public int IdReceptor { get; set; }

    public virtual Notificacione IdNotificacionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
