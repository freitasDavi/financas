FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# COPIA TUDO DA PASTA ATUAL PARA A PASTA -> app do container
COPY . ./
RUN dotnet restore 
RUN dotnet publish -c Release -o out

# Joga tudo que tinha antes fora e começa do zero
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /runtime-app
# Copia do ambiente de build-env -> da pasta /app/out -> pra raiz .
COPY --from=build-env /app/out .

# Install pt-BR locale
RUN apt-get update && apt-get install -y locales
RUN sed -i '/pt_BR.UTF-8/s/^# //g' /etc/locale.gen && locale-gen

ENV LANG pt_BR.UTF-8
ENV LANGUAGE pt_BR:pt
ENV LC_ALL pt_BR.UTF-8

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080
ENTRYPOINT [ "dotnet", "Fintech.dll" ]