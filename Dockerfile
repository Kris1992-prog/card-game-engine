# --- STAGE 1: Build & Publish ---
# Utilizziamo l'SDK .NET 9.0 ufficiale per compilare il codice sorgente
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamo il file di progetto e ripristiniamo le dipendenze
COPY ["CardGameEngine/CardGameEngine.csproj", "CardGameEngine/"]
RUN dotnet restore "CardGameEngine/CardGameEngine.csproj"

# Copiamo tutto il resto del codice sorgente
COPY . .
WORKDIR "/src/CardGameEngine"

# Compiliamo e pubblichiamo l'applicazione in modalità Release
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# --- STAGE 2: Runtime Environment ---
# Utilizziamo il runtime di .NET 9.0 leggero e sicuro
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app

# Copiamo i binari compilati dallo stage precedente
COPY --from=build /app/publish .

# Eseguiamo l'applicazione a riga di comando quando il container si avvia
ENTRYPOINT ["dotnet", "CardGameEngine.dll"]