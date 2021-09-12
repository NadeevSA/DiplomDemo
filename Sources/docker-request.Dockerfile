FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app


COPY Request/. ./Request/
COPY Common/. ./Common/
WORKDIR /app/Request
RUN dotnet restore


ENV ASPNETCORE_URLS=http://+:8082
RUN dotnet publish Request.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/Request
ENV ASPNETCORE_URLS=http://+:8082 
COPY --from=build-env /app/Request/out/. .

EXPOSE 82
ENTRYPOINT ["dotnet", "/app/Request/Request.dll"]