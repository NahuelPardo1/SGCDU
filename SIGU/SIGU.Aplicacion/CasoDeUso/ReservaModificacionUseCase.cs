using SIGU.Aplicacion.Interfaces;
using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.DTOs;
using SIGU.Aplicacion.Validadores;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Enums;

namespace SIGU.Aplicacion.CasoDeUso;
public class ReservaModificacionUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _servicioAutorizacion;
    private readonly ValidadorReserva validadorReserva;
    public ReservaModificacionUseCase(IRepositorioReserva repositorioReserva, IServicioAutorizacion servicioAutorizacion,ValidadorReserva validadorReserva)
    {
        _repositorioReserva = repositorioReserva;
        _servicioAutorizacion = servicioAutorizacion;
        this.validadorReserva = validadorReserva;
    }
    public async Task ModificarReservaAsync(Guid idEliminar,ReservaDTO dtoModificado,Guid idUsuario)
    {
        bool estaAutorizado = await _servicioAutorizacion.EstaAutorizado(idUsuario, Permiso.ReservaModificacion);  
        if (!estaAutorizado)
        {
            throw new FalloAutorizacionException("No tiene permiso para modificar reservas.");
        }
        Reserva? reservaModicada =new Reserva(dtoModificado.PersonaId,dtoModificado.EventoDeportivoId,dtoModificado.EstadoAsistencia);
        var (esValido, msgError) = await validadorReserva.ValidarParaModificarAsync(reservaModicada);
        if (!esValido)
        {
            throw new ValidacionException(msgError);
        }
        await _repositorioReserva.ModificarAsync(reservaModicada,idEliminar);
    }
}