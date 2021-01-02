$(function () {

    $("#add").click(function (e) {
        e.preventDefault();
        var i = $(".items").length;
        var n = '<input type="text" class="items" name="ListItems[' + i + '].Text" />';
        $("#item-list").append(n);
    });

});