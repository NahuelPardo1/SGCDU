using SIGU.Aplicacion.DTOs;
using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Enums;
using SIGU.Aplicacion.Excepciones;
using SIGU.Aplicacion.Interfaces;

namespace SIGU.Aplicacion.CasoDeUso;

public class ListarAsistenciaAEventoUseCase
{
    private readonly IRepositorioEventoDeportivo _repositorioEventoDeportivo;
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IRepositorioReserva _repositorioReserva;
    public ListarAsistenciaAEventoUseCase(IRepositorioEventoDeportivo repositorioEventoDeportivo, IRepositorioUsuario repositorioUsuario, IRepositorioReserva repositorioReserva)
    {
        _repositorioEventoDeportivo = repositorioEventoDeportivo;
        _repositorioUsuario = repositorioUsuario;
        _repositorioReserva = repositorioReserva;
    }
    public async Task<List<UsuarioDTO>> Ejecutar(Guid id)
    {
        // 1. Verificar existencia del evento

        if ( await _repositorioEventoDeportivo.ObtenerPorIDAsync(id) == null)
        {
            throw new EntidadNotFoundException("El evento no existe");
        }

        List<Usuario> asistentes = new List<Usuario>();
        List<Reserva> reservas = await _repositorioReserva.ObtenerPorEventoAsync(id);
        foreach (Reserva reserva in reservas)
        {
            if (reserva.EstadoAsistencia == Estado.Presente)
            {
                Usuario? usuario = await _repositorioUsuario.ObtenerPorIDAsync(reserva.usuarioID);
                if (usuario != null)
                {
                    asistentes.Add(usuario);
                }
            }
        }
        var usuariosDTO = asistentes.Select(u => new UsuarioDTO
        {
            ID = u.Id,
            Nombre = u.Nombre,
            Apellido = u.Apellido,
            DNI = u.DNI,
            Email = u.Email,
            Telefono = u.Telefono,
            Contrasenia = u.Contrasenia
        }).ToList();
        return usuariosDTO;
    }
}