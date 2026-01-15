using AP1GestionInventario.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AP1GestionInventario.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Entrada> Entradas { get; set; }
    public DbSet<EntradaDetalle> EntradaDetalles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Entrada>()
            .HasMany(e => e.EntradaDetalles)
            .WithOne(d => d.Entrada)
            .HasForeignKey(d => d.EntradaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Costo)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Entrada>()
            .Property(e => e.Total)
            .HasPrecision(18, 2);

        modelBuilder.Entity<EntradaDetalle>()
            .Property(d => d.Costo)
            .HasPrecision(18, 2);
    }
}