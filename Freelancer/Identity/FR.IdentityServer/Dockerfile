FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Identity/FR.IdentityServer/FR.IdentityServer.csproj", "Identity/FR.IdentityServer/"]
RUN dotnet restore "Identity/FR.IdentityServer/FR.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Identity/FR.IdentityServer"
RUN dotnet build "FR.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FR.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FR.IdentityServer.dll"]