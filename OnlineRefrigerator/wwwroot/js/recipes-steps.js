$("#addRow").click(function () {

        var numberOfElements = $(".form-control.step").length;
    //  form - control m - input
        var counter = numberOfElements + 1;
        var html = '';

  

        html += '<div id="inputFormRow">';
        html += '<div class="input-group mb-3">';
        //html += '<p>krok numba: ' + counter + '</p>';
        //<textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
        //html += '<input type="text" name="StepList[' + numberOfElements + '].Text" class="form-control step" placeholder="Enter information" autocomplete="off">';
        html += '<textarea class="form-control step" placeholder="Next step " autocomplete="off" id="exampleFormControlTextarea1" rows="3" name="StepList[' + numberOfElements + '].Text"></textarea>'
        html += '<div class="input-group-append">';
        html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
        html += '</div>';
        html += '</div>';

        $('#item-list-steps').append(html);


    });

    // remove row
    $(document).on('click', '#removeRow', function () {
        $(this).closest('#inputFormRow').remove();
      
    });



