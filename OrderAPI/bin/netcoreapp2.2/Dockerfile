FROM microsoft/dotnet:2.2-aspnetcore-runtime 
WORKDIR /app
EXPOSE 80
ENV OAUTH_ISSUER="http://172.16.89.91:5000"
COPY . .
ENTRYPOINT ["dotnet", "OrderAPI.dll"]