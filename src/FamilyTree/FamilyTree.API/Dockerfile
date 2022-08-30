#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FamilyTree.API/FamilyTree.API.csproj", "FamilyTree.API/"]
COPY ["FamilyTree.Persistence/FamilyTree.Persistence.csproj", "FamilyTree.Persistence/"]
COPY ["FamilyTree.Domain/FamilyTree.Domain.csproj", "FamilyTree.Domain/"]
COPY ["FamilyTree.Service/FamilyTree.Service.csproj", "FamilyTree.Service/"]
RUN dotnet restore "FamilyTree.API/FamilyTree.API.csproj"
COPY . .
WORKDIR "/src/FamilyTree.API"
RUN dotnet build "FamilyTree.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FamilyTree.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamilyTree.API.dll"]