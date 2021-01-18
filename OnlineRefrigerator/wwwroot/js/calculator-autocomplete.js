$(document).ready(function () {
   
    $("#Ingredient_Name").autocomplete({
        
        source: function (request, response) {
            $.ajax({
                url: "/Calculator/Index",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        console.log(item);
                       
                        return { label: item.name, value: item.name };
                    }))

                }
            })
        },
        //messages: {
        //    noResults: "", results: ""
        //}

         
    });
}) 




function DisplayDetails(id) {
    console.log(id);
    $.ajax({
        url: '/Calculator/DisplayDetails',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: id

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