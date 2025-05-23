$(document).ready(function() {
    $('#btnEffect').on('click', function() {
        alert('버튼클릭완료')
    })
    $('#box').css ({
        'background-color' : 'orange',
        'width' : 100,
        'height' :100,
        'margin' : '5px'
    }).on('click' , function() {
        $(this).css('background-color' , 'red')
    }).on('mouseenter' , function() {
        $(this).css('background-color' , 'blue')
    }).on('mouseleave' , function() {
        $(this).css('background-color' , 'orange')
        $(this).css('border-radius' , '0')

    })
    $('#btnToggle').click(function() {
        $('.page').fadeToggle('fast')
   }) 
   $('.box').css( {
    width : 100,
    height : 50 ,
    backgroundColor : 'red'
   })

   $('#btnAnimation').click(function() {
    $('.box').animate({
        width:400,
        opacity : 0.6,
    }, 1000)
}) 
}) 

