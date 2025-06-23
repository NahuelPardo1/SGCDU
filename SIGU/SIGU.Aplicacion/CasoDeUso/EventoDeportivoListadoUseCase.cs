using SIGU.Aplicacion.DTOs;
using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Enums;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;
namespace SIGU.Aplicacion.CasoDeUso;
public class EventoDeportivoListadoUseCase
{
    private readonly IServicioAutorizacion _servicioAutorizacion;
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;

    public EventoDeportivoListadoUseCase(IServicioAutorizacion servicioAutorizacion,IRepositorioEventoDeportivo repositorioEventoDeportivo)
    {
        _servicioAutorizacion = servicioAutorizacion;
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
    }
    public async Task<List<EventoDeportivoDTO>> EjecutarAsync()
    {
        List<EventoDeportivo> ListaEventos = await _repositorioEventoDeportivo.ListarAsync() ?? new List<EventoDeportivo>();
        if (ListaEventos.Count == 0)
        {
            throw new ValidacionException("No se encontraron eventos deportivos.");
        }
        List<EventoDeportivo> listaEventosFiltrada = new List<EventoDeportivo>();
        var listaEventosDTO = ListaEventos.Select(e => new EventoDeportivoDTO
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
