$(document).on('change', "#ServingQuantity", function (e) {


    var data = {
        fat: $('#Fat').text(),
        carbs: $('#Carbs').text(),
        protein: $('#Protein').text(),
        energy: $('#Energy').text(),
        servingValue: $('#ServingValue').text(),
        servingType: $('#SelectedServing').val(),
        servingQuantity: $('#ServingQuantity').val()


    }
  
    //console.log(data);
    console.log(data);
    GetResults(data);
});



function GetResults(data) {

    var calculatorParameters = {
        fat: data.fat,
        carbs: data.carbs,
        protein: data.protein,
        energy: data.energy,
        servingType: data.servingType,
        servingValue: data.servingValue,
        servingQuantity: data.servingQuantity

    }
    


    $.ajax({
        url: '/Calculator/DisplayResults',
        type: 'POST',
        cache: false,
        async: true,        
        data: calculatorParameters

    })
        .done(function (result) {
            $('#renderResults')
                .html(result)
              

        }).fail(function (xhr) {
            console.log('error: ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });

}