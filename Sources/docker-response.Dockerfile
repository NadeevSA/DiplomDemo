FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app


COPY Response/. ./Response/
COPY Common/. ./Common/
COPY RabbitCommon/. ./RabbitCommon/
WORKDIR /app/Response
RUN dotnet restore


RUN dotnet publish Response.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/Response
ENV Postgres:DATABASE_HOST=db
ENV RabbitMq:Hostname=compose_rabbitmq_1
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:84
COPY --from=build-env /app/Response/out/. .

EXPOSE 84
ENTRYPOINT ["dotnet", "/app/Response/Response.dll"]