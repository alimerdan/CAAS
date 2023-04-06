#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CAAS.csproj", "."]
RUN dotnet restore "./CAAS.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CAAS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CAAS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Make sure the app binds to port 80
ENV ASPNETCORE_URLS="http://+:80"
ENV DOTNET_RUNNING_IN_CONTAINER="true"
ENV PATH="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin"
ENV NUGET_PACKAGES="/root/.nuget/fallbackpackages2"
ENV NUGET_FALLBACK_PACKAGES="value: /root/.nuget/fallbackpackages;/root/.nuget/fallbackpackages2"

ENTRYPOINT ["dotnet", "CAAS.dll"]