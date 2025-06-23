
using SIGU.Aplicacion.Enums;   
using SIGU.Aplicacion.Interfaces;
using SIGU.Aplicacion.Excepciones;
namespace SIGU.Aplicacion.Servicios;

public class ServicioAutorizacion : IServicioAutorizacion
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    public ServicioAutorizacion(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario= repositorioUsuario;
    }
    public async Task<bool> EstaAutorizado(Guid idUsuario, Permiso permiso)
    {
        if(idUsuario== Guid.Empty)
        {
            Console.WriteLine("id invalido");
            throw new EntidadNotFoundException("El ID de usuario no puede ser un GUID vacío.");
        }
        if(!Enum.IsDefined(typeof(Permiso), permiso))
        {
            throw new ArgumentException("El permiso especificado no es valido.");
        }
        var usuario = await _repositorioUsuario.ObtenerPorIDAsync(idUsuario);
        if (usuario == null)
        {
            throw new EntidadNotFoundException($"No se encontró un usuario con el ID.");
        }
        if (!usuario.Permisos.Contains(permiso))
        {
            throw new ValidacionException($"El usuario no tiene el permiso {permiso}.");
        }
        return true;
    }
}