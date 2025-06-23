using SIGU.Aplicacion.Enums;
using System.ComponentModel.DataAnnotations;

namespace SIGU.Aplicacion.DTOs;

public class UsuarioDTO
{
    public Guid ID { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El Dni es obligatorio.")]
    [Range(1000000, 99999999, ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos.")]
    public int DNI { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
    public string? Apellido { get; set; }

    [Required(ErrorMessage = "El Email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
    [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Contrasenia { get; set; }
    public List<Permiso> permisos { get; set; } = new List<Permiso>();
    public void setPermiso(List<Permiso> list)
    {
        permisos = list;
    }
}
