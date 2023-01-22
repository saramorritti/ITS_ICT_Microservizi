#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS http://*:5020

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["BookMicroservice/BookMicroservice.csproj", "BookMicroservice/"]
RUN dotnet restore "BookMicroservice/BookMicroservice.csproj"
COPY . .
WORKDIR "/src/BookMicroservice"
RUN dotnet build "BookMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookMicroservice.dll"]