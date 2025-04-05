# ---------- Build Stage ----------
    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

    WORKDIR /app
    
    # Copy solution file
    COPY microapp.sln ./
    
    # Copy csproj files for restore
    COPY src/StudentService/StudentService.csproj ./src/StudentService/
    COPY src/SearchService/SearchService.csproj ./src/SearchService/
    COPY src/IdentityService/IdentityService.csproj ./src/IdentityService/
    COPY src/GatewayService/GatewayService.csproj ./src/GatewayService/
    COPY src/Contract/Contract.csproj ./src/Contract/
    
    # Restore dependencies
    RUN dotnet restore microapp.sln
    
    # Copy all source files
    COPY src/ ./src/
    
    # Publish the GatewayService project
    WORKDIR /app/src/GatewayService
    RUN dotnet publish -c Release -o /app/out
    
    # ---------- Runtime Stage ----------
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
    
    WORKDIR /app
    
    # Copy from build stage
    COPY --from=build /app/out .
    
    # Expose HTTP port
    EXPOSE 80
    
    ENTRYPOINT ["dotnet", "GatewayService.dll"]
    