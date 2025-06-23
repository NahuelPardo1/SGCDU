using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;
using SIGU.Aplicacion.DTOs;
namespace SIGU.Aplicacion.CasoDeUso;
public class ModificarPerfilPropioUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IHasheador _hasheador;
    public ModificarPerfilPropioUseCase(IRepositorioUsuario repositorioUsuario, IHasheador hasheador)
    {
        _repositorioUsuario = repositorioUsuario;
        _hasheador = hasheador;
    }
    public async Task EjecutarAsync(UsuarioDTO usuarioNuevo, Guid idUsuarioAmodificar)
    {
        // este caso de uso se encarga de registrar un nuevo usuario en el sistema sin permisos especiales
        // Hashear la contrase�a
        if (string.IsNullOrEmpty(usuarioNuevo.Contrasenia))
        {
            throw new ValidacionException("La contrase�a no puede ser nula o vac�a.");
        }
        usuarioNuevo.Contrasenia = _hasheador.Hashear(usuarioNuevo.Contrasenia);
        // Validacion adicional para evitar argumentos nulos
        if (string.IsNullOrEmpty(usuarioNuevo.Nombre))
        {
            throw new ArgumentException("El nombre no puede ser nulo o vac�o.");
        }
        if (string.IsNullOrEmpty(usuarioNuevo.Apellido))
        {
            throw new ArgumentException("El apellido no puede ser nulo o vac�o.");
        }
        if (string.IsNullOrEmpty(usuarioNuevo.Email))
        {
            throw new ArgumentException("El email no puede ser nulo o vac�o.");
        }
        if (string.IsNullOrEmpty(usuarioNuevo.Telefono))
        {
            throw new ArgumentException("El tel�fono no puede ser nulo o vac�o.");
        }
        // si el dni que viene es distinto al que tiene el usuario, se debe validar que no exista otro usuario con ese dni
        Usuario? usuarioExitente = await _repositorioUsuario.ObtenerPorIDAsync(idUsuarioAmodificar);
        if (usuarioExitente == null)
        {
            throw new ValidacionException("El usuario a modificar no existe.");
        }
        if (usuarioExitente.DNI != usuarioNuevo.DNI)
        {
            Usuario? usuarioPorDni = await _repositorioUsuario.obtenerPorDniAsync(usuarioNuevo.DNI);
            if (usuarioPorDni != null)
            {
                throw new ValidacionException("Ya existe un usuario con ese DNI.");
            }
        }
        // si el email que viene es distinto al que tiene el usuario, se debe validar que no exista otro usuario con ese email
        if (usuarioExitente.Email != usuarioNuevo.Email)
        {
            Usuario? usuarioPorEmail = await _repositorioUsuario.obtenerPorEmailAsync(usuarioNuevo.Email);
            if (usuarioPorEmail != null)
            {
                throw new ValidacionException("Ya existe un usuario con ese email.");
            }
        }
        // Crear una instancia de Usuario a partir del DTO
        Usuario? usuario = new Usuario(usuarioNuevo.Nombre, usuarioNuevo.Apellido, usuarioNuevo.DNI, usuarioNuevo.Email, usuarioNuevo.Telefono, usuarioNuevo.Contrasenia,usuarioNuevo.permisos);
        // Guardar el nuevo usuario en el repositorio
        await _repositorioUsuario.ModificarAsync(usuario, idUsuarioAmodificar);
    }
}
