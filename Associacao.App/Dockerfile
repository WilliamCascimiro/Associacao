#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Associacao.App/Associacao.App.csproj", "Associacao.App/"]
RUN dotnet restore "Associacao.App/Associacao.App.csproj"
COPY . .
WORKDIR "/src/Associacao.App"
RUN dotnet build "Associacao.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Associacao.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Associacao.App.dll"]