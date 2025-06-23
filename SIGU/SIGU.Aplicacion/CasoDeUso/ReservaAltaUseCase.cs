using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;
using SIGU.Aplicacion.Enums;
using SIGU.Aplicacion.DTOs;
using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Validadores;

namespace SIGU.Aplicacion.CasoDeUso;
public class ReservaAltaUseCase
{
    private readonly IRepositorioReserva _repositorioReserva;
    private readonly IServicioAutorizacion _servicioAutorizacion;
    private readonly ValidadorReserva _validadorReserva;
    public ReservaAltaUseCase(IRepositorioReserva repositorioReserva,IServicioAutorizacion servicioAutorizacion, ValidadorReserva validadorReserva)
    {
        _repositorioReserva = repositorioReserva;
        _servicioAutorizacion = servicioAutorizacion;
        _validadorReserva = validadorReserva;
    }
    public async Task EjecutarAsync(ReservaDTO dto, Guid idUsuario)
    {
        //1. Validar que el usuario tiene permiso para crear reservas
        bool estaAutorizado = await _servicioAutorizacion.EstaAutorizado(idUsuario, Permiso.ReservaAlta);
        if (!estaAutorizado)
        {
            throw new FalloAutorizacionException("El usuario no tiene permiso para crear reservas.");
        }
        Reserva? reserva = new Reserva(dto.PersonaId, dto.EventoDeportivoId);
        var (esValido,msgError) = await _validadorReserva.ValidarParaAgregarAsync(reserva);
        if (!esValido)
        {
            throw new ValidacionException(msgError);
        }
        await _repositorioReserva.AgregarAsync(reserva);
    }
}