//$(document).ready(function () {
//    alert("I am an alert box!");
//});



$(function () {
    GetIngredients();
});


$('#category').on('change', function (e) {

    var filters = {
        ingredientName: $('#ingredient').val(),
        categoryId: $('#category').val()

    };

    GetIngredients(filters);

});


$('#ingredient').on('keydown', function (e) {

    var filters = {
        ingredientName: $('#ingredient').val(),
        categoryId: $('#category').val()

    };

    GetIngredients(filters);

});

//$(document).on('click', "#category", function (e) {

//    var sortValue = "asc";

//    var filters = {
//        ingredientName: $('#ingredient').val(),
//        categoryId: $('#category').val(),
//        sort: sortValue
//    };

   

//    //sortValue.append(filters);


//   // alert(filters.sort);
//    GetIngredients(filters);
//});



