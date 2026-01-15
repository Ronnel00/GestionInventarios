using System.Linq.Expressions;
using AP1GestionInventario.Data;
using AP1GestionInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace AP1GestionInventario.Services;

public class ProductoService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public ProductoService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> Guardar(Producto producto)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        if (producto.ProductoId == 0)
        {
            context.Productos.Add(producto);
        }
        else
        {
            context.Productos.Update(producto);
        }

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var producto = await context.Productos.FindAsync(id);

        if (producto == null)
            return false;

        context.Productos.Remove(producto);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Producto?> Buscar(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Productos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductoId == id);
    }

    public async Task<List<Producto>> Listar(Expression<Func<Producto, bool>> criterio)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Productos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Existe(int id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Productos.AnyAsync(p => p.ProductoId == id);
    }
}