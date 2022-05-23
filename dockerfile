FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /opt/build

COPY Sarf/ Sarf/
WORKDIR Sarf

RUN dotnet restore Sarf.sln

RUN mkdir /opt/app
RUN dotnet publish Sarf.sln -c Release -o /opt/app/

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /opt/app
COPY --from=build-env /opt/build/ .
WORKDIR /opt/app/Sarf/bin/Release/net6.0

EXPOSE 7002

ENTRYPOINT ["dotnet", "Sarf.dll"]
