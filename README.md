# .NET-DDD-Domain-Driven-Design-
Repositorio sobre DDD 


Creacion de proyectos: 
 dotnet new classlib -n Wpm.Management.Domain
 dotnet new webapi -n Wpm.Management.Api --use-controllers
  dotnet new xunit -n Wpm.Management.Domain.Tests
Relacion entre ellos:
  cd  .\Wpm.Management.Api\
  dotnet add reference ..\Wpm.Management.Domain\

Añadimos la solucion
  cd..
  dotnet new sln -n Wpm

Añadimos las bibliotecas a la solucion
  dotnet sln add .\Wpm.Management.Api\
