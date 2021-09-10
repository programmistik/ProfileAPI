#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ProfileAPI.csproj", "."]
RUN dotnet restore "./ProfileAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ProfileAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileAPI.csproj" -c Release -o /app/publish

FROM base AS final
EXPOSE 80
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfileAPI.dll"]