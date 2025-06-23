using SIGU.Aplicacion.Entidades;
namespace SIGU.Aplicacion.Interfaces;

public interface IRepositorioUsuario : IRepositorioBase<Usuario>
{
    Task<Usuario> obtenerPorDniAsync(int dni);
    Task<Usuario> obtenerPorEmailAsync(string email);
}