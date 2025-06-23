using System.ComponentModel.DataAnnotations;

namespace SIGU.Aplicacion.DTOs;

public class UsuarioLoginDTO
{

    [Required(ErrorMessage = "El Email es obligatorio.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La Contraseña es obligatoria.")]
    public string Contraseña { get; set; } = string.Empty;
}