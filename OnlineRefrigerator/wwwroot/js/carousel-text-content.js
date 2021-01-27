$(document).ready(function () {

    //default starting value
    $('.text-box-content').html("seks");

    $('#myCarousel').on('slide.bs.carousel', function (e) {
        var slideFrom = $(this).find('.active').index();
        var slideTo = $(e.relatedTarget).index();
        var id = slideTo;

        
        switch (id) {
            case 0:
                $('.text-box-content').html("aaaaa");
                break;
            case 1:
                $('.text-box-content').html("bbbbb");
                break;
            case 2:
                $('.text-box-content').html("ccccc");
                break;
            default:
            //the id is none of the above
        }
    });


});

    