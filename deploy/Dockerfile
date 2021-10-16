FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./src/Route256.MerchandiseService/*.csproj ./src/Route256.MerchandiseService/
RUN dotnet restore

# copy everything else and build app
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Route256.MerchandiseService.dll"]