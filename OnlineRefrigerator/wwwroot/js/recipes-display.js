let displayUrl = '/Recipes/ShowRecipes';
let partialDiv = '#displayRecipes';
let searchBox = '#recipes';
let categoryName = '#category';
let autocompleteUrl = "/Recipes/AutocompleteFindRecipe";
let sortOrder = true; //true = "asc", false ="desc"

//loads results when page is loaded
$(function () { 
    GetResults(undefined, displayUrl, partialDiv);
});