//$('button').on('click', function (event) {
//    event.preventDefault(); // Prevent default action.

//    var option = $(this).data('option');

//    var recipeId = $('#currentRecipe').text();


//    console.log(option);
//    console.log(recipeId);
//    $.ajax({
//        url: '/Recipes/CastVote',
//        type: 'POST',
//        cache: false,
//        async: true,
//        dataType: 'html',
//        data: {
//            vote: option,
//            id: recipeId
//        }
//    })
//        .done(function (result) {
//            //$('#displayRecipes')
//            //    .html(result)
//            //    .hide()
//            //    .slideDown('slow');
//            console.log("cyk: " + option)

//        }).fail(function (xhr) {
//            console.log('error: ' + xhr.status + ' - '
//                + xhr.statusText + ' - ' + xhr.responseText);
//        });

    
//});