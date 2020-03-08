FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
EXPOSE 5000
COPY . .
RUN dotnet publish FR.IdentityServer.csproj -c release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet FR.IdentityServer.dll

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["Identity/FR.IdentityServer/FR.IdentityServer.csproj", "Identity/FR.IdentityServer/"]
#RUN dotnet restore "Identity/FR.IdentityServer/FR.IdentityServer.csproj"
#COPY . .
#WORKDIR "/src/Identity/FR.IdentityServer"
#RUN dotnet build "FR.IdentityServer.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "FR.IdentityServer.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "FR.IdentityServer.dll"]