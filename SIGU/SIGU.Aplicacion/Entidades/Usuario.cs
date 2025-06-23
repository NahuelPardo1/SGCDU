using SIGU.Aplicacion.Enums;
namespace SIGU.Aplicacion.Entidades;
public class Usuario
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nombre { get; private set; } = "";
    public int DNI { get; private set; }
    public string Apellido { get; private set; } = "";
    public string Email { get; private set; } = "";
    public string Telefono { get; private set; } = "";
    public string Contrasenia { get; private set; } = "";
    public List<Permiso> Permisos { get; set; } = new List<Permiso>();
    public List<EventoDeportivo> EventosResponsables { get; set; } = new List<EventoDeportivo>();
    public List<Reserva> Reservas { get; private set; } = new List<Reserva>();

    protected Usuario() { }
    public Usuario(string nombre, string apellido, int dni, string email, string telefono, string contrasenia,List<Permiso> list)
    {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.DNI = dni;
        this.Email = email;
        this.Telefono = telefono;
        this.Contrasenia = contrasenia;
        this.Permisos = list ?? new List<Permiso>();
    }
    public Usuario(string nombre, string apellido, int dni, string email, string telefono, string contrasenia)
    {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.DNI = dni;
        this.Email = email;
        this.Telefono = telefono;
        this.Contrasenia = contrasenia;
    }

    public void ActualizarDatos(string nombre, string apellido, int dni, string email, string telefono, string contrasenia, List<Permiso> permisos)
    {
        Nombre = nombre;
        Apellido = apellido;
        DNI = dni;
        Email = email;
        Telefono = telefono;
        Contrasenia = contrasenia;
        Permisos = permisos;
    }
    public List<Permiso> GetPermisos()
    {
        return this.Permisos;
    }
}