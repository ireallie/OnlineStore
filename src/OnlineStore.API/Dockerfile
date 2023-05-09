#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OnlineStore.Api/OnlineStore.Api.csproj", "src/OnlineStore.Api/"]
COPY ["src/OnlineStore.BusinessLogic/OnlineStore.BusinessLogic.csproj", "src/OnlineStore.BusinessLogic/"]
RUN dotnet restore "src/OnlineStore.Api/OnlineStore.Api.csproj"
COPY . .
WORKDIR "/src/src/OnlineStore.Api"
RUN dotnet build "OnlineStore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineStore.Api.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStore.Api.dll"]