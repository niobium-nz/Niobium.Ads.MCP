# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
WORKDIR /src

# Copy the solution file and any NuGet config
COPY --link *.sln ./
COPY --link NuGet.config ./

# Copy all .csproj files to the root of the workdir
COPY ./*/*.csproj ./

# Recreate the directory structure for each csproj file and move them into place
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

# Restore as distinct layers
RUN dotnet restore -a $TARGETARCH *.sln

# Copy source code and publish app
COPY --link . .
RUN dotnet publish -a $TARGETARCH -c $BUILD_CONFIGURATION --no-restore -o /app Niobium.Ads.MCP/*.csproj

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --link --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["./Niobium.Ads.MCP"]
