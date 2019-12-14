FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["FinanceManager/FR.FinanceManager.API/FR.FinanceManager.API.csproj", "FinanceManager/FR.FinanceManager.API/"]
RUN dotnet restore "FinanceManager/FR.FinanceManager.API/FR.FinanceManager.API.csproj"
COPY . .
WORKDIR "/src/FinanceManager/FR.FinanceManager.API"
RUN dotnet build "FR.FinanceManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FR.FinanceManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FR.FinanceManager.API.dll"]