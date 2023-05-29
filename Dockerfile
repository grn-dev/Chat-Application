FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./Chat.Application.Service/Chat.Application.Service.csproj", "./Chat.Application.Service/Chat.Application.Service.csproj"]
COPY ["./Chat.Infra/Chat.Infra.csproj", "./Chat.Infra/Chat.Infra.csproj"]
COPY ["./Chat.Infra.Bus/Chat.Infra.Bus.csproj", "./Chat.Infra.Bus/Chat.Infra.Bus.csproj"]
COPY ["./Chat.Infra.Ioc/Chat.Infra.Ioc.csproj", "./Chat.Infra.Ioc/Chat.Infra.Ioc.csproj"]
COPY ["./Chat.Application/Chat.Application.csproj", "./Chat.Application/Chat.Application.csproj"]
COPY ["./Chat.Infra.Data/Chat.Infra.Data.csproj", "./Chat.Infra.Data/Chat.Infra.Data.csproj"]
COPY ["./Chat.Infra.Security/Chat.Infra.Security.csproj", "./Chat.Infra.Security/Chat.Infra.Security.csproj"]
COPY ["./Chat.Domain.Core/Chat.Domain.Core.csproj", "./Chat.Domain.Core/Chat.Domain.Core.csproj"]
COPY ["./Chat.Api/Chat.Api.csproj", "./Chat.Api/Chat.Api.csproj"]
COPY ["./EventBus/EventBus.Tests/EventBus.Tests.csproj", "./EventBus/EventBus.Tests/EventBus.Tests.csproj"]
COPY ["./EventBus/EventBusServiceBus/EventBusServiceBus.csproj", "./EventBus/EventBusServiceBus/EventBusServiceBus.csproj"]
COPY ["./EventBus/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "./EventBus/IntegrationEventLogEF/IntegrationEventLogEF.csproj"]
COPY ["./EventBus/EventBus/EventBus.csproj", "./EventBus/EventBus/EventBus.csproj"]
COPY ["./EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "./EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj"]
COPY ["./Chat.Domain/Chat.Domain.csproj", "./Chat.Domain/Chat.Domain.csproj"]
RUN dotnet restore "Chat.Api/Chat.Api.csproj"
COPY . .
WORKDIR "/src/Chat.Api/"
RUN dotnet build "Chat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chat.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.Api.dll"]
