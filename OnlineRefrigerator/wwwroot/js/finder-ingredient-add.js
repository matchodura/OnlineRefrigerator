var ingredients = [];
var missingIngredientsOn;
var ignoreHerbs;

$(document).ready(function () {
    GetRecipes();
});

$(document).on("click", "#customSwitch1", function () {
    if ($(this).is(':checked'))
        missingIngredientsOn = true;

    else
        missingIngredientsOn = false;
});


$(document).on("click", "#customSwitch2", function () {
    if ($(this).is(':checked'))
        ignoreHerbs = true;

    else
        ignoreHerbs = false;
});


$(".custom-control.custom-switch").change(function () {
    GetRecipes(ingredients, missingIngredientsOn, ignoreHerbs);
});


$("#addIngredient").click(function () {

    var numberOfIngredients = $(".form-control.m-input.ingredientBox").length;
     

    var html = '';
    html += '<div id="inputIngredientsRow">';
    html += '<div class="input-group mb-3">';
    html += '<input type="text" id="test-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].Name" class="form-control m-input ingredientBox" placeholder="Enter ingredient">';
    html += '<input type="hidden" id="ingredient-id-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].Id" class="form-control m-input" placeholder="tutaj id">';
    html += '<div class="input-group-append">';
    html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
    html += '</div>';
    html += '</div>';

    $('#item-list-ingredients').append(html);
    //id = "test-' + numberOfElements +


    $("#test-" + numberOfIngredients).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Recipes/AutocompleteFindIngredient",
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
            //alert(ui.item ? ("You picked '" + ui.item.label + "' with an ID of " + ui.item.id)
            //    : "Nothing selected, input was " + this.value);

            
            $("#ingredient-id-" + numberOfIngredients).val(ui.item.id);
            //console.log(ui.item.id);
            var ingredientId = ui.item.id;
            ingredients.push(ingredientId);       
            
            GetRecipes(ingredients, missingIngredientsOn, ignoreHerbs);
        }

    });
});


// remove row
$(document).on('click', '#removeRow', function () {
    $(this).closest('#inputIngredientsRow').remove();
    ingredients.pop();
    GetRecipes(ingredients, missingIngredientsOn, ignoreHerbs);
});

function GetRecipes(ingredients, missingIngredientsOn, ignoreHerbs) {

    
    $.ajax({
        url: '/Finder/DisplayRecipes',
        type: 'POST',
        cache: false,
        async: true,
        dataType: 'html',
        data: {
            ids: ingredients,
            displayMissing: missingIngredientsOn,
            ignoreHerbs: ignoreHerbs
        }
    })
        .done(function (result) {
            $('#displayRecipes')
                .html(result)
                .hide()
                .slideDown('slow');

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });

}
