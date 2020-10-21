# Business Day Counter Tech Challenge

--------------------------------------
Task One: Weekdays Between Two Dates
--------------------------------------
Calculates the number of weekdays in between two dates.
● Weekdays are Monday, Tuesday, Wednesday, Thursday, Friday.
● The returned count should not include either firstDate or secondDate -
e.g. between Monday 07-Oct-2013 and Wednesday 09-Oct-2013 is one weekday.
● If secondDate is equal to or before firstDate, return 0.Expected Results

-------------------------------------------
​Task Two: Business Days Between Two Dates
-------------------------------------------
Calculate the number of business days in between two dates.
● Business days are Monday, Tuesday, Wednesday, Thursday, Friday, but excluding any
dates which appear in the supplied list of public holidays.
● The returned count should not include either firstDate or secondDate - e.g. between Monday
07-Oct-2013 and Wednesday 09-Oct-2013 is one weekday.
● If secondDate is equal to or before firstDate, return 0.
Expected Results
Sample list of Public Holidays:
● 25​th​ December 2013
● 26​th​ December 2013
● 1​st​ January 2014

--------------------------------------
​Task Three: More Holidays
--------------------------------------
Design a data structure or hierarchy of structures which can define public holidays in a more
complex fashion than simple dates.This should cater for things such as:

- Public holidays which are always on the same day, e.g. Anzac Day on April 25th every year.
- Public holidays which are always on the same day, except when that falls on a weekend. e.g. New
Year's Day on January 1st every year, unless that is a Saturday or Sunday, in which case the
holiday is the next Monday.
- Public holidays on a certain occurrence of a certain day in a month. e.g. Queen's Birthday on the
second Monday in June every year.

Given this data structure, the BusinessDaysBetweenTwoDates() function should be able to be
extended to take a list of public holiday rules, rather than a list of DateTimes, and calculate the
number of business days between two dates using those rules to define public holidays.

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