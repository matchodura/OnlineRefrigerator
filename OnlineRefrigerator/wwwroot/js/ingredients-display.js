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

