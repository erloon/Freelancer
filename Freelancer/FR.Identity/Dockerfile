FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["FR.Identity/FR.Identity.csproj", "FR.Identity/"]
RUN dotnet restore "FR.Identity/FR.Identity.csproj"
COPY . .
WORKDIR "/src/FR.Identity"
RUN dotnet build "FR.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FR.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FR.Identity.dll"]