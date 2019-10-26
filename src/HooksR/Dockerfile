FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["HooksR/HooksR.csproj", "HooksR/"]
RUN dotnet restore "HooksR/HooksR.csproj"
COPY . .
WORKDIR "/src/HooksR"
RUN dotnet build "HooksR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HooksR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HooksR.dll"]