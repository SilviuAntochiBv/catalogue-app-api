FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["API.Web/API.Web.csproj", "API.Web/"]
COPY ["API.Business/API.Business.csproj", "API.Business/"]
COPY ["API.Domain/API.Domain.csproj", "API.Domain/"]
COPY ["API.Data/API.Data.csproj", "API.Data/"]
RUN dotnet restore "API.Web/API.Web.csproj"
COPY . .
WORKDIR "/src/API.Web"
RUN dotnet build "API.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "API.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API.Web.dll"]