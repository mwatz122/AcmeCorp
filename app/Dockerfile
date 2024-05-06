FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY . /src
WORKDIR /src
RUN dotnet build "AcmeCorp.sln" -c Release
RUN dotnet test

FROM build AS publish
RUN dotnet publish "AcmeCorp.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcmeCorp.dll"]
ENV ASPNETCORE_HTTP_PORTS=80