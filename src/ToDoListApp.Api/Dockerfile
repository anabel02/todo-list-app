FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ToDosApi/ToDosApi.csproj", "ToDosApi/"]
RUN dotnet restore "ToDosApi/ToDosApi.csproj"
COPY . .
WORKDIR "/src/ToDosApi"
RUN dotnet build "ToDosApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDosApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDosApi.dll"]
