let sortOrder = true; //true = "asc", false ="desc"

$(document).on("click", ".fa-sort ", function (e) {
        
    sortOrder = !sortOrder;

    let elem = $(this).closest('th');
    let columnName = elem.attr('id');
    e.preventDefault();
     
    let filters = {
        ingredientName: $('#ingredient').val(),
        categoryId: $('#category').val(),
        sortOrder: sortOrder,
        columnName: columnName
    };
        
    GetIngredients(filters);        
});