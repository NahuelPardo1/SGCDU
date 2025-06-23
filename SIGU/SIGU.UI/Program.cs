using Microsoft.EntityFrameworkCore;
using SIGU.Aplicacion.CasoDeUso;
using SIGU.Aplicacion.Interfaces;
using SIGU.Aplicacion.Servicios;
using SIGU.Aplicacion.Validadores;
using SIGU.Repositorios;
using SIGU.Repositorios.reserva;
using SIGU.UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//  Inyectar SIGUContext con EF Core y SQLite
builder.Services.AddDbContext<SIGUContext>(options =>
    options.UseSqlite("Data Source=../SIGU.Repositorios/SIGU.sqlite"));

// Inyeccion de dependencias
builder.Services.AddScoped<IServicioAutorizacion,ServicioAutorizacion>();
builder.Services.AddScoped<IRepositorioUsuario,RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioEventoDeportivo,RepositorioEventoDeportivo>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddScoped<IHasheador,Hasheador>();
builder.Services.AddScoped<ValidadorEventoDeportivo>();
builder.Services.AddScoped<ValidadorReserva>();
builder.Services.AddScoped<ValidadorUsuario>();
builder.Services.AddSingleton<UsuarioServicioLogin>();
//Usuarios
builder.Services.AddScoped<ModificarPerfilPropioUseCase>();
builder.Services.AddScoped<UsuarioAltaUseCase>();
builder.Services.AddScoped<UsuarioBajaUseCase>();
builder.Services.AddScoped<UsuarioListadoUseCase>();
builder.Services.AddScoped<UsuarioModificacionUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RegisterUseCase>();
builder.Services.AddScoped<ListarAsistenciaAEventoUseCase>();
// Eventos Deportivos
builder.Services.AddScoped<EventoDeportivoAltaUseCase>();
builder.Services.AddScoped<EventoDeportivoBajaUseCase>();
builder.Services.AddScoped<EventoDeportivoListadoUseCase>();
builder.Services.AddScoped<EventoDeportivoModificacionUseCase>();
builder.Services.AddScoped<ListarEventosConCupoDisponibleUseCase>();
// Reservas
builder.Services.AddScoped<ReservaAltaUseCase>();
builder.Services.AddScoped<ReservaBajaUseCase>();
builder.Services.AddScoped<ReservaListadoUseCase>();
builder.Services.AddScoped<ReservaModificacionUseCase>();
builder.Services.AddAuthorizationCore();
var app = builder.Build();


// Inicializar la base de datos SQLite
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SIGUContext>();
    DatabaseSqlite.Inicializar(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
