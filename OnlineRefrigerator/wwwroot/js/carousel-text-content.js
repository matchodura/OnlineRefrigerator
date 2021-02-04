$(document).ready(function () {
    let firstSlide = '';   
    firstSlide += '<h1>Online-Refrigerator</h1>'
    firstSlide += '<div class="carousel-subtext">';
    firstSlide += '<p>Do you have some unused food in refrigerator?</p>'
    firstSlide += '<p>Are you hungry?</p>'
    firstSlide += '<p>Find out your new dinner idea by inputing food you have no idea what to do with!</p>'
    firstSlide += '</div>'
   

    let secondSlide = ''; 
    secondSlide += '<h1>Recipes base</h1>'
    secondSlide += '<div class="carousel-subtext">';
    secondSlide += '<p>Need inspiration?</p>'
    secondSlide += '<p>Check recipe base with step by step instructions!</p>'
    secondSlide += '</div>'


    let thirdSlide = '';   
    thirdSlide += '<h1>Ingredients base</h1>'
    thirdSlide += '<div class="carousel-subtext">';
    thirdSlide += '<p>How many different types of fruits are there?</p>'
    thirdSlide += '<p>Check ingredients base to find out!</p>'
    thirdSlide += '</div>'


    let fourthSlide = '';  
    fourthSlide += '<h1>Find a recipe</h1>'
    fourthSlide += '<div class="carousel-subtext">';
    fourthSlide += '<p>Too many weird ingredients in your refrigerator?</p>'
    fourthSlide += '<p>Write them in recipe finder and see what comes up!</p>'
    fourthSlide += '</div>'
 

    let fifthSlide = '';
    fifthSlide += '<h1>Calculator</h1>'
    fifthSlide += '<div class="carousel-subtext">';
    fifthSlide += '<p>Are two apples enough for a dinner?</p>'
    fifthSlide += '<p>Calculate their nutricious values in calculator to find out!</p>'
    fifthSlide += '</div>'
  

    //default starting value
    $('.text-box-content').html(firstSlide);

    $('#myCarousel').on('slide.bs.carousel', function (e) {
        var slideFrom = $(this).find('.active').index();
        var slideTo = $(e.relatedTarget).index();
        var id = slideTo;
     
        switch (id) {
            case 0:
                $('.text-box-content').html(firstSlide);
                break;
            case 1:
                $('.text-box-content').html(secondSlide);
                break;
            case 2:
                $('.text-box-content').html(thirdSlide);
                break;
            case 3:
                $('.text-box-content').html(fourthSlide);
                break;
            case 4:
                $('.text-box-content').html(fifthSlide);
                break;
            default:
          
        }
    });


});

    