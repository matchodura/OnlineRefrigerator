$(function () {
    GetIngredients();
    GetCategories();
});


$(document).on('change', "#categorySearchNew", function (e) {

    var filters = {
        itemName: $('#itemSearch').val(),
        typeId: $('#typeSearch').val(),
        categoryId: $('#categorySearchNew').val()
    };

    GetIngredients(filters);
});



$('#typeSearch').on('change', function (e) {

    var filters = {
        itemName: $('#itemSearch').val(),
        typeId: $('#typeSearch').val(),
        categoryId: $('#categorySearchNew').val()
    };

   
    GetIngredients(filters);

});


$('#itemSearch').on('keydown', function (e) {

    var filters = {
        itemName: $('#itemSearch').val(),
        typeId: $('#typeSearch').val()

    };

    GetIngredients(filters);

});



