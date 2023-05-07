using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webApiTesis.Models;

namespace webApiTesis.Data;

public partial class DreamTeamContext : DbContext
{
    public DreamTeamContext()
    {
    }

    public DreamTeamContext(DbContextOptions<DreamTeamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<EquiposJugadore> EquiposJugadores { get; set; }

    public virtual DbSet<EstadoEquipo> EstadoEquipos { get; set; }

    public virtual DbSet<EstadoJugador> EstadoJugadors { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<GenerosEquipo> GenerosEquipos { get; set; }

    public virtual DbSet<Jugadore> Jugadores { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<NotificacionesEj> NotificacionesEjs { get; set; }

    public virtual DbSet<Posicione> Posiciones { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TipoNotificacione> TipoNotificaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-I97QGOR\\SQLEXPRESS;Initial Catalog=dreamTeam; Trusted_Connection=true; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo);

            entity.ToTable("equipos");

            entity.Property(e => e.IdEquipo)
                .ValueGeneratedNever()
                .HasColumnName("idEquipo");
            entity.Property(e => e.Celular).HasColumnName("celular");
            entity.Property(e => e.Entrenador)
                .HasMaxLength(50)
                .HasColumnName("entrenador");
            entity.Property(e => e.IdEstadoE).HasColumnName("idEstadoE");
            entity.Property(e => e.IdGeneroE).HasColumnName("idGeneroE");
            entity.Property(e => e.IdProvincia).HasColumnName("idProvincia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.TorneoGanado).HasColumnName("torneoGanado");

            entity.HasOne(d => d.IdEstadoENavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdEstadoE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idEstadoE");

            entity.HasOne(d => d.IdGeneroENavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdGeneroE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idGeneroE");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idProvincia");
        });

        modelBuilder.Entity<EquiposJugadore>(entity =>
        {
            entity.HasKey(e => e.IdEquipoJugador);

            entity.ToTable("equiposJugadores");

            entity.Property(e => e.IdEquipoJugador).HasColumnName("idEquipoJugador");
            entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            entity.Property(e => e.IdJugador).HasColumnName("idJugador");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.EquiposJugadores)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idEquipo");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.EquiposJugadores)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idJugador");
        });

        modelBuilder.Entity<EstadoEquipo>(entity =>
        {
            entity.HasKey(e => e.IdEstadoE);

            entity.ToTable("estadoEquipo");

            entity.Property(e => e.IdEstadoE).HasColumnName("idEstadoE");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<EstadoJugador>(entity =>
        {
            entity.HasKey(e => e.IdEstadoJ);

            entity.ToTable("estadoJugador");

            entity.Property(e => e.IdEstadoJ).HasColumnName("idEstadoJ");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero);

            entity.ToTable("generos");

            entity.Property(e => e.IdGenero).HasColumnName("idGenero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<GenerosEquipo>(entity =>
        {
            entity.HasKey(e => e.IdGeneroE);

            entity.ToTable("generosEquipo");

            entity.Property(e => e.IdGeneroE).HasColumnName("idGeneroE");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Jugadore>(entity =>
        {
            entity.HasKey(e => e.IdJugador);

            entity.ToTable("jugadores");

            entity.Property(e => e.IdJugador)
                .ValueGeneratedNever()
                .HasColumnName("idJugador");
            entity.Property(e => e.Celular).HasColumnName("celular");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.IdEstadoJ).HasColumnName("idEstadoJ");
            entity.Property(e => e.IdGenero).HasColumnName("idGenero");
            entity.Property(e => e.IdPosicion).HasColumnName("idPosicion");
            entity.Property(e => e.IdProvincia).HasColumnName("idProvincia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdEstadoJNavigation).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.IdEstadoJ)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idEstadoJ");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.IdGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idGenero");

            entity.HasOne(d => d.IdPosicionNavigation).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.IdPosicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idPosicion");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idProvinciaJ");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion);

            entity.ToTable("notificaciones");

            entity.Property(e => e.IdNotificacion).HasColumnName("idNotificacion");
            entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            entity.Property(e => e.IdJugador).HasColumnName("idJugador");
            entity.Property(e => e.IdTipo).HasColumnName("idTipo");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idEeequipo");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idJjjugador");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idTipo");
        });

        modelBuilder.Entity<NotificacionesEj>(entity =>
        {
            entity.HasKey(e => e.IdNotificacionEj);

            entity.ToTable("notificacionesEJ");

            entity.Property(e => e.IdNotificacionEj).HasColumnName("idNotificacionEJ");
            entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            entity.Property(e => e.IdJugador).HasColumnName("idJugador");
            entity.Property(e => e.IdTipo).HasColumnName("idTipo");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.NotificacionesEjs)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idEeeequipo");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.NotificacionesEjs)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idJjjjugador");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.NotificacionesEjs)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idTtipo");
        });

        modelBuilder.Entity<Posicione>(entity =>
        {
            entity.HasKey(e => e.IdPosicion);

            entity.ToTable("posiciones");

            entity.Property(e => e.IdPosicion).HasColumnName("idPosicion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia);

            entity.ToTable("provincias");

            entity.Property(e => e.IdProvincia).HasColumnName("idProvincia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoNotificacione>(entity =>
        {
            entity.HasKey(e => e.IdTipo);

            entity.ToTable("tipoNotificaciones");

            entity.Property(e => e.IdTipo).HasColumnName("idTipo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.HashContrasena).HasColumnName("hashContrasena");
            entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            entity.Property(e => e.IdJugador).HasColumnName("idJugador");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEquipo)
                .HasConstraintName("FK_idEequipo");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdJugador)
                .HasConstraintName("FK_idJjugador");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idRol");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    public async Task<int> SaveChangesAsync(string usuario)
    {

        return await base.SaveChangesAsync();
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
