FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApplication3/IdentityService/IdentityService.csproj", "IdentityService/"]
COPY ["WebApplication3/ProfilesService/ProfilesService.csproj", "ProfilesService/"]
COPY ["WebApplication3/JobsService/JobsService.csproj", "JobsService/"]
COPY ["WebApplication3/ApplicationsService/ApplicationsService.csproj", "ApplicationsService/"]
RUN dotnet restore "IdentityService/IdentityService.csproj"
COPY . .
WORKDIR "/src/WebApplication3/IdentityService"
RUN dotnet publish "IdentityService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.dll"]
