$("#addIngredient").click(function () {

    var numberOfIngredients = $(".form-control.m-input.xd").length;

    var html = '';
    html += '<div id="inputIngredientsRow">';
    html += '<div class="input-group mb-3">';
    html += '<input type="text" id="test-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].Name" class="form-control m-input xd" placeholder="Enter ingredient">';
    html += '<input type="hidden" id="ingredient-id-' + numberOfIngredients + '" name="IngredientsList[' + numberOfIngredients + '].Id" class="form-control m-input xde" placeholder="tutaj id">';
    html += '<div class="input-group-append">';
    html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
    html += '</div>';
    html += '</div>';

    $('#item-list-ingredients').append(html);
    //id = "test-' + numberOfElements +
    $("#test-" + numberOfIngredients).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Recipes/Autocomplete",
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

            console.log(ui.item.id);
            $("#ingredient-id-" + numberOfIngredients).val(ui.item.id);
        }

    });
});






// remove row
$(document).on('click', '#removeRow', function () {
    $(this).closest('#inputIngredientsRow').remove();
});