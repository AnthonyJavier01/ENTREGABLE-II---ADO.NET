# Entregable ADO.NET - Sistema de Ventas
 
## Descripción
 
Aplicación de consola desarrollada en C# utilizando ADO.NET y SQL Server para la gestión de un sistema de ventas.
 
El sistema permite realizar operaciones CRUD sobre las siguientes entidades:
 
- Categorías
- Productos
- Clientes
- Órdenes
- Detalle de Órdenes
 
## Tecnologías Utilizadas
 
- .NET 10
- C#
- SQL Server
- ADO.NET
- Microsoft.Data.SqlClient
 
## Patrones y Principios Aplicados
 
### Repository Pattern
 
Implementado mediante:
 
- CategoriasRepository
- ProductosRepository
- ClientesRepository
- OrdenesRepository
- OrdenDetalleRepository
 
### Factory Pattern
 
Implementado mediante:
 
- SqlConnectionFactory
 
### SOLID
 
- SRP (Single Responsibility Principle)
- Separación por capas:
- Modelos
- Repositorios
- Servicios
- Menús

## Base de Datos
 
Tablas implementadas:
 
- Categorias
- Productos
- Clientes
- Ordenes
- OrdenDetalle
 
## Funcionalidades
 
### Categorías
 
- Listar
- Agregar
- Actualizar
- Eliminar (Eliminacion logica)
 
### Productos
 
- Listar
- Agregar
- Actualizar
- Eliminar (Eliminacion logica)
 
### Clientes
 
- Listar
- Agregar
- Actualizar
- Eliminar (Eliminacion logica)
 
### Órdenes
 
- Listar
- Agregar
- Actualizar
- Eliminar  (Eliminacion logica)
 
### Detalle de Órdenes
 
- Listar
- Agregar
- Actualizar
- Eliminar  (Eliminacion logica)
 
## Instalación
 

 
Ejecutar proyecto:
 
```bash
navegar hasta la carpeta que contine elarchivo Program.cs y ejecutar:

dotnet clean
dotnet restore
dotnet run
```
 
## Autor
 
Anthony Bueno
``