#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RedArborAPI/RedArborAPI.csproj", "RedArborAPI/"]
COPY ["RedArborAPI.App/RedArborAPI.Business.csproj", "RedArborAPI.App/"]
COPY ["RedArborAPI.Domain/RedArborAPI.Domain.csproj", "RedArborAPI.Domain/"]
COPY ["RedArborAPI.Infrastructure/RedArborAPI.Infrastructure.csproj", "RedArborAPI.Infrastructure/"]
RUN dotnet restore "RedArborAPI/RedArborAPI.csproj"
COPY . .
WORKDIR "/src/RedArborAPI"
RUN dotnet build "RedArborAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedArborAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedArborAPI.dll"]
