using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;

namespace SIGU.Aplicacion.CasoDeUso;
public class ReservaBajaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _servicioAutorizacion;
    public ReservaBajaUseCase(IRepositorioReserva repositorioReserva, IServicioAutorizacion servicioAutorizacion)
    {
        _repositorioReserva = repositorioReserva;
        _servicioAutorizacion = servicioAutorizacion;
    }
    public async Task EjecutarAsync(Guid idEliminar, Guid idUsuario)
    {
        bool estaAutorizado = await _servicioAutorizacion.EstaAutorizado(idUsuario, Enums.Permiso.ReservaBaja);
        if (!estaAutorizado)
        {
            throw new FalloAutorizacionException("El usuario no tiene permiso para dar bajas de reserva.");
        }   
        Reserva? reserva = await _repositorioReserva.ObtenerPorIDAsync(idEliminar);
        if (reserva == null)
        {
            throw new ValidacionException("No se encontró la reserva a eliminar.");
        }
        await _repositorioReserva.EliminarAsync(idEliminar);
    }
}