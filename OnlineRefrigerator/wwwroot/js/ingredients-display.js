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


$('#ingredient').on('keyup', function (e) {

    var filters = {
        ingredientName: $('#ingredient').val(),
        categoryId: $('#category').val()

    };

   
    GetIngredients(filters);

});

$("#ingredient").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/Ingredients/Autocomplete",
            type: "POST",
            dataType: "json",
            data: { Prefix: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    console.log(item);

                    return { label: item.name, value: item.name, id: item.id, servingType: item.servingType };
                }))

            }
        })
    },
    select: function (event, ui) {


        var filters = {
            ingredientName: $('#ingredient').val(),
            categoryId: $('#category').val()

        };


        GetIngredients(filters);
    }

});