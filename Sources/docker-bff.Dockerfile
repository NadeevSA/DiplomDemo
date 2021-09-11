FROM mcr.microsoft.com/dotnet/aspnet:5.0
FROM mcr.microsoft.com/dotnet/sdk:5.0
COPY BFF/bin/Release/net5.0/ App/
WORKDIR /App
ARG BUILD_CONFIGURATION=Release
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:8083  
EXPOSE 8083
ENTRYPOINT ["dotnet", "BFF.dll"]