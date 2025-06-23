using SIGU.Aplicacion.Enums;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;
namespace SIGU.Aplicacion.CasoDeUso;
public class EventoDeportivoBajaUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _servicioAutorizacion;

    public EventoDeportivoBajaUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IServicioAutorizacion servicioAutorizacion, IRepositorioReserva repositorioReserva)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _servicioAutorizacion = servicioAutorizacion;
        _repositorioReserva = repositorioReserva;
    }
    public async Task EjecutarAsync(Guid idEvento, Guid idUsuario)
    {
        var estaAutorizado = await _servicioAutorizacion.EstaAutorizado(idUsuario, Permiso.EventoBaja);
        if (!estaAutorizado)
        {
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar bajas de eventos deportivos.");
        }
        var evento = await _repositorioEventoDeportivo.ObtenerPorIDAsync(idEvento);
        if (evento == null)
        {
            throw new ValidacionException("El evento deportivo no existe.");
        }
        if (evento.FechaHoraInicio < DateTime.Now)
        {
            throw new ValidacionException("No se puede eliminar un evento deportivo que ya ha comenzado.");
        }
        var reservas = await _repositorioReserva.ObtenerPorEventoAsync(idEvento);
        if (reservas.Count != 0)
        {
            throw new ValidacionException("No se puede eliminar un evento deportivo que tiene reservas asociadas.");
        }
        await _repositorioEventoDeportivo.EliminarAsync(idEvento);
    }

}
