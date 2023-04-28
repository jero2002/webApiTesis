using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiTesis.Commands;
using webApiTesis.Comun;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;
using webApiTesis.Services.IServices;

namespace webApiTesis.Services
{
    public class ServicioEquipo : IServicioEquipo
    {
        private readonly DreamTeamContext context;
        private readonly ISecurityService _securityService;

        public ServicioEquipo(DreamTeamContext _context, ISecurityService securityService)
        {
            this.context = _context;
            _securityService = securityService;
        }

        public async Task<ResultadoBase> PostEquipo(Equipo e)
        {
            
            ResultadoBase resultado = new ResultadoBase();
            try
            {
                await context.AddAsync(e);
                await context.SaveChangesAsync(_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName);

                Equipo equipoGuardado = await context.Equipos.FindAsync(e.IdEquipo);

                int userId = _securityService.GetUserId();
                Usuario usuario = await context.Usuarios.FindAsync(userId);


                usuario.IdEquipo = equipoGuardado.IdEquipo;
                var  respondeme =  await context.SaveChangesAsync(_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName);


                return new ResultadoBase
                {
                    Ok = true,
                    CodigoEstado = 200,
                    Message = "El Equipo se guardó exitosamente.",
                    respondeme = respondeme
                };
            }
            catch (Exception)
            {
                return new ResultadoBase
                {
                    Ok = false,
                    CodigoEstado = 400,
                    Message = "Error al ingresar un Equipo"
                };
            }
        }

        public async Task<List<EstadoEquipo>> GetEstadoEquipo()
        {
            return await this.context.EstadoEquipos.AsNoTracking().ToListAsync();
        }

        public async Task<List<GenerosEquipo>> GetGeneroEquipo()
        {
            return await this.context.GenerosEquipos.AsNoTracking().ToListAsync();
        }

        public async Task<List<Provincia>> GetProvincia()
        {
            return await this.context.Provincias.AsNoTracking().ToListAsync();
        }

        public async Task<ResultadoBase> PutEquipos(DTOequipos dtoEquipos)
        {
            ResultadoBase resultado = new ResultadoBase();

            try
            {
                if (dtoEquipos == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El objeto equipo está vacío";
                    return resultado;
                }

                var Equipo = await context.Equipos.FindAsync(dtoEquipos.IdEquipo);

                if (Equipo == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un equipo con el id {dtoEquipos.IdEquipo}";
                    return resultado;
                }

                Equipo.Nombre = dtoEquipos.Nombre;
                Equipo.Celular = dtoEquipos.Celular;
                Equipo.TorneoGanado = dtoEquipos.TorneoGanado;
                Equipo.Entrenador = dtoEquipos.Entrenador;
                Equipo.IdEstadoE = dtoEquipos.IdEstadoE;
                Equipo.IdGeneroE = dtoEquipos.IdGeneroE;
                Equipo.IdProvincia = dtoEquipos.IdProvincia;
          

                context.Update(Equipo);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El equipo fue modificado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar el equipo";
                resultado.Error = ex.ToString();
            }

            return resultado;
        }



        public async Task<ComandoEquipo> GetEquipoByID(int id)
        {
            var equipo = await context.Equipos

                .FirstOrDefaultAsync(x => x.IdEquipo == id);
            ComandoEquipo comando = new ComandoEquipo();

            if (equipo != null)
            {
                comando.IdEquipo = equipo.IdEquipo;
                comando.Nombre = equipo.Nombre;
                comando.Celular = equipo.Celular;
                comando.TorneoGanado = equipo.TorneoGanado;
                comando.Entrenador = equipo.Entrenador;
                comando.IdEstadoE = equipo.IdEstadoE;
                comando.IdGeneroE = equipo.IdGeneroE;
                comando.IdProvincia = equipo.IdProvincia;
               
            }
            return comando;
        }


        public async Task<List<Equipo>> GetEquipoByIDGenero(int idGenero)
        {
            IQueryable<Equipo> query = context.Equipos
                .Include(x => x.IdEstadoENavigation)
                .Include(x => x.IdGeneroENavigation)
                .Include(x => x.IdProvinciaNavigation)
                .AsNoTracking();

            if (idGenero == 3)
            {
                query = query.Where(x => x.IdGeneroENavigation.IdGeneroE <= idGenero);
            }
            else if (idGenero == 1)
            {
                query = query.Where(x => x.IdGeneroENavigation.IdGeneroE == 1 || x.IdGeneroENavigation.IdGeneroE == 3);
            }
            else if (idGenero == 2)
            {
                query = query.Where(x => x.IdGeneroENavigation.IdGeneroE == 2 || x.IdGeneroENavigation.IdGeneroE == 3);
            }


            return await query.ToListAsync();
        }


        public async Task<List<DTOEquiposJugador>> GetEquiposjugador(int idjugador) //dtoequiposjugador
        {
            ResultadoBase resultado = new ResultadoBase();

            var equipos = await context.EquiposJugadores
                .Where(x => x.IdJugador == idjugador) 
                .Select(x => new DTOEquiposJugador
                {
                    idEquipoJugador = x.IdEquipoJugador,
                    Nombre = x.IdEquipoNavigation.Nombre,
                    Celular = x.IdEquipoNavigation.Celular,
                    TorneoGanado = x.IdEquipoNavigation.TorneoGanado,
                    Entrenador = x.IdEquipoNavigation.Entrenador,
                  
                })
                .AsNoTracking()
                .ToListAsync();

            return equipos;
        }






    }
}
