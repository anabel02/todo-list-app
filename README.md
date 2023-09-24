# ToDos

La solución cuenta con una API implementada usando ASP.NET Core (.Net6.0) conectada a una base de datos MySQL utilizando EntityFramework Core, 
y una aplicación web implementada con ReactJS y TypeScript.

## Ejecutar la aplicación
Ejecute los siguientes comandos en el directorio de la solución para instalar las dependencias <br />
Del frontend: 
```
yarn --cwd to-dos-app install
```
Del backend: 
```
dotnet restore
```

Para configurar la conexión con la base de datos debe ir al archivo ``./ToDosApi/appsettings.json`` y en el campo ``DefaultConnection`` de ``ConnectionStrings`` colocar su conexión. <br />

Para actualizar la base de datos al estado actual de las migraciones debe ejecutar el siguiente comando:
```
dotnet ef database update --project ToDosApi
```
Luego ejecutar el siguiente comando para levantar el backend:
```dotnet run --project ToDosApi```
Especificar en el ``.env`` de ``./to-dos-app`` el puerto en que corre el backend
y por último este para levantar el fronted:
```yarn --cwd to-dos-app start```