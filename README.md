## Online-Refrigerator
* [General info](#general-info)
* [Features](#features)
* [Live demo](#live-demo)
* [Site](#site)
* [Technologies](#technologies)
* [Setup](#setup)

## General info
Online-refrigerator is a web app that allows user to find recipes based on provided ingredients. It is common for many of us to have no idea what to do for dinner with things we have in our refrigerator. This project was created with such a thought to help with choosing process.

## Features
* Adding ingredients and recipes to database with validation of input data
* On ingredient creation, user can specify its photo, nutrition values and category
* Recipes are added with step-by step instructions and ingredients quantity that it needs to be made
* User can search recipes based on provided ingredients and display matching results with option to hide recipes that don't contain inputted ingredients
* Ranking and viewing current scores of recipes
* Calculator of ingredient nutrition values
* Sorting results by type, preparation time, rate, values
* AJAX calls with partial views to increase user experience

## Live demo
[Link](https://onlinerefrigerator.azurewebsites.net) (May take a while to load as it uses free azure service)

## Site

(gifs make take a while to load)

Main page is a home screen for the site. Here user has access to navbar functions and login panel.

![](https://drive.google.com/uc?export=view&id=1mtskk10sGfqw-I3fELInRULt6QIwF-Xv)

Important part of the site are ingredients and recipes base. When on these pages user can search for interesing item. Search and sorting are based on AJAX calls so there is not need to refresh the page.

![](https://drive.google.com/uc?export=view&id=1jU1q8P34zzYBOyLmDb4g00tZE31DBPgM)

Logged in user can add ingredient and provide most important informations about it - name, nutrition values, photo, category and serving type.

![](https://drive.google.com/uc?export=view&id=1KZUh4kVmjr_YZNRvY-v4ihEbGqN1RW1W)

Adding of recipes is an option available for logged in users. In similar manner as ingredient creation, users input basic info - recipe name, photo, category and preparation time. More complex options include adding step-by-step instructions and setting up inngredients list that are neede for this dish to make.

![](https://drive.google.com/uc?export=view&id=14SbXBXfnlaQxgzisqZu3S6GEYtCN-HA_)

Website defining function is recipe finder. Users input available ingredients and create a list of items, based on which recipes that cointains such items will be found and displayed. Also an option to display recipes with missing ingredients that have not been provided by the user to be also shown.

![](https://drive.google.com/uc?export=view&id=1J25OiFpxom0Pn0LmQIYHaIXQFQ2X0yh9)

Calculator is a subsection in which users can find existing ingredients in the database and check how much calories do they have.

![](https://drive.google.com/uc?export=view&id=1A-agT6ydaw31G0EZZpsNa-SKrOLMM_mr)

Rating provides an option to rate recipes by users themselves. Ranking page display current scores of recipes in the database.

![](https://drive.google.com/uc?export=view&id=1drle1cQKKFdFHw58-9dug9JQS2nt244w)


## Technologies
Project was created with:
* ASP.NET MVC 4.8 
* .NET Core 3.1 
* C# 8.0
* Entity Framework Core 
* LINQ
* MS SQL
* HTML5, CSS3, JS6
	
## Setup
If you are interested in running this project locally, follow these steps(note: without database content):

0. Ensure .NET is installed
1. Clone or download repository to desired folder
2. From command prompt navigate to folder 
OnlineRefrigerator
3. When in directory, simply write command
```
c:\OnlineRefrigerator>dotnet run
```
4. Then look for localhost port, and put it into your browser
```
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001 
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
```
