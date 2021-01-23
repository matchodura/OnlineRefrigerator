function GetRecipes() {
    console.log("b");

    $.ajax({
        url: '/Finder/DisplayRecipes',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        //data: filters

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
