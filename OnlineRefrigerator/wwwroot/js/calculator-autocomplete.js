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
                        return { label: item.name, value: item.name, id: item.id };
                    }))
                }
            })
        },
        select: function (event, ui) {      
            let id = ui.item.id;
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