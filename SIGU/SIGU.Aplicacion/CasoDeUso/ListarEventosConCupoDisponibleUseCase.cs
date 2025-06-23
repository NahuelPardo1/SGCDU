namespace SIGU.Aplicacion.CasoDeUso;

using SIGU.Aplicacion.DTOs;
using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Interfaces;

public class ListarEventosConCupoDisponibleUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IRepositorioReserva _repositorioReserva;

    public ListarEventosConCupoDisponibleUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IRepositorioReserva repositorioReserva)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _repositorioReserva = repositorioReserva;
    }
    public async Task< List<EventoDeportivoDTO>> Ejecutar()
    {
        List<EventoDeportivo> eventosConCupo = new List<EventoDeportivo>();
        List<EventoDeportivo>? eventos = await _repositorioEventoDeportivo.ListarAsync();
        if (eventos != null)
        {
            foreach (EventoDeportivo evento in eventos)
            {
                List<Reserva> reservas = await _repositorioReserva.ObtenerPorEventoAsync(evento.Id);
                if (evento.FechaHoraInicio > DateTime.Now && reservas.Count < evento.CupoMaximo)
                {
                    eventosConCupo.Add(evento);
                }
            }
        }
        var listaEventosDTO = eventosConCupo.Select(e => new EventoDeportivoDTO
            {
                Id = e.Id,
                Nombre = e.Nombre ?? "",
                Descripcion = e.Descripcion ?? "",
                FechaHoraInicio = e.FechaHoraInicio,
                DuracionHoras = e.DuracionHoras,
                CupoMaximo = e.CupoMaximo,
                ResponsableId = e.ResponsbleID
            })
          .ToList();
        return listaEventosDTO;
    }

}