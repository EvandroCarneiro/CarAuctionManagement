
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/Api/Api.csproj src/Api/Api.csproj
COPY src/Api.Dto/Api.Dto.csproj src/Api.Dto/Api.Dto.csproj
COPY src/Domain/Domain.csproj src/Domain/Domain.csproj
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/Infrastructure.csproj
RUN dotnet restore "src/Api/Api.csproj"

COPY . .
WORKDIR "/src/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

#
# Service Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:12000
EXPOSE 12000
ENTRYPOINT ["dotnet", "BCA.CarAuctionManagement.Api.dll"]