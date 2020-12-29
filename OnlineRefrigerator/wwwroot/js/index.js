//$(document).ready(function () {
//    alert("I am an alert box!");
//});



$(function () {
    GetIngredients();
});



$('#btnSearch').on('click', function (e) {
   
    var filters = {
        ingredient: $('#ingredient').val(),
        categoryId: $('#category').val()

    };
    GetIngredients(filters);
    
});


function GetIngredients(filters) {
    
    $.ajax({
        url: '/Ingredients/ShowIngredients',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: filters

    })
        .done(function (result) {            
            $('#dupa').html(result);
        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });



}