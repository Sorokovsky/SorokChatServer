FROM bitnami/aspnet-core
WORKDIR .
COPY . .
CMD ["dotnet", "publish"]
ENTRYPOINT ["bin/Release/met8.0/SorokChatServer.exe"]