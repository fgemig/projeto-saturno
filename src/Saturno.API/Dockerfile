FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Saturno.API/Saturno.API.csproj", "src/Saturno.API/"]
RUN dotnet restore "src/Saturno.API/Saturno.API.csproj"
COPY . .
WORKDIR "/src/src/Saturno.API"
RUN dotnet build "Saturno.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Saturno.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Saturno.API.dll"]