var sortOrder = true; //true = "asc", false ="desc"


$(document).on("click", ".btnHover ", function (e) {

    
    sortOrder = !sortOrder;


    var elem = $(this).closest('th');

    var columnName = elem.attr('id');

    e.preventDefault();

    //var columnName= $(this).id;
    

    var filters = {
        ingredientName: $('#ingredient').val(),
        categoryId: $('#category').val(),
        sortOrder: sortOrder,
        columnName: columnName
    };

    


    console.log(columnName);
   
    GetIngredients(filters);
   
     
});

