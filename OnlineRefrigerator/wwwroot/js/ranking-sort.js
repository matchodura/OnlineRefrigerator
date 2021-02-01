var sortOrder = true; //true = "asc", false ="desc"


$(document).on("click", ".fa-sort ", function (e) {


    sortOrder = !sortOrder;


    var elem = $(this).closest('th');

    var columnName = elem.attr('id');

    e.preventDefault();

    //var columnName= $(this).id;


    var filters = {
        name: $('#Name').val(),
        score: $('#Score').val(),
        sortOrder: sortOrder,
        columnName: columnName
    };




    console.log(filters);

    GetScores(filters);


});

