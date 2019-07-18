FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["MvcClient/MvcClient.csproj", "MvcClient/"]
RUN dotnet restore "MvcClient/MvcClient.csproj"
COPY . .
WORKDIR "/src/MvcClient"
RUN dotnet build "MvcClient.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MvcClient.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MvcClient.dll"]