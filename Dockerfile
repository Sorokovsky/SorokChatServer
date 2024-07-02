# Використовуємо офіційний .NET SDK образ для побудови
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Встановлюємо робочу директорію
WORKDIR /app

# Копіюємо .csproj файл і відновлюємо залежності
COPY . ./
RUN dotnet restore

# Копіюємо всі файли і будуємо додаток
COPY . ./
RUN dotnet publish -c Release -o out

# Використовуємо офіційний .NET Runtime образ для запуску
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Встановлюємо робочу директорію
WORKDIR /app

# Копіюємо збірку з попереднього кроку
COPY --from=build /app/out .

# Виставляємо порт
EXPOSE 80

# Запускаємо додаток
ENTRYPOINT ["dotnet", "SorokChatServer.dll"]
