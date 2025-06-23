namespace SIGU.Aplicacion.Excepciones;

public class ValidacionException : Exception
{

    public ValidacionException(string mensaje) : base(mensaje) { }
    public ValidacionException(string mensaje, Exception innerException) : base(mensaje, innerException) { }
}