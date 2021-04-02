# Description

Simple Example on how to use GraphQL for .net app with multiple Schema

## How to run

- open terminal/terminal
- go to project root path
- run `dotnet restore`
- goto src/NetGraphQL.Web
- run `dotnet run`

## PlayGround URL:

User : https://localhost:5001/ui/user/playground
App: https://localhost:5001/ui/app/playground

## Issue:

With multiple `schema`, the subscription only work for the first register (see on Startup.cs), the other subscription does not work.

## License

MIT
