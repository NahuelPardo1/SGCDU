using SIGU.Aplicacion.Entidades;
using SIGU.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace SIGU.Repositorios;


public class RepositorioUsuario(SIGUContext db) : IRepositorioUsuario
{
    public async Task<List<Usuario>?> ListarAsync()
    {
        var usuarios = await db.Usuario.ToListAsync();
        if(usuarios == null || usuarios.Count == 0)
        {
            return new List<Usuario>();
        }
        return usuarios;
    }

    public async Task<Usuario?> ObtenerPorIDAsync(Guid id)
    {
        // Como la base de datos es SQLite, no se puede usar Guid directamente en la consulta.
        // Por lo tanto, se convierte a string para buscarlo.
        var usuario = await db.Usuario.FindAsync(id);
        if (usuario == null)
        {
            return null!;
        }
        return usuario;
    }

    public async Task AgregarAsync(Usuario usuario)
    {
        await db.Usuario.AddAsync(usuario);
        await db.SaveChangesAsync();
    }

    public async Task ModificarAsync(Usuario usuario, Guid id)
    {
        var usuarioExistente = await db.Usuario.FindAsync(id);
        if (usuarioExistente == null) return;
        usuarioExistente.ActualizarDatos(usuario.Nombre, usuario.Apellido, usuario.DNI, usuario.Email, usuario.Telefono, usuario.Contrasenia, usuario.Permisos);
        db.Usuario.Update(usuarioExistente);
        await db.SaveChangesAsync();
    }

    public async Task<Usuario> obtenerPorDniAsync(int dni) {
        var usuario = await db.Usuario.FirstOrDefaultAsync(u => u.DNI == dni);
        if (usuario == null) { 
            return null!; 
        }
        return usuario;
    }

    public async Task<Usuario> obtenerPorEmailAsync(string email) {
        var usuario = await db.Usuario.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario == null) { 
            return null!; 
        }
        return usuario;
    }

    public async Task EliminarAsync(Guid id)
    {
        var usuario = await db.Usuario.FindAsync(id);
        if (usuario == null) return;
        db.Usuario.Remove(usuario);
        await db.SaveChangesAsync();
    }
}