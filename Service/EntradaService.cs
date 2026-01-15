using System.Linq.Expressions;
using AP1GestionInventario.Data;
using AP1GestionInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace AP1GestionInventario.Services;

public class EntradaService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public EntradaService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> Guardar(Entrada entrada)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        if (entrada.EntradaId == 0)
        {
            return await Insertar(entrada, context);
        }
        else
        {
            return await Modificar(entrada, context);
        }
    }

    private async Task<bool> Insertar(Entrada entrada, ApplicationDbContext context)
    {
        // Calcular el total
        entrada.Total = entrada.EntradaDetalles.Sum(d => d.Cantidad * d.Costo);

        // Limpiar las referencias de navegación para evitar problemas de tracking
        foreach (var detalle in entrada.EntradaDetalles)
        {
            detalle.Producto = null;
            detalle.Entrada = null;
        }

        context.Entradas.Add(entrada);

        // Sumar cantidades al inventario
        foreach (var detalle in entrada.EntradaDetalles)
        {
            var producto = await context.Productos.FindAsync(detalle.ProductoId);
            if (producto != null)
            {
                producto.Existencia += detalle.Cantidad;
            }
        }

        return await context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Entrada entrada, ApplicationDbContext context)
    {
        // Obtener la entrada original con sus detalles
        var entradaOriginal = await context.Entradas
            .Include(e => e.EntradaDetalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntradaId == entrada.EntradaId);

        if (entradaOriginal == null)
            return false;

        // 1. REVERTIR las cantidades originales (restar del inventario)
        foreach (var detalleOriginal in entradaOriginal.EntradaDetalles)
        {
            var producto = await context.Productos.FindAsync(detalleOriginal.ProductoId);
            if (producto != null)
            {
                producto.Existencia -= detalleOriginal.Cantidad;
            }
        }

        // 2. APLICAR las nuevas cantidades (sumar al inventario)
        foreach (var detalleNuevo in entrada.EntradaDetalles)
        {
            var producto = await context.Productos.FindAsync(detalleNuevo.ProductoId);
            if (producto != null)
            {
                producto.Existencia += detalleNuevo.Cantidad;
            }

            // Limpiar referencias de navegación
            detalleNuevo.Producto = null;
            detalleNuevo.Entrada = null;
        }

        // 3. Actualizar la entrada
        entrada.Total = entrada.EntradaDetalles.Sum(d => d.Cantidad * d.Costo);

        // Eliminar los detalles anteriores
        var detallesAEliminar = context.EntradaDetalles
            .Where(d => d.EntradaId == entrada.EntradaId);
        context.EntradaDetalles.RemoveRange(detallesAEliminar);

        // Actualizar la entrada master
        context.Entradas.Update(entrada);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var entrada = await context.Entradas
            .Include(e => e.EntradaDetalles)
            .FirstOrDefaultAsync(e => e.EntradaId == id);

        if (entrada == null)
            return false;

        foreach (var detalle in entrada.EntradaDetalles)
        {
            var producto = await context.Productos.FindAsync(detalle.ProductoId);
            if (producto != null)
            {
                producto.Existencia -= detalle.Cantidad;
            }
        }

        context.Entradas.Remove(entrada);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Entrada?> Buscar(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Entradas
            .Include(e => e.EntradaDetalles)
            .ThenInclude(d => d.Producto)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntradaId == id);
    }

    public async Task<List<Entrada>> Listar(Expression<Func<Entrada, bool>> criterio)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Entradas
            .Include(e => e.EntradaDetalles)
            .AsNoTracking()
            .Where(criterio)
            .OrderByDescending(e => e.Fecha)
            .ToListAsync();
    }

    public async Task<bool> Existe(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Entradas.AnyAsync(e => e.EntradaId == id);
    }
}