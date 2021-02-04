$(function () {
    GetRecipes();
});


$('#category').on('change', function (e) {

    let filters = {
        recipeName: $('#recipes').val(),
        categoryId: $('#category').val()

    };

    GetRecipes(filters);
});


$('#recipes').on('keyup', function (e) {

    let filters = {
        recipeName: $('#recipes').val(),
        categoryId: $('#category').val()

    };

    GetRecipes(filters);
});


$("#recipes").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/Recipes/AutocompleteFindRecipe",
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

        let filters = {
            recipeName: $('#recipes').val(),
            categoryId: $('#category').val()

        };

        GetRecipes(filters);
    }
});