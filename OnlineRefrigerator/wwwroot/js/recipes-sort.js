let sortOrder = true; //true = "asc", false ="desc"

$(document).on("click", ".fa-sort ", function (e) {

    sortOrder = !sortOrder;
    let elem = $(this).closest('th');
    let columnName = elem.attr('id');
    e.preventDefault();

    var filters = {
        recipeName: $('#recipes').val(),
        categoryId: $('#category').val(),
        sortOrder: sortOrder,
        columnName: columnName
    };      

    GetRecipes(filters);
});
