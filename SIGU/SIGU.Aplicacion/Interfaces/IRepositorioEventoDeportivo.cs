using SIGU.Aplicacion.Entidades;
namespace SIGU.Aplicacion.Interfaces;

public interface IRepositorioEventoDeportivo : IRepositorioBase<EventoDeportivo>
{
    Task<List<EventoDeportivo>> ObtenerPorPersonaAsync(Guid personaId);
}