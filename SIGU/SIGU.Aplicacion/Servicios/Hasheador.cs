using System.Security.Cryptography;
using System.Text;
using SIGU.Aplicacion.Interfaces;
namespace SIGU.Aplicacion.Servicios;

public class Hasheador: IHasheador
{
    public string Hashear(string input)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        byte[] hash = sha256.ComputeHash(bytes);

        // Convertimos el hash a string hexadecimal legible
        StringBuilder sb = new StringBuilder();
        foreach (var b in hash)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }
}