# ������������� ��������� .NET SDK ����� ��� ��������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# ������������ ������ ���������
WORKDIR /app

# ������� .csproj ���� � ���������� ���������
COPY . ./
RUN dotnet restore

# ������� �� ����� � ������ �������
COPY . ./
RUN dotnet publish -c Release -o out

# ������������� ��������� .NET Runtime ����� ��� �������
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# ������������ ������ ���������
WORKDIR /app

# ������� ����� � ������������ �����
COPY --from=build /app/out .

# ����������� ����
EXPOSE 5000

# ������������ ����� ����������
ENV ASPNETCORE_URLS=http://*:5000

# ��������� �������
ENTRYPOINT ["dotnet", "SorokChatServer.dll"]
