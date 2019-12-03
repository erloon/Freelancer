FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["FR.Identity.API/FR.Identity.API.csproj", "FR.Identity.API/"]
RUN dotnet restore "FR.Identity.API/FR.Identity.API.csproj"
COPY . .
WORKDIR "/src/FR.Identity.API"
RUN dotnet build "FR.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FR.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FR.Identity.API.dll"]