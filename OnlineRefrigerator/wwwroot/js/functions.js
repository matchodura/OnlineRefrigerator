/*makes query to db via ajax call, based on provided filters e.g category or name
 url is a route to action in controller eg. '/Recipes/ShowRecipes'
 partialDiv is a name of div placeholder that displays returned from controller partial view e.g '#displayRecipes'
*/
function GetResults(filters, url, partialDiv) {
    console.log("url: " + url);
    console.log("partialDiv: " + partialDiv);

    $.ajax({
        url: url,
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: filters

    })
        .done(function (result) {
            $(partialDiv)
                .html(result)
                .hide()
                .slideDown('slow');

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });
}


/*displays autocomplete results in search box via ajax call
 *searchBox stands for div id on page that is being used on
 *autocompleteUrl is a route to action in controller that fetches data from database e.g  "/Recipes/AutocompleteFindRecipe"
 */
$(searchBox).autocomplete({
    source: function (request, response) {
        $.ajax({
            url: autocompleteUrl,
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

        let filters = {
            name: $(searchBox).val(),
            category: $(categoryName).val()

        };

        GetResults(filters, displayUrl, partialDiv);
    }
});


//updates results based on category change
$(categoryName).on('change', function (e) {

    let filters = {
        name: $(searchBox).val(),
        category: $(categoryName).val()
    };

    GetResults(filters, displayUrl, partialDiv);
});


//updates results based on entered char
$(searchBox).on('keyup', function (e) {

    let filters = {
        name: $(searchBox).val(),
        category: $(categoryName).val()
    };

    GetResults(filters, displayUrl, partialDiv);
});


//sorting function
$(document).on("click", ".fa-sort ", function (e) {

    sortOrder = !sortOrder;
    let elem = $(this).closest('th');
    let columnName = elem.attr('id');
    e.preventDefault();

    let filters = {
        name: $(searchBox).val(),
        category: $(categoryName).val(),
        sortOrder: sortOrder,
        columnName: columnName
    };

    GetResults(filters, displayUrl, partialDiv);
});