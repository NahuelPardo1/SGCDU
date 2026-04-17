# SGCDU - Sistema de Gestión de Club Deportivo Universitario 🏟️📋

Este repositorio contiene una aplicación completa para gestionar **usuarios, eventos deportivos y reservas** dentro de un club universitario. El proyecto está construido sobre **.NET 8**, separando responsabilidades en capas para mantener una base de código más ordenada, mantenible y preparada para crecer.

---

## 🧭 Análisis general de la aplicación

La solución está organizada en tres proyectos principales:

- **SIGU.Aplicacion**: núcleo de negocio (entidades, casos de uso, validaciones, contratos e interfaces).
- **SIGU.Repositorios**: persistencia con Entity Framework Core + SQLite.
- **SIGU.UI**: interfaz de usuario en Blazor Server, rutas, páginas y composición de dependencias.

Este enfoque sigue una arquitectura por capas donde la UI depende de Aplicación y Repositorios, y la lógica de negocio se encapsula en casos de uso específicos.

---

## 🛠️ Tecnologías y herramientas usadas

- **Plataforma**: .NET 8 (C#)
- **UI**: ASP.NET Core Blazor Server (Razor Components, Interactive Server)
- **Persistencia**: Entity Framework Core 8
- **Base de datos**: SQLite (archivo local `SIGU.sqlite`)
- **Patrón de acceso a datos**: Repositorios por agregado (Usuario, EventoDeportivo, Reserva)
- **Seguridad funcional**:
  - Hash de contraseñas con **SHA256**
  - Autorización basada en permisos (`Permiso`) a nivel de casos de uso

> Nota: actualmente el proyecto no incluye JWT, Swagger/OpenAPI, ni middleware global de excepciones como una API REST separada.

---

## 🧱 Estructura del proyecto

```text
SIGU/
├── SIGU.Aplicacion/      # Dominio + lógica de negocio
│   ├── Entidades/
│   ├── DTOs/
│   ├── CasoDeUso/
│   ├── Validadores/
│   ├── Interfaces/
│   ├── Servicios/
│   ├── Excepciones/
│   └── Enums/
├── SIGU.Repositorios/    # Infraestructura y acceso a datos
│   ├── SIGUContext.cs
│   ├── RepositorioUsuario.cs
│   ├── RepositorioEventoDeportivo.cs
│   ├── RepositorioReserva.cs
│   └── DatabaseSqlite.cs
└── SIGU.UI/              # Aplicación Blazor Server
    ├── Components/
    │   ├── Pages/
    │   └── Layout/
    └── Program.cs
```

---

## 🧠 Lógica de negocio principal

### 1) Gestión de usuarios

- Alta, baja, listado y modificación de usuarios mediante casos de uso dedicados.
- Registro público de usuario (`RegisterUseCase`) y login (`LoginUseCase`).
- El **primer usuario registrado** recibe todos los permisos del sistema automáticamente.
- Validaciones de unicidad por **DNI** y **Email**.

### 2) Gestión de eventos deportivos

- Alta, modificación, baja y listado de eventos.
- Validaciones de negocio:
  - nombre y descripción obligatorios,
  - duración > 0,
  - cupo > 0,
  - fecha de inicio futura,
  - responsable existente.

### 3) Gestión de reservas

- Alta, baja, modificación y listados de reservas.
- Reglas de negocio:
  - no se puede reservar con usuario/evento inexistente,
  - no se puede reservar un evento ya iniciado,
  - no se permite duplicar reserva para mismo usuario + evento.
- Listado especializado de eventos con cupo disponible comparando reservas actuales contra cupo máximo.

### 4) Control de permisos (RBAC simple por enum)

El sistema usa el enum `Permiso` para restringir acciones (alta/modificación/baja de usuarios, eventos y reservas). La autorización se aplica dentro de casos de uso a través de `ServicioAutorizacion`.

---

## 🖥️ Flujo de interfaz (Blazor)

La UI incluye páginas para:

- Login (`/`)
- Registro (`/Registrarse`)
- Home (`/Home`)
- Gestión de usuarios (`/ListadoUsuarios`, `/AgregarUsuarios`, `/usuario/{Id}`)
- Gestión de eventos (`/ListadoEventosDeportivos`, `/AgregarEventos`, `/evento/{Id}`)
- Gestión de reservas (`/ListadoReservas`, `/reserva/{Id}`, `/MisIncripciones`)
- Reporte operativo de cupos (`/ListadoEventosConCupo`)
- Asistentes por evento (`/ListadoAsistentes/{Id}`)

El menú lateral muestra opciones administrativas en función de si el usuario actual es considerado administrador por el servicio de sesión.

---

## ⚙️ Configuración técnica relevante

- El `DbContext` se registra en la UI y apunta a SQLite con:
  - `Data Source=../SIGU.Repositorios/SIGU.sqlite`
- En el arranque se ejecuta inicialización de base de datos con `EnsureCreated()` y activación de FK en SQLite.
- Las relaciones están configuradas con `DeleteBehavior.Restrict` para evitar borrados en cascada no deseados.

---

## 🚀 Cómo levantar la aplicación en local

### Requisitos previos

- SDK de .NET 8 instalado (`dotnet --version`)

### Pasos

1. Ir a la raíz del repositorio.
2. Restaurar dependencias:

```bash
dotnet restore SIGU/SIGU.sln
```

3. Ejecutar la aplicación Blazor (proyecto de inicio):

```bash
dotnet run --project SIGU/SIGU.UI
```

4. Abrir el navegador en la URL de desarrollo:

- `http://localhost:5233`

---

## 🧪 Testing y calidad

Actualmente la solución no incluye un proyecto de pruebas automatizadas (`*.Tests`) dentro del repositorio.

Se recomienda incorporar:

- pruebas unitarias de casos de uso y validadores,
- pruebas de integración de repositorios con SQLite en memoria,
- pruebas de componentes Blazor críticos.

---

## 📌 Recomendaciones de mejora

1. **Seguridad de contraseñas**: migrar de SHA256 a BCrypt/Argon2 con salt por usuario.
2. **Autenticación robusta**: agregar JWT o ASP.NET Core Identity según necesidad.
3. **Observabilidad**: logging estructurado y manejo centralizado de excepciones.
4. **Pruebas**: añadir suite de tests y pipeline CI.
5. **API separada (opcional)**: exponer capa Application vía Web API si se planea integración externa.

---

## ✅ Estado actual

El proyecto funciona como una solución de gestión interna en Blazor Server con una base sólida en arquitectura por capas, reglas de negocio explícitas y persistencia en SQLite.
