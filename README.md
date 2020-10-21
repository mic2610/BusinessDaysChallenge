# designCrowd-tech-challenge
Business Day Counter Tech Challenge

# Design Choices and Architecture:

Business Day Counter Tech Challenge solution has been developed in .NET Core 3.1 using the existing boilerplate with many modifications. It has been developed using the Onion architecture (does include the data layer) consisting of the following in order from the inner most layer to the outer most layer:

* BusinessDayCounter.Core
* BusinessDayCounter.Business
* BusinessDayCounter.Web

This allows more moduler and cleaner code to be written and it also avoids any issues in Dependency Injection such as a circular dependency from when two services attempt to access each other. Tasks are displayed using MVC. Furthermore this solution also uses SOLID principles:

# SOLID Implementation:
* S - Single-responsiblity principle in the form of Individual services for a responsibility such as IBusinessDayCounter which is then injected into HomeController
* O - Open-closed principle: static extensions such as IEnumerableExtensions
* L - Liskov substitution principle
* I - Interface segregation principle: use of Interfaces for services
* D - Dependency Inversion Principle: Use of injected services into controllers and services

# Tasks
The web app displays all three tasks on initial load using the provided examples

# Unit Tests
Unit tests have been created using MSUnit and are available witin ~/Tests/BusinessDayCounter.Business.Tests/