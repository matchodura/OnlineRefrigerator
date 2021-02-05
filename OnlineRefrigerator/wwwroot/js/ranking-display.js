let sortOrder = true; //true = "asc", false ="desc"
let displayUrl = '/Ranking/ShowResults';
let partialDiv = '#displayResults';
let searchBox = '#recipes';
let categoryName = '#category';

$(document).ready(function () {  
    GetResults(undefined, displayUrl, partialDiv);
});