using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Enums;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;

namespace SIGU.Aplicacion.CasoDeUso;
public class UsuarioBajaUseCase
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IServicioAutorizacion _servicioAutorizacion;
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IRepositorioReserva _repositorioReserva;

    public UsuarioBajaUseCase(IRepositorioUsuario repositorioUsuario, IServicioAutorizacion servicioAutorizacion, IRepositorioEventoDeportivo repositorioEventoDeportivo, IRepositorioReserva repositorioReserva)
    {
        _repositorioUsuario = repositorioUsuario;
        _servicioAutorizacion = servicioAutorizacion;
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _repositorioReserva = repositorioReserva;
    }
    public async Task EjecutarAsync(Guid IDBaja, Guid IdUsuario)
    {
        // 1. Verificar permiso
        bool tienePermiso = await _servicioAutorizacion.EstaAutorizado(IdUsuario, Permiso.UsuarioBaja);
        if (tienePermiso== false)
        {
            throw new FalloAutorizacionException("El usuario no posee el permiso para relizar esta acci�n");
        }
        // 2. Verificar existencia de la persona
        Usuario? usuario =  await _repositorioUsuario.ObtenerPorIDAsync(IDBaja);
        if (usuario == null)
        {
            throw new EntidadNotFoundException("El usuario a eliminar no existe");
        }
        // 3. Verificar reservas asociadas
        List<Reserva> reservas = await _repositorioReserva.ObtenerPorPersonaAsync(IDBaja);
        if (reservas != null && reservas.Count > 0)
        {
            throw new OperacionInvalidaException("El usuario a eliminar tiene reservas");
        }
        // 4. Verificar si es responsable de alg�n evento deportivo
        List<EventoDeportivo> eventos  =  await _repositorioEventoDeportivo.ObtenerPorPersonaAsync(IDBaja);
        if (eventos != null && eventos.Count > 0)
        {
            throw new OperacionInvalidaException("El usuario a eliminar contiene eventos deportivos");
        }
        // 5. Eliminar persona
        await _repositorioUsuario.EliminarAsync(IDBaja);
    }
}