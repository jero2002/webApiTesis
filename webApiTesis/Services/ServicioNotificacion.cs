using Microsoft.EntityFrameworkCore;
using System.Net;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;
using webApiTesis.Services.IServices;

namespace webApiTesis.Services
{
    public class ServicioNotificacion :IServiceNotificacion
    {
        private readonly DreamTeamContext context;
        private readonly ISecurityService _securityService;

        public ServicioNotificacion(DreamTeamContext _context, ISecurityService securityService)
        {
            this.context = _context;
            _securityService = securityService;
        }


        //jugadorxequipo
        public async Task<ResultadoBase> postNotificacionJugadorxEquipo(int idjugador, int idequipo)
        {
            var exists = await context.Notificaciones.AnyAsync(n => n.IdJugador == idjugador && n.IdEquipo == idequipo);
            ResultadoBase resultado = new ResultadoBase();
            if (exists)
            {
               
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "Ya existe esta solicitud";
                    return resultado;
                };
            }
            else
            {
                var notification = new Notificacione
                {
                    IdJugador = idjugador,
                    IdEquipo = idequipo,
                    IdTipo = 1
                };

                await context.Notificaciones.AddAsync(notification);
                await context.SaveChangesAsync();

                {
                    resultado.Ok = true;
                    resultado.CodigoEstado = 200;
                    resultado.Message = "Solicitud realizada";
                    return resultado;
                };
            }
        }



        //Equipoxjugador
        public async Task<ResultadoBase> postNotificacionEquipoxJugador(int idequipo, int idjugador)
        {
            var exists = await context.NotificacionesEjs.AnyAsync(n => n.IdEquipo == idequipo && n.IdJugador == idjugador);
            ResultadoBase resultado = new ResultadoBase();
            if (exists)
            {

                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "Ya existe esta solicitud";
                    return resultado;
                };
            }
            else
            {
                var notificationej = new NotificacionesEj
                {
                    IdEquipo = idequipo,
                    IdJugador = idjugador,               
                    IdTipo = 1
                };

                await context.NotificacionesEjs.AddAsync(notificationej);
                await context.SaveChangesAsync();

                {
                    resultado.Ok = true;
                    resultado.CodigoEstado = 200;
                    resultado.Message = "Solicitud realizada";
                    return resultado;
                };
            }
        }

        public async Task<List<DTONotificacionParaEquipo>> NotificacionParaEquipo(int idequipo)
        {
            ResultadoBase resultado = new ResultadoBase();

            var notificacionesequipo = await context.Notificaciones
                .Where(x => x.IdEquipo == idequipo) 
                .Select(x => new DTONotificacionParaEquipo
                {
                  IdNotificacion = x.IdNotificacion,
                  IdJugador = x.IdJugador,
                  Nombre = x.IdJugadorNavigation.Nombre
                })
                .AsNoTracking()
                .ToListAsync();

            return notificacionesequipo;
        }

        public async Task<List<DTONotificacionParaJugador>> NotificacionParaJugador(int idjugador)
        {
            ResultadoBase resultado = new ResultadoBase();

            var notificacionesjugador = await context.NotificacionesEjs
                .Where(x => x.IdJugador == idjugador)
                .Select(x => new DTONotificacionParaJugador
                {
                    IdNotificacionEJ = x.IdNotificacionEj,
                    IdEquipo = x.IdEquipo,
                    Nombre = x.IdEquipoNavigation.Nombre
                })
                .AsNoTracking()
                .ToListAsync();

            return notificacionesjugador;
        }

        public async Task<ResultadoBase> Deletenotificacion(int id)
        {
            ResultadoBase resultado = new ResultadoBase();
            var not = await context.Notificaciones.Where(c => c.IdNotificacion.Equals(id)).FirstOrDefaultAsync();
            if (not != null)
            {
                context.Notificaciones.Remove(not); 
                await context.SaveChangesAsync(); 
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Eliminaste exitosamente";
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al eliminar";
                return resultado;
            }

            return resultado;
        }


        public async Task<ResultadoBase> DeletenotificacionEJ(int id)
        {
            ResultadoBase resultado = new ResultadoBase();
            var not = await context.NotificacionesEjs.Where(c => c.IdNotificacionEj.Equals(id)).FirstOrDefaultAsync();
            if (not != null)
            {
                context.NotificacionesEjs.Remove(not);
                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Eliminaste exitosamente";
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al eliminar";
                return resultado;
            }

            return resultado;
        }





    }
}
