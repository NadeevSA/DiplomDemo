FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app


COPY BFF/. ./BFF/
COPY Common/. ./Common/
WORKDIR /app/BFF
RUN dotnet restore


RUN dotnet publish BFF.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/BFF
ENV ASPNETCORE_URLS=http://+:8084
ENV ASPNETCORE_ENVIRONMENT=Development
COPY --from=build-env /app/BFF/out/. .

EXPOSE 8084
ENTRYPOINT ["dotnet", "/app/BFF/BFF.dll"]