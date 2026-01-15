using System.ComponentModel.DataAnnotations;

namespace AP1GestionInventario.Models;

public class Producto
{
    [Key]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(200)]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El costo es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor a 0")]
    public decimal Costo { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    public int Existencia { get; set; }
}
