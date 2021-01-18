$(function () {
    GetIngredients();
});

$('#ingredient').on('keydown', function (e) {

    var filters = {
        ingredientName: $('#ingredient').val()
       
    };

    GetIngredients(ingredientName);
   

});