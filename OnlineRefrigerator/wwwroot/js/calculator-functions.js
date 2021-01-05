function GetIngredients(filters) {

    $.ajax({
        url: '/Calculator/ShowIngredients',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: filters

    })
        .done(function (result) {
            $('#displayCalculator')
                .html(result)
                .hide()
                .slideDown('slow');

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });

}

function GetCategories(filters) {

    $.ajax({
        url: '/Calculator/ShowCategories',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: filters

    })
        .done(function (result) {
            $('#renderCategories')
                .html(result)
               
           

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });

}
