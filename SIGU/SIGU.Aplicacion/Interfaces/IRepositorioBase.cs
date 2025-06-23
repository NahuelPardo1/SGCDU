namespace SIGU.Aplicacion.Interfaces;

public interface IRepositorioBase<T>
{
	Task AgregarAsync(T entidad);
	Task ModificarAsync(T entidad, Guid id);
	Task EliminarAsync(Guid id);
	Task<List<T>?> ListarAsync();
	Task<T?> ObtenerPorIDAsync(Guid id);

}