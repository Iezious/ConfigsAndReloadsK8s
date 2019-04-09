FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
RUN rm -f /app/out/config.app.json
RUN rm -f /app/out/config.dc.json

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 5555 
ENTRYPOINT ["dotnet", "ConfigReadApp.dll"]