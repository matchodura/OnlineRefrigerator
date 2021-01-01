function GetIngredients(filters) {

    $.ajax({
        url: '/Ingredients/ShowIngredients',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: filters

    })
        .done(function (result) {
            $('#displayIngredients')
                .html(result);            
                     
        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });

}
