    $("#addRow").click(function () {

        var numberOfElements = $(".form-control.m-input").length;
      //  form - control m - input
        var html = '';
        html += '<div id="inputFormRow">';
        html += '<div class="input-group mb-3">';
        html += '<input type="text" name="StepList[' + numberOfElements + '].Text" class="form-control m-input" placeholder="Enter information" autocomplete="off">';
        html += '<div class="input-group-append">';
        html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
        html += '</div>';
        html += '</div>';

        $('#item-list').append(html);


    });

    // remove row
    $(document).on('click', '#removeRow', function () {
        $(this).closest('#inputFormRow').remove();
    });



