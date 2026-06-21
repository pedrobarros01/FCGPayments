FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /build
COPY ["src/FCG.Payments/FCG.Payments.csproj", "build/FCG.Payments/"]
COPY ["src/FCG.Payments.Application/FCG.Payments.Application.csproj", "build/FCG.Payments.Application/"]
COPY ["src/FCG.Payments.Domain/FCG.Payments.Domain.csproj", "build/FCG.Payments.Domain/"]
COPY ["src/FCG.Payments.Infrastructure/FCG.Payments.Infrastructure.csproj", "build/FCG.Payments.Infrastructure/"]
COPY . .
RUN dotnet restore "build/FCG.Payments/FCG.Payments.csproj"
WORKDIR "/build/src/FCG.Payments"
RUN dotnet build "FCG.Payments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FCG.Payments.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet",  "FCG.Payments.dll"]
