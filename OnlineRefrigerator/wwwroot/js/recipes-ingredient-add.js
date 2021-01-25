

$("#addIngredient").click(function () {

    var numberOfIngredients = $(".form-control.m-input.ingredientBox").length;
    
    var html = '';
    html += '<div id="inputIngredientsRow">';
    html += '<div class="input-group mb-3">';
    html += '<input type="text" id="autocomplete-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].Name" class="form-control m-input ingredientBox" placeholder="Enter ingredient">';
    html += '<input type="hidden" id="ingredient-id-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].Id" class="form-control m-input">';    
    html += '<div class="input-group-append">';   
    html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
    html += '</div>';
    html += '</div>';


    // tutaj będzie dropdown soon m    
    html += '<div class="input-group mb-3">';
    html += '<select id="servingsOptions-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].ServingId" class="form-control m-input">';
    html += '</select>';
    html += '</div>';
    //a tutaj dzieją się rzeczy niestworzone tj. wartość danej porcji


    html += '<div class="input-group mb-3">';  
    html += '<input type="text" id="quantity-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].ServingQuantity" class="form-control m-input" placeholder="Enter quantity">';
    html += '</div>';
    $('#item-list-ingredients').append(html);


    //id = "test-' + numberOfElements +
    $("#autocomplete-" + numberOfIngredients).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Recipes/AutocompleteFindIngredient",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        console.log(item);

                        return { label: item.name, value: item.name, id: item.id };
                    }))

                }
            })
        },
        select: function (event, ui) {
            //alert(ui.item ? ("You picked '" + ui.item.label + "' with an ID of " + ui.item.id)
            //    : "Nothing selected, input was " + this.value);

            
            $("#ingredient-id-" + numberOfIngredients).val(ui.item.id);

            GetServingTypes(numberOfIngredients, ui.item.id);
        }

    });
        
});

// remove row
$(document).on('click', '#removeRow', function () {
    $(this).closest('#inputIngredientsRow').remove();
});


function GetServingTypes(numberOfIngredients, ingredientId) {

    var servings;

    $.ajax({
        url: '/Recipes/GetServingTypes',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "json",
        data: {
            id: ingredientId
        }


    })
        .done(function (result) {        
            $.each(result, function (i, p) {
               
                console.log(numberOfIngredients);
                $('#servingsOptions-' + numberOfIngredients).append($('<option/>').attr("value", p.id).text(p.name));
              
            });

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });

   
}
