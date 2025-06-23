using SIGU.Aplicacion.Entidades;
namespace SIGU.Aplicacion.Interfaces;

public interface IRepositorioReserva : IRepositorioBase<Reserva>
{
	Task<Reserva?> ObtenerPorPersonaYEventoAsync(Guid personaId, Guid eventoId);
	Task<List<Reserva>> ObtenerPorEventoAsync(Guid eventoId);
	Task<List<Reserva>> ObtenerPorPersonaAsync(Guid personaId);
}