FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProducerApplication/ProducerApplication.csproj", "ProducerApplication/"]
RUN dotnet restore "ProducerApplication/ProducerApplication.csproj"
COPY . .
WORKDIR "/src/ProducerApplication"
RUN dotnet build "ProducerApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProducerApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProducerApplication.dll"]
