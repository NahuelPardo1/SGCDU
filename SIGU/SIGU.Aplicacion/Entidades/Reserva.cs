using SIGU.Aplicacion.Enums;
namespace SIGU.Aplicacion.Entidades;
public class Reserva
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid usuarioID { get; set; }
    public Guid EventoDeportivoId { get; set; } 
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    public Estado EstadoAsistencia { get; set; } = Estado.Pendiente;
    public EventoDeportivo EventoDeportivo { get; set; } = null!;
    public Usuario Usuario { get; set; } = null!;
    protected Reserva() { }

    public Reserva(Guid usuarioId, Guid eventoDeportivoId)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("El ID del usuario no puede ser nulo.");
        if (eventoDeportivoId == Guid.Empty) throw new ArgumentException("El ID del evento deportivo no puede ser nulo.");
        this.usuarioID = usuarioId;
        this.EventoDeportivoId = eventoDeportivoId;
        this.FechaAlta = DateTime.Now;
        this.EstadoAsistencia = Estado.Pendiente;
        this.EventoDeportivo = null!; // Se asignará posteriormente
        this.Usuario = null!; // Se asignará posteriormente
        this.Id = Guid.NewGuid();
    }
    public Reserva(Guid usuarioId, Guid eventoDeportivoId,Estado es)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("El ID del usuario no puede ser nulo.");
        if (eventoDeportivoId == Guid.Empty) throw new ArgumentException("El ID del evento deportivo no puede ser nulo.");
        this.usuarioID = usuarioId;
        this.EventoDeportivoId = eventoDeportivoId;
        this.FechaAlta = DateTime.Now;
        this.EstadoAsistencia = es;
        this.EventoDeportivo = null!; // Se asignará posteriormente
        this.Usuario = null!; // Se asignará posteriormente
        this.Id = Guid.NewGuid();
    }
    public void ActualizarDatos(Guid usuarioID, Guid EventoDeportivoId, DateTime FechaAlta, Estado EstadoAsistencia)
    {
        this.usuarioID = usuarioID;
        this.EventoDeportivoId = EventoDeportivoId;
        this.FechaAlta = FechaAlta;
        this.EstadoAsistencia = EstadoAsistencia;
    }
}