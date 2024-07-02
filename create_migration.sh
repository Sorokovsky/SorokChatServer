echo "Enter a migration name: "
read name
dotnet ef migrations add $name --project Database --startup-project SorokChatServer
dotnet ef database update --project Database --startup-project SorokChatServer