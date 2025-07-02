FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CalcCDB.Appication.Api/CalcCDB.Appication.Api.csproj", "CalcCDB.Appication.Api/"]
COPY ["CalcCDB.Domain/CalcCDB.Domain.csproj", "CalcCDB.Domain/"]
RUN dotnet restore CalcCDB.Appication.Api/CalcCDB.Appication.Api.csproj
WORKDIR "/src/CalcCDB"
COPY . .
RUN dotnet build CalcCDB.Appication.Api/CalcCDB.Appication.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CalcCDB.Appication.Api/CalcCDB.Appication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalcCDB.Appication.Api.dll"]