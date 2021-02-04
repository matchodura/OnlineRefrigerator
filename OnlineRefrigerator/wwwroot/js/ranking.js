let sortOrder = true; //true = "asc", false ="desc"


$(document).ready(function () {
    GetScores();
});


//getting name of clicked column and sending it to controller via ajax call
$(document).on("click", ".fa-sort ", function (e) {

    sortOrder = !sortOrder;

    let elem = $(this).closest('th');
    let columnName = elem.attr('id');
    e.preventDefault();
      
    let filters = {
        name: $('#Name').val(),
        score: $('#Score').val(),
        sortOrder: sortOrder,
        columnName: columnName
    };

    GetScores(filters);
});


function GetScores(filters) {

    $.ajax({
        url: '/Ranking/ShowResults',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: filters

    })
        .done(function (result) {
            $('#displayResults')
                .html(result)
                .hide()
                .slideDown('slow');

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });
}