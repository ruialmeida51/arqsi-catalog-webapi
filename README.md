# ARQSI - PRODUCT CATALOG

Restful API Web Server written in C#.

It was an academic project, as a first contact with the language and in building restful apis. It allows for catalogs (of products) management and product configuration. Product in this scenario are closets, that can have multiple components, such as drawers, for example. 


## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
.NET Core
```

### Installing

To get your server up and running on your machine, open the terminal inside the project folder and execute the following commands:

First, let's build our project:

```
dotnet build
```

Create the migrations to generate our database

```
dotnet ef migrations add InitCreate
```

We'll be using a local SQLITE database for testing/developing purposes

```
dotnet ef database update
```

##### (Optional) Run bootstrap.sql in the SQL folder, to populate the database with some mock data.

Now, just run the server and send your requests

```
dotnet run
```

## API Examples:

Find all products in the database:

```
https://localhost/5001/api/product/
```

## Running the tests

To run the tests with the .net environment:

```
dotnet test
```

There are some postman integration tests included as well in the project.

## Built With
* [.Net Core](https://dotnet.github.io/) 
* [ASPNET MVC](https://www.asp.net/mvc) 

## Authors

* **Rui Almeida** -[Github](https://github.com/ruialmeida51)
* **João Rocha** - [Github](https://github.com/alm0sttt)

## License

This project is licensed under the Mozilla Public License Version 2.0 License - see the [LICENSE](LICENSE) file for details



