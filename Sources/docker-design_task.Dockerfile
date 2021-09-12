FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app


COPY DesignTask/. ./DesignTask/
COPY Common/. ./Common/
COPY RabbitCommon/. ./RabbitCommon/
WORKDIR /app/DesignTask
RUN dotnet restore


RUN dotnet publish DesignTask.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/DesignTask

ENV ASPNETCORE_URLS=http://+:85
ENV Postgres:DATABASE_HOST=db
ENV RabbitMq:Hostname=compose_rabbitmq_1
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build-env /app/DesignTask/out/. .

EXPOSE 85
ENTRYPOINT ["dotnet", "/app/DesignTask/DesignTask.dll"]