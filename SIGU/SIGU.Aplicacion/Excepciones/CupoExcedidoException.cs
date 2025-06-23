namespace SIGU.Aplicacion.Excepciones;
public class CupoExcedidoException : Exception
{
    public CupoExcedidoException(string mensaje) : base(mensaje) { }
    public CupoExcedidoException(string mensaje, Exception innerException) : base(mensaje, innerException) { }
}