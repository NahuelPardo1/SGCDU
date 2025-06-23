using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace SIGU.Repositorios;

public class RepositorioEventoDeportivo(SIGUContext db) : IRepositorioEventoDeportivo
{
    public async Task<List<EventoDeportivo>?> ListarAsync() 
    { 
        var eventos = await db.EventoDeportivo.ToListAsync();
        if(eventos == null || eventos.Count == 0) 
        { 
            return new List<EventoDeportivo>();
        }
        return eventos;
    }

    public async Task<EventoDeportivo?> obtenerPorIDAsync(Guid id) 
    {
        return await db.EventoDeportivo.FindAsync(id);
    }

    public async Task AgregarAsync(EventoDeportivo evento) { 
        await db.EventoDeportivo.AddAsync(evento);
        await db.SaveChangesAsync();
    }

    public async Task<List<EventoDeportivo>> ObtenerPorPersonaAsync(Guid personaID) { 
        return await db.EventoDeportivo
            .Where(e => e.Responsable.Id == personaID)
            .ToListAsync();
    }

    public async Task ModificarAsync(EventoDeportivo evento, Guid id) 
    { 
        var eventoExistente = await db.EventoDeportivo.FindAsync(id);
        if (eventoExistente != null)
        {
            eventoExistente.ActualizarDatos(evento.Nombre, evento.Descripcion, evento.FechaHoraInicio, evento.DuracionHoras, evento.CupoMaximo, evento.ResponsbleID);
            db.EventoDeportivo.Update(eventoExistente);
            await db.SaveChangesAsync();
        }
    }

    public async Task EliminarAsync(Guid id) 
    { 
        var evento = await db.EventoDeportivo.FindAsync(id);
        if (evento == null) return;
        db.EventoDeportivo.Remove(evento);
        await db.SaveChangesAsync();
    }

    public async Task<EventoDeportivo?> ObtenerPorIDAsync(Guid id)
    {
        return await db.EventoDeportivo.FindAsync(id);
    }
}