$("#addIngredient").click(function () {

    let numberOfIngredients = $(".form-control.m-input.ingredientBox").length;
    
    let html = '';
    html += '<div id="inputIngredientsRow">';
    html += '<div class="input-group mb-3">';
    html += '<input type="text" id="autocomplete-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].Name" class="form-control m-input ingredientBox" placeholder="Enter ingredient">';
    html += '<input type="hidden" id="ingredient-id-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].Id" class="form-control m-input">';    
    html += '<div class="input-group-append">';   
    html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
    html += '</div>';
    html += '</div>';  
    html += '<div class="input-group mb-3">';
    html += '<select id="servingsOptions-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].ServingId" class="form-control m-input">';
    html += '</select>';
    html += '</div>';
    html += '<div class="input-group mb-3">';  
    html += '<input type="text" id="quantity-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].ServingQuantity" class="form-control m-input" placeholder="Enter quantity">';
    html += '</div>';

    $('#item-list-ingredients').append(html);    
    $("#autocomplete-" + numberOfIngredients).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Recipes/AutocompleteFindIngredient",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {                     

                        return { label: item.name, value: item.name, id: item.id };
                    }))

                }
            })
        },
        select: function (event, ui) {       
            
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
                             
                $('#servingsOptions-' + numberOfIngredients).append($('<option/>').attr("value", p.id).text(p.name));
              
            });

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });   
}