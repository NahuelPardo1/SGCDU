using SIGU.Aplicacion.DTOs;
using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Enums;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;
using SIGU.Aplicacion.Validadores;
namespace SIGU.Aplicacion.CasoDeUso;
public class EventoDeportivoAltaUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IServicioAutorizacion _servicioAutorizacion;
    private readonly ValidadorEventoDeportivo _validadorEventoDeportivo;

    public EventoDeportivoAltaUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo,IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion, ValidadorEventoDeportivo validadorEventoDeportivo)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _repositorioUsuario = repositorioUsuario;
        _servicioAutorizacion = servicioAutorizacion;
        _validadorEventoDeportivo = validadorEventoDeportivo;
    }
    public async Task EjecutarAsync(EventoDeportivoDTO dto, Guid idUsuario)
    {
        var estaAutorizado = await _servicioAutorizacion.EstaAutorizado(idUsuario, Permiso.EventoAlta);
        if (!estaAutorizado)
        {
            throw new FalloAutorizacionException("El usuario no tiene permiso para crear eventos deportivos.");
        }
        var responsable = await _repositorioUsuario.ObtenerPorIDAsync(dto.ResponsableId);

        if (responsable == null)
        {
            throw new ValidacionException("El responsable del evento deportivo no existe.");
        }
        EventoDeportivo? evento = new EventoDeportivo(dto.Nombre,dto.Descripcion,dto.FechaHoraInicio,dto.DuracionHoras,dto.CupoMaximo,dto.ResponsableId);
        var (esValido, msgError) = await _validadorEventoDeportivo.ValidarParaAgregarAsync(evento); 
        if(!esValido)
        {
            throw new ValidacionException(msgError);
        }
        await _repositorioEventoDeportivo.AgregarAsync(evento);
    }

}
