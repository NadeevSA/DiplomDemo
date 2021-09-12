FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app


COPY Response/. ./Response/
COPY Common/. ./Common/
WORKDIR /app/Response
RUN dotnet restore


ENV ASPNETCORE_URLS=http://+:84
RUN dotnet publish Response.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/Response
ENV ASPNETCORE_URLS=http://+:84
COPY --from=build-env /app/Response/out/. .

EXPOSE 84
ENTRYPOINT ["dotnet", "/app/Response/Response.dll"]