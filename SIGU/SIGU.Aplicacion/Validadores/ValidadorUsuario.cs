using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Interfaces; 
namespace SIGU.Aplicacion.Validadores;

public class ValidadorUsuario
{
    private readonly IRepositorioUsuario _repositorioUsuario;  

    public ValidadorUsuario(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }
    public async Task<(bool esValido, string msgError)> ValidarParaAgregarAsync(Usuario usuario)
    {
        var (valido, errorComun) = ValidarEnMemoria(usuario);
        if (!valido)
        {
            return (false, errorComun);
        }
        if (usuario.DNI!= 0)
        {
            Usuario? existentePorDni = await _repositorioUsuario.obtenerPorDniAsync(usuario.DNI);
            if (existentePorDni != null)
            {
                string msgError = "El DNI ya existe.";
                return (false, msgError);
            }       
        }
        if (!string.IsNullOrWhiteSpace(usuario.Email))
        {
            Usuario? existentePorEmail = await _repositorioUsuario.obtenerPorEmailAsync(usuario.Email);
            if (existentePorEmail != null)
            {
                string msgError = "El email ya existe.";
                return (false, msgError);
            }
        }
        return (true, "");
    }
    public async Task<(bool esValido, string msgError)> ValidarParaModificarAsync(Usuario usuario)
    {
        var (valido, errorComun) = ValidarEnMemoria(usuario);
        if (!valido)
        {
            return (false, errorComun);
        }
        if (usuario.DNI <= 0)
        {
            Usuario? existentePorDni = await _repositorioUsuario.obtenerPorDniAsync(usuario.DNI);
            if (existentePorDni ==null)
            {
                string msgError = "El DNI no existe";
                return (false, msgError);
            }
        }
        return (true, "");
    }
    private (bool esValido, string msgError) ValidarEnMemoria(Usuario usuario)
    {
        if (usuario == null)
        {
            string msgError = "El usuario no puede ser nulo.";
            return (false, msgError);
        }
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
        {
            string msgError = "El nombre no puede ser nulo ni estar vacío.";
            return (false, msgError);
        }
        if (string.IsNullOrWhiteSpace(usuario.Apellido))
        {
            string msgError = "El apellido no puede ser nulo ni estar vacío.";
            return (false, msgError);
        }
        if (usuario.DNI <= 0)
        {
            string msgError = "El DNI no puede ser nulo ni estar vacío.";
            return (false, msgError);
        }
        if (string.IsNullOrWhiteSpace(usuario.Email) && !usuario.Email.Contains("@") && !usuario.Email.Contains("."))
        {
            string msgError = "El email no puede ser nulo, ni estar vacío, y debe contener '@' y '.'";
            return (false, msgError);
        }
        return (true, "");
    }
}