using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Interfaces;

namespace SIGU.Aplicacion.Validadores;

public class ValidadorEventoDeportivo
{
    private readonly IRepositorioUsuario _repositorioUsuario;

    public ValidadorEventoDeportivo(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }
    public async Task<(bool esValido, string msgError)> ValidarParaAgregarAsync(EventoDeportivo evento)
    {
        var (valido, errorComun) = ValidarEnMemoria(evento);
        if (!valido)
            return (false, errorComun);

        var responsable = await _repositorioUsuario.ObtenerPorIDAsync(evento.ResponsbleID);
        if (responsable == null)
            return (false, "El responsable del evento deportivo no existe.");

        return (true, "");
    }
    private (bool esValido, string msgError) ValidarEnMemoria(EventoDeportivo evento)
    {
        if (evento == null)
        {
            string msgError = "El evento deportivo no puede ser nulo.";
            return (false,msgError);
        }
        if (string.IsNullOrWhiteSpace(evento.Nombre))
        {
            string msgError = "El nombre del evento deportivo no puede ser nulo ni estar vacío.";
            return (false, msgError);
        }
        if (string.IsNullOrWhiteSpace(evento.Descripcion))
        {
            string msgError = "La descripción del evento deportivo no puede ser nula ni estar vacía.";
            return (false, msgError);
        }
        if (evento.DuracionHoras <= 0)
        {
            string msgError = "La duración del evento deportivo no puede ser menor o igual a 0 horas.";
            return (false, msgError);
        }
        if (evento.CupoMaximo <= 0)
        {
            string msgError = "El cupo máximo del evento deportivo no puede ser menor o igual a 0.";
            return (false, msgError);
        }
        if (evento.FechaHoraInicio < DateTime.Now)
        {
            string msgError = "La fecha y hora de inicio del evento deportivo no puede ser menor a la fecha y hora actual.";
            return (false, msgError);
        }
        if (evento.ResponsbleID == Guid.Empty)
        {
            string msgError = "El ID del responsable del evento deportivo no puede ser nulo.";
            return (false, msgError);
        }
        return (true, "");
    }
}