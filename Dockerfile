FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["StudentCourseManagement.Presentation/StudentCourseManagement.Presentation.csproj", "StudentCourseManagement.Presentation/"]
RUN dotnet restore "StudentCourseManagement.Presentation/StudentCourseManagement.Presentation.csproj"
COPY . .
WORKDIR "/src/StudentCourseManagement.Presentation"
RUN dotnet build "StudentCourseManagement.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentCourseManagement.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentCourseManagement.Presentation.dll"]
