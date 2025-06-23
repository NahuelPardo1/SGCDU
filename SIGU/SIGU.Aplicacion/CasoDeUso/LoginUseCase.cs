using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;
namespace SIGU.Aplicacion.CasoDeUso;
public class LoginUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IHasheador _hasheador;
    public LoginUseCase(IRepositorioUsuario repositorioUsuario,IHasheador hasheador)
    {
        _repositorioUsuario= repositorioUsuario;
        _hasheador = hasheador;
    }
    public async Task<Usuario?>IniciarSesion(string email, string password)
    {
        Usuario? usuario = await _repositorioUsuario.obtenerPorEmailAsync(email);
        if (usuario == null)
        {
            throw new ValidacionException("Usuario no encontrado. Por favor, verifique sus credenciales.");
        }
        string contraseniaHasheada = _hasheador.Hashear(password);
        if (usuario.Contrasenia != contraseniaHasheada)
        {
            throw new ValidacionException("Contraseña incorrecta. Por favor, intente nuevamente.");
        }
        return usuario;
    }
}