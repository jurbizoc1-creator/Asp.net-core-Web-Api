# WebApiSolution

Entity Framework Core con SQL Server y .NET 9.

Esta solución utiliza controladores CRUD que reciben directamente los modelos. No utiliza DTO, carpeta `Contracts` ni clases `Request`. Los modelos son POCO sencillos y la configuración de tablas, claves y relaciones está centralizada con Fluent API en el `DbContext`. Los modelos también incluyen atributos de validación.

## Endpoints

- `/api/estudiantes`
- `/api/cursos`
- `/api/docentes`
- `/api/asignaciones`

Cada controlador ofrece `GET`, `GET/{id}`, `POST`, `PUT/{id}` y `DELETE/{id}`.

## Base de datos

- Motor: Microsoft SQL Server LocalDB
- Servidor: `(localdb)\MSSQLLocalDB`
- Base: `University`
- Autenticación: Windows
- Cadena de conexión: `WebApiSolution/appsettings.json`

## Relaciones

- Un estudiante puede tener muchas asignaciones.
- Un curso puede tener muchas asignaciones.
- Un docente puede tener muchas asignaciones.
- Cada asignación pertenece a un estudiante, un curso y un docente.

## Abrir y restaurar la solución

Se requiere el SDK de .NET 9 y SQL Server Express LocalDB.

Desde la carpeta que contiene `WebApiSolution.sln`:

```powershell
cd WebApiSolution
dotnet restore
dotnet build
```

También se puede abrir `WebApiSolution.sln` directamente en Visual Studio.

Los comandos siguientes se ejecutan desde la carpeta que contiene `WebApiSolution.csproj`.

## Instalar los paquetes NuGet desde cero

El proyecto ya incluye sus referencias NuGet; normalmente basta con `dotnet restore`. Para agregar los paquetes de Entity Framework Core a un proyecto nuevo:

```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.21
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.21
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.21
```

## Instalar o actualizar dotnet-ef

Instalar:

```powershell
dotnet tool install --global dotnet-ef --version 9.0.21
```

Actualizar si ya está instalado:

```powershell
dotnet tool update --global dotnet-ef --version 9.0.21
```

Comprobar la versión:

```powershell
dotnet ef --version
```

## Crear una migración

Después de modificar un modelo o una relación:

```powershell
dotnet ef migrations add NombreDeLaMigracion
```

Por ejemplo, después de agregar un campo de correo al modelo Estudiante:

```powershell
dotnet ef migrations add AgregarCorreoEstudiante
```

## Aplicar las migraciones a SQL Server

```powershell
dotnet ef database update
```

Este comando crea `University` si no existe y aplica las migraciones pendientes.

## Consultar las migraciones

```powershell
dotnet ef migrations list
```

## Eliminar la última migración no aplicada

```powershell
dotnet ef migrations remove
```

No se debe eliminar una migración aplicada sin regresar primero la base a una migración anterior.

## Regresar a una migración anterior

```powershell
dotnet ef database update NombreMigracionAnterior
```

Para revertir todas las migraciones:

```powershell
dotnet ef database update 0
```

Revertir migraciones puede eliminar tablas y sus datos. Estos comandos no son necesarios para ejecutar el proyecto normalmente.

## Ejecutar el proyecto

```powershell
dotnet run --launch-profile http
```

Después se puede abrir `http://localhost:5200`.

Para consultar estudiantes: `http://localhost:5200/api/estudiantes`.

El archivo `WebApiSolution.http` contiene ejemplos para probar los endpoints.

## Revisar y actualizar paquetes NuGet

```powershell
dotnet list package --outdated
```

Para actualizar un paquete, se ejecuta `dotnet add package` con la versión elegida. Los paquetes de Entity Framework Core deben mantener versiones compatibles.

Para establecer las versiones utilizadas por este proyecto:

```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.21
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.21
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.21
dotnet restore
dotnet build
```

## Flujo después de cambiar un modelo

```powershell
dotnet build
dotnet ef migrations add DescripcionDelCambio
dotnet ef database update
dotnet ef migrations list
```

Los archivos generados dentro de `Migrations` no deben editarse manualmente.
