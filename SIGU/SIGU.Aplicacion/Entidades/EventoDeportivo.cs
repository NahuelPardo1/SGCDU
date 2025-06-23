using SIGU.Aplicacion.Excepciones;
namespace SIGU.Aplicacion.Entidades;
public class EventoDeportivo
{
    public Guid Id { get; private set;} = Guid.NewGuid();
    public string? Nombre { get; private set; } = "";
    public string? Descripcion {get; private set;} = "";
    public DateTime FechaHoraInicio { get; private set; } = DateTime.Now;
    public double DuracionHoras{get; private set;} = 0;
    public int CupoMaximo {get; private set;} = 0;
    public Guid ResponsbleID { get; private set; }
    public Usuario Responsable { get; private set; } = null!;

    public List<Reserva> Reservas { get; set; } = new List<Reserva>();
    protected EventoDeportivo() { }

    public EventoDeportivo(string? nombre, string? descripcion, DateTime fechaInicio, double duracion, int cupo, Guid responsable)
    {
        if (string.IsNullOrWhiteSpace(nombre)) throw new ValidacionException("El nombre no puede ser nulo ni estar vacio");
        if (string.IsNullOrWhiteSpace(descripcion)) throw new ValidacionException("La descripcion no puede ser nula ni estar vacia");
        if (duracion <= 0) throw new ValidacionException("La duracion no puede ser menor o igual a 0");
        if (cupo <= 0) throw new ValidacionException("El cupo no puede ser menor o igual a 0");
        if (fechaInicio < DateTime.Now) throw new ValidacionException("La fecha de inicio no puede ser menor a la fecha actual");
        Nombre = nombre;
        Descripcion = descripcion;
        FechaHoraInicio = fechaInicio;
        DuracionHoras = duracion;
        CupoMaximo = cupo;
        ResponsbleID = responsable;
    }

    public void ActualizarDatos(string? nombre, string? descripcion, DateTime fechaInicio, double duracion, int cupo, Guid responsable)
    {
        if (string.IsNullOrWhiteSpace(nombre)) throw new ValidacionException("El nombre no puede ser nulo ni estar vacio");
        if (string.IsNullOrWhiteSpace(descripcion)) throw new ValidacionException("La descripcion no puede ser nula ni estar vacia");
        if (duracion <= 0) throw new ValidacionException("La duracion no puede ser menor o igual a 0");
        if (cupo <= 0) throw new ValidacionException("El cupo no puede ser menor o igual a 0");
        if (fechaInicio < DateTime.Now) throw new ValidacionException("La fecha de inicio no puede ser menor a la fecha actual");
        Nombre = nombre;
        Descripcion = descripcion;
        FechaHoraInicio = fechaInicio;
        DuracionHoras = duracion;
        CupoMaximo = cupo;
        ResponsbleID = responsable;
    }
}