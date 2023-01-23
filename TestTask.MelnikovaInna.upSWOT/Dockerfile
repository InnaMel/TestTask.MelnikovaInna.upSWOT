#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestTask.MelnikovaInna.upSWOT/TestTask.MelnikovaInna.upSWOT.csproj", "TestTask.MelnikovaInna.upSWOT/"]
RUN dotnet restore "TestTask.MelnikovaInna.upSWOT/TestTask.MelnikovaInna.upSWOT.csproj"
COPY . .
WORKDIR "/src/TestTask.MelnikovaInna.upSWOT"
RUN dotnet build "TestTask.MelnikovaInna.upSWOT.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTask.MelnikovaInna.upSWOT.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTask.MelnikovaInna.upSWOT.dll"]