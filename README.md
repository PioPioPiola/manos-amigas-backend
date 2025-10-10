# ManosAmigas - .NET Hexagonal Template

Proyecto plantilla con arquitectura hexagonal (Domain, Application, Infrastructure, Web) en .NET 8.

Requisitos:
- .NET 8 SDK
- dotnet-ef tool (opcional para migraciones): dotnet tool install --global dotnet-ef

Pasos (Windows PowerShell):

```powershell
# Restaurar paquetes
dotnet restore

# Crear/Aplicar migraciones (desde la raíz del repo)
dotnet ef migrations add Initial -p src/Infrastructure -s src/Web --framework net8.0
dotnet ef database update -p src/Infrastructure -s src/Web --framework net8.0

# Correr la API
dotnet run --project src/Web
```

La cadena de conexión por defecto está en `src/Web/appsettings.json` usando la URL de Railway que proporcionaste. Cambia el secreto JWT en `Jwt:Key` antes de usar en producción.
