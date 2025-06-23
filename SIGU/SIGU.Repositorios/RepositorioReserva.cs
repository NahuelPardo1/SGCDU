namespace SIGU.Repositorios.reserva;
using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RepositorioReserva(SIGUContext db) : IRepositorioReserva
{
    public async Task<List<Reserva>?> ListarAsync()
    {
        var reservas = await db.Reserva.ToListAsync();
        if (reservas == null || reservas.Count == 0)
        {
            return new List<Reserva>();
        }
        return reservas;
    }
    public async Task<Reserva?> ObtenerPorIDAsync(Guid id)
    {
        return await db.Reserva.FindAsync(id);
    }
    public async Task AgregarAsync(Reserva reserva)
    {
        await db.Reserva.AddAsync(reserva);
        await db.SaveChangesAsync();
    }
    public async Task ModificarAsync(Reserva reserva, Guid id)
    {
        var reservaExistente = await db.Reserva.FindAsync(id);
        if (reservaExistente == null) return;
        reservaExistente.ActualizarDatos(reserva.usuarioID,reserva.EventoDeportivoId,reserva.FechaAlta, reserva.EstadoAsistencia);
        db.Reserva.Update(reservaExistente);
        await db.SaveChangesAsync();
    }
    public async Task EliminarAsync(Guid id)
    {
        Reserva? reserva = await db.Reserva.FindAsync(id);
        if (reserva == null) return;
        db.Reserva.Remove(reserva);
        await db.SaveChangesAsync();
    }
    public async Task<Reserva?> ObtenerPorPersonaYEventoAsync(Guid personaId, Guid eventoId)
    {
        return await db.Reserva.FirstOrDefaultAsync(r => r.usuarioID == personaId && r.EventoDeportivoId == eventoId);
    }
    public async Task<List<Reserva>> ObtenerPorEventoAsync(Guid eventoId)
    {
        return await db.Reserva.Where(r => r.EventoDeportivoId == eventoId).ToListAsync();
    }
    public async Task<List<Reserva>> ObtenerPorPersonaAsync(Guid personaId)
    {
        return await db.Reserva.Where(r => r.usuarioID == personaId).ToListAsync();
    }
    public async Task<List<Reserva>> ObtenerPorEventoYPersonaAsync(Guid eventoId, Guid personaId)
    {
        return await db.Reserva.Where(r => r.EventoDeportivoId == eventoId && r.usuarioID == personaId).ToListAsync();
    }
}