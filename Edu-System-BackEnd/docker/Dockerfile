# Базовий імідж для запуску
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Відкриваємо порти для HTTP і HTTPS
EXPOSE 8080
EXPOSE 8081

# Копіюємо сертифікати (опціонально, якщо не використовуєш volume)
# COPY aspnetapp.pfx /https/aspnetapp.pfx

# SDK для збірки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . ./

RUN dotnet restore "Edu-System-BackEnd.csproj"
RUN dotnet publish "Edu-System-BackEnd.csproj" -c Release -o /publish

# Фінальний образ
FROM base AS final
WORKDIR /app
COPY --from=build /publish ./

# ❗️Вказуємо середовище (можеш прибрати, якщо вказується в docker-compose)
ENV ASPNETCORE_URLS="https://+:8081;http://+:8080"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="Aboba123!"

ENTRYPOINT ["dotnet", "Edu-System-BackEnd.dll"]
