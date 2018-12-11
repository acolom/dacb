@echo off

dotnet build .\src\minsk.sln
dotnet test .\src\Dacb.Tests\Dacb.Tests.csproj