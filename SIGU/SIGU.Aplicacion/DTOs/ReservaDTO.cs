using SIGU.Aplicacion.Enums;
namespace SIGU.Aplicacion.DTOs;

public class ReservaDTO
{
    public Guid Id { get; set; }
    public Guid PersonaId { get; set; }
    public Guid EventoDeportivoId { get; set; }
    public Estado EstadoAsistencia { get; set; }
    public DateTime FechaAlta { get; set; }

    public void setPersonaId(Guid idpersona)
    {
        this.PersonaId = idpersona;
    }
    public void setEventoDeportivoid(Guid id)
    {
        this.EventoDeportivoId = id;
    }

}