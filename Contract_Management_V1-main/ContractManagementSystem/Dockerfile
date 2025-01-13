# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

#expose
EXPOSE 8080
EXPOSE 8081

# Copy the .csproj file and restore any dependencies (via 'dotnet restore')
COPY ["ContractManagementSystem.csproj", "."]
# COPY *.csproj ./
RUN dotnet restore "./ContractManagementSystem.csproj"

# Copy the rest of the application code
COPY ./ ./

# Publish the application to a folder in the container
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory in the container
WORKDIR /app

# Copy the published files from the build image
COPY --from=build /app/publish .

# Expose the port the app will run on
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ContractManagementSystem.dll"]
