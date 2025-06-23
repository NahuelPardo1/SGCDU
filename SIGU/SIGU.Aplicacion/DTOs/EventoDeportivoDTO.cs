using SIGU.Aplicacion.Entidades;
using System.ComponentModel.DataAnnotations;

namespace SIGU.Aplicacion.DTOs
{
	public class EventoDeportivoDTO
	{
		[Required(ErrorMessage = "El Id es obligatorio.")]
        public Guid Id { get; set; } 
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = "";
		[Required(ErrorMessage = "La descripcion es obligatoria.")]
        public string Descripcion { get; set; } = "";
		[Required(ErrorMessage = "La fecha y hora de inicio es obligatoria.")]
        public DateTime FechaHoraInicio { get; set; }
		[Required(ErrorMessage = "La duracion en horas es obligatoria.")]
        public double DuracionHoras { get; set; }
		[Required(ErrorMessage = "El cupo maximo es obligatorio.")]
        public int CupoMaximo { get; set; }

		[Required(ErrorMessage = "El responsable es obligatorio")]
        public Guid ResponsableId{ get; set; }
	}
}