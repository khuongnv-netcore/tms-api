#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /src
COPY ["CORE-API.csproj", "CORE-API/"]
RUN dotnet restore "CORE-API/CORE-API.csproj"
COPY . .
RUN dotnet build "CORE-API.csproj" -c Release -o /app/build
RUN dotnet publish "CORE-API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80
COPY --from=0 /app/publish .
ENTRYPOINT ["dotnet", "COREAPI.dll"]