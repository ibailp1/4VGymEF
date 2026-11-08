# 4VGymEF

## Descripción

Este proyecto consiste en unir los dos proyectos originales de 4VGym en una única API:

- **4VGymAPI:** API REST original, cuya persistencia de datos se realizaba mediante listas y diccionarios en memoria.
- **4VGymEF:** proyecto que contiene los modelos y la conexión a la base de datos mediante Entity Framework y SQL Server.

El objetivo es mantener las mismas operaciones y endpoints que tenía 4VGymAPI, pero utilizando Entity Framework para consultar los datos almacenados en SQL Server.

## Tecnologías utilizadas

- C#
- ASP.NET Core
- .NET 10
- Entity Framework Core
- SQL Server
- Swagger

## Estructura del proyecto

El proyecto está organizado en diferentes carpetas para separar las responsabilidades:

- **Controllers:** controladores que gestionan las peticiones HTTP de la API.
- **Models:** modelos utilizados en el proyecto.
- **DTOs:** objetos utilizados para intercambiar datos con la API.
- **Repositories:** clases encargadas de conectar los controladores con Entity Framework y la base de datos.
- **Data:** configuración del contexto de Entity Framework.
- **Migrations:** migraciones de la base de datos.
- **Validations:** validaciones de los datos recibidos.
- **Program.cs:** configuración e inicio de la aplicación.

## Base de datos

La persistencia de datos se realiza mediante Entity Framework Core y SQL Server.

En el repositorio se incluye el archivo `script.sql`, que contiene los datos de prueba necesarios para trabajar con la base de datos y comprobar el funcionamiento de la API.

Para utilizarlo, basta con ejecutarlo desde SQL Server Management Studio sobre la base de datos correspondiente.

## Funcionamiento

La API mantiene la misma definición y las mismas operaciones que el proyecto original 4VGymAPI.

Los repositorios se encargan de realizar la conexión entre la API y la base de datos, utilizando Entity Framework para recuperar la información.

Los datos recibidos mediante las peticiones también pasan por las correspondientes validaciones.

Los métodos POST validan los datos recibidos, aunque no realizan necesariamente la inserción en la base de datos, tal como se indica en los requisitos de la práctica.

## Instalación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/ibailp1/4VGymEF.git
```

### 2. Configurar SQL Server

Configurar la cadena de conexión a SQL Server en el archivo `appsettings.json`, utilizando los datos de tu instalación.

### 3. Preparar los datos de prueba

Ejecutar el archivo `datos.sql` desde SQL Server Management Studio para cargar los datos de prueba.

### 4. Ejecutar el proyecto

Abrir la solución en Visual Studio o ejecutar desde la terminal:

```bash
dotnet restore
dotnet run
```

### 5. Acceder a Swagger

Una vez iniciada la aplicación, acceder a la dirección de Swagger que aparece en la consola para consultar y probar los endpoints disponibles.
