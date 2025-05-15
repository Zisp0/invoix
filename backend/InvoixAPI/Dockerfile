# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia csproj y restaura
COPY ["InvoixAPI/InvoixAPI.csproj", "InvoixAPI/"]
RUN dotnet restore "InvoixAPI/InvoixAPI.csproj"

# Copia todo y publica
COPY . .
WORKDIR "/src/InvoixAPI"
RUN dotnet publish "InvoixAPI.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "InvoixAPI.dll"]
