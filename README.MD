

COMANDOS
dotnet new mvc --auth Individual
dotnet new console -o miproyecto


##MIGRACION

dotnet ef migrations add InitialSetup --context appwebcine.Data.ApplicationDbContext -o "C:\Users\lukae\Documents\Trabajo\ENSEÑAR\Talleres\C#\appwebcine\Data\Migrations"

dotnet ef migrations add PagoMigracion --context appwebcine.Data.ApplicationDbContext -o "C:\Users\lukae\Documents\Trabajo\ENSEÑAR\Talleres\C#\appwebcine\Data\Migrations"

dotnet ef database update

##GENERADOR DE LOGIN Y REGISTRO
dotnet aspnet-codegenerator identity -dc appwebcine.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout;Account.ForgotPassword;Account.ResetPassword"














##DOCKER
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

#CAMBIAR AQUI EL NOMBRE DEL APLICATIVO
#nombre de tu app busca en bin\Release****\netcore5.0\plantitas.exe
ENV APP_NET_CORE appwebcine.dll 

CMD ASPNETCORE_URLS=http://*:$PORT dotnet $APP_NET_CORE