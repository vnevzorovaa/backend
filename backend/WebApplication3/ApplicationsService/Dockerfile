FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApplication3/ApplicationsService/ApplicationsService.csproj", "ApplicationsService/"]
COPY ["WebApplication3/IdentityService/IdentityService.csproj", "IdentityService/"]
COPY ["WebApplication3/ProfilesService/ProfilesService.csproj", "ProfilesService/"]
COPY ["WebApplication3/JobsService/JobsService.csproj", "JobsService/"]
RUN dotnet restore "ApplicationsService/ApplicationsService.csproj"
COPY . .
WORKDIR "/src/WebApplication3/ApplicationsService"
RUN dotnet publish "ApplicationsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApplicationsService.dll"]
