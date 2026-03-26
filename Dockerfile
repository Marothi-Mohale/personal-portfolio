FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY NuGet.Config ./
COPY .config ./.config
COPY MarothiMohale.Portfolio.sln ./
COPY MarothiMohale.Portfolio.Web/MarothiMohale.Portfolio.Web.csproj MarothiMohale.Portfolio.Web/
RUN dotnet restore MarothiMohale.Portfolio.Web/MarothiMohale.Portfolio.Web.csproj --configfile ./NuGet.Config

COPY MarothiMohale.Portfolio.Web/. MarothiMohale.Portfolio.Web/
RUN dotnet publish MarothiMohale.Portfolio.Web/MarothiMohale.Portfolio.Web.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN mkdir /app/data

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/portfolio.db;Cache=Shared"

EXPOSE 8080

ENTRYPOINT ["dotnet", "MarothiMohale.Portfolio.Web.dll"]
