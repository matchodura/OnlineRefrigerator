$(document).ready(function () {
   
    $("#Name").autocomplete({
        
        source: function (request, response) {
            $.ajax({
                url: "/Calculator/Index",
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

            var id = ui.item.id;
            DisplayDetails(id);
        }

         
    });
}) 




function DisplayDetails(ingredientId) {

  
    $.ajax({
        url: '/Calculator/DisplayDetails',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: {
            id: ingredientId
        }


    })
        .done(function (result) {
            $('#renderDetails')
                .html(result)
                .hide()
                .slideDown('slow');

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });

}