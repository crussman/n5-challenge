FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /src

COPY . .

RUN dotnet restore ./N5.Challenge.WebApi/N5.Challenge.WebApi.csproj

WORKDIR /src/N5.Challenge.WebApi

EXPOSE 8080
EXPOSE 8081
