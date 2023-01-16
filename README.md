# News Api And Crawler

This repository contains backend application written in C# that consists of two projects: News Api and Crawler.

## Crawler 🕷
The Crawler project uses HtmlAgilityPack to collect items from news RSS files and saves them in a SQL Server database. As long as the application is running, it will extract items from a certain RSS list once an hour.

## News Api 👂
The News project contains three API controllers for the client application: one for the items, the second for the subjects of the items, and the third for the sources. To get data from the database, NewsContext uses the Entity Framework to return data from the tables.

### Prerequisites 📝

You need to have the following software installed on your machine:
- SqlServer
- .Net Core

## Getting Started ▶

Clone the repository and navigate to the root directory of the project. Run the command `dotnet restore` to restore the packages and dependencies, then run the command `dotnet run` to start the application.

Please note that you will need to configure the SQL Server connection string in the `appsettings.json` file before running the application.

## Screenshots or videos


## Built With

* [HtmlAgilityPack](https://html-agility-pack.net/) - HTML parser
* [.Net Core](https://dotnet.microsoft.com/download) - Cross-platform framework

## Authors 💻

* **Yael** - *Software Developer* - [<username>](https://github.com/benyehudayael)

## The project's target 🎯

I wrote this app to experiment and get better at writing code in C#.
