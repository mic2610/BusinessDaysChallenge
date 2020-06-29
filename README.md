# wooliesx-tech-challenge
WooliesX Tech Challenge

# Design Choices and Architecture:

WooliesX Tech Challenge solution has been developed in .NET Core 3.1 using the existing boilerplate with many modifications. It has been developed using the Onion architecture consisting of the following in order from the inner most layer to the outer most layer:

* WooliesXTC.Core
* WooliesXTC.Data
* WooliesXTC.Business
* WooliesXTC.Api

This allows more moduler and cleaner code to be written and it also avoids any issues in Dependency Injection such as a circular dependency from when two services attempt to access each other. Furthermore this solution also uses SOLID principles:

# SOLID Implementation:
* S - Single-responsiblity principle in the form of Individual services for a responsibility such as ProductService which is then injected into HomeController
* O - Open-closed principle: static extensions such as IEnumerableExtensions
* L - Liskov substitution principle
* I - Interface segregation principle: use of Interfaces for services
* D - Dependency Inversion Principle: Use of injected services into controllers and services

# Exercise 1
RESTful API JSON request:

{baseUrl}/api/users

# Exercise 2
RESTful API JSON request:

* Sort Options: {baseUrl}/api/sort/
* Products Search: {baseUrl}/api/products/sortOption={sortOption} e.g: {baseUrl}/api/products/sortOption={ascending}