
# Instrucciones de Instalación y Despliegue - Invoix

---

## Requisitos previos

- Node.js (v14 o superior) instalado
- Angular CLI instalado globalmente:  
  ```bash
  npm install -g @angular/cli
  ```
- Firebase CLI instalado globalmente:  
  ```bash
  npm install -g firebase-tools
  ```
- Cuenta en Firebase
- Cuenta en Render.com

---

## Backend (.NET 8) - Build y Despliegue con Docker en Render.com

1. El backend usa .NET 8 y este Dockerfile:

```dockerfile
# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia y restaura dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia todo y publica
COPY . . 
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "InvoixAPI.dll"]
```

2. Para desplegar en Render.com:
   - Crea un nuevo servicio tipo "Web Service" en Render.com.
   - Elige "Docker" como método de despliegue.
   - Sube el repositorio con el Dockerfile.
   - Render se encargará de construir y ejecutar el contenedor.

3. El backend quedará accesible en la URL que Render.com te asigne.

---

## Frontend (Angular) - Build y Despliegue en Firebase Hosting

1. Instalar dependencias:
   ```bash
   npm install
   ```

2. Generar build para producción:
   ```bash
   ng build --configuration production
   ```

3. Archivo `firebase.json` para hosting:

```json
{
  "hosting": {
    "public": "dist/frontend/browser",
    "ignore": [
      "firebase.json",
      "**/.*",
      "**/node_modules/**"
    ],
    "rewrites": [
      {
        "source": "**",
        "destination": "/index.html"
      }
    ]
  }
}
```

4. Desplegar en Firebase:

```bash
firebase init hosting
firebase deploy --only hosting
```

5. Acceder a la URL que Firebase asigne.

---

## Notas adicionales

- Asegúrate que el frontend apunte al backend desplegado en Render para evitar problemas.
- Para evitar errores con MIME o scripts, verifica la configuración de firebase.json y la carpeta pública.
