FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app


COPY Request/. ./Request/
COPY Common/. ./Common/
COPY RabbitCommon/. ./RabbitCommon/
WORKDIR /app/Request
RUN dotnet restore


RUN dotnet publish Request.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/Request
ENV Postgres:DATABASE_HOST=db
ENV RabbitMq:Hostname=compose_rabbitmq_1
ENV ASPNETCORE_URLS=http://+:8082 
ENV ASPNETCORE_ENVIRONMENT=Development
COPY --from=build-env /app/Request/out/. .

EXPOSE 82
ENTRYPOINT ["dotnet", "/app/Request/Request.dll"]