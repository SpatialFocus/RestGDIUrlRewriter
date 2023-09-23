FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["src/UbaUrlRewrite.Proxy/UbaUrlRewrite.Proxy.csproj", "src/UbaUrlRewrite.Proxy/"]
COPY ["src/UbaUrlRewrite.Data/UbaUrlRewrite.Data.csproj", "src/UbaUrlRewrite.Data/"]
COPY ["src/UbaUrlRewrite.Management.Business/UbaUrlRewrite.Management.Business.csproj", "src/UbaUrlRewrite.Management.Business/"]
RUN dotnet restore "src/UbaUrlRewrite.Proxy/UbaUrlRewrite.Proxy.csproj"
COPY . .
WORKDIR "/src/src/UbaUrlRewrite.Proxy"
RUN dotnet build "UbaUrlRewrite.Proxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UbaUrlRewrite.Proxy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UbaUrlRewrite.Proxy.dll"]