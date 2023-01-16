# News Viewer of RSS Feeds

This repository contains the implementation of a crawling and scraping of news items from RSS feeds. The items are extracted using HtmlAgilityPack and persisted in SqlServer DB. The .Net Core Web API allows retrieval of the data for the client app. An angular application is also built to view the items.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

You need to have the following software installed on your machine:
- SqlServer
- .Net Core
- Angular

### Installing

Clone the repository to your local machine:

```
git clone https://github.com/<username>/News-Viewer-RSS-Feeds.git
```

Navigate to the project folder and install the necessary packages:

```
cd News-Viewer-RSS-Feeds
npm install
```

Run the application:

```
ng serve
```

## Built With

* [HtmlAgilityPack](https://html-agility-pack.net/) - HTML parser
* [.Net Core](https://dotnet.microsoft.com/download) - Cross-platform framework
* [Angular](https://angular.io/) - Front-end web framework

## Contributing

Please read [CONTRIBUTING.md](https://github.com/<username>/News-Viewer-RSS-Feeds/blob/master/CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **<Your name>** - *Initial work* - [<username>](https://github.com/<username>)

See also the list of [contributors](https://github.com/<username>/News-Viewer-RSS-Feeds/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/<username>/News-Viewer-RSS-Feeds/blob/master/LICENSE) file for details.
