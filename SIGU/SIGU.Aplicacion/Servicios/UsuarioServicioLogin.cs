using SIGU.Aplicacion.Entidades;

namespace SIGU.Aplicacion.Servicios;

public class UsuarioServicioLogin
{
	private Usuario? _usuario;

	private bool _logueado = false;
    public UsuarioServicioLogin() {
		_usuario = null!;
    }
    public void SetUser(Usuario user) { 
		_usuario = user;
		_logueado = true;
    }
	public bool IsLogueado() {
		return _logueado;
	}
    public Usuario GetUser() {
        if (!_logueado || _usuario == null)
            throw new InvalidOperationException("No hay un usuario logueado.");
        return _usuario;
    }
	public bool isAdmin() 
	{
		if (_usuario != null)
		{
			return _usuario.Permisos.Count > 8;
		}
		else return false;
	}
    public void Logueado()
    {
        _logueado = true;
    }
    public Guid recuperarID() {
		if (_usuario == null) {
			throw new InvalidOperationException("No se pudo recuperar el ID del usuario porque no está logueado o el usuario es nulo.");
		}
		return _usuario.Id;
	}

}