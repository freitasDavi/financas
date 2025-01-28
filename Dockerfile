# Stage 1: build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

#Restore
COPY ["Fintech/Fintech.csproj", "Fintech/"]
RUN dotnet restore 'Fintech/Fintech.csproj'

#Build
COPY ["Fintech", "Fintech/"]
WORKDIR /Fintech
RUN dotnet build 'Fintech.csproj' -c Release -o /app/build

#Publish
FROM build as publish
RUN dotnet publish 'Fintech.csproj' -c Release -o /app/publish

#Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Fintech.dll" ]
