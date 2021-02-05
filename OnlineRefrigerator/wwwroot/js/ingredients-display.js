let displayUrl = '/Ingredients/ShowIngredients';
let partialDiv = '#displayIngredients';
let searchBox = '#ingredient';
let categoryName = '#category';
let autocompleteUrl = "/Ingredients/Autocomplete";
let sortOrder = true; //true = "asc", false ="desc"

//loads results when page is loaded
$(function () {
    GetResults(undefined, displayUrl, partialDiv);
});