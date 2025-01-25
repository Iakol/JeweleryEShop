$(document).ready(function ()
{
    $('.FaqEditBtn').each(function ()
    {
        $(this).on('click', function ()
        {
            var parent = $(this).parent().parent();

            $('#EditFAQID').val(parseInt(parent.find(".FaqId span").text(), 10)); 
            $('#EditQuestionUKR').val(parent.find(".FaqQuestionUKR span").text()); 
            $('#EditQuestionENG').val(parent.find(".FaqQuestionENG span").text()); 
            $('#EditAnswerUKR').val(parent.find(".FaqAnswerUKR span").text()); 
            $('#EditAnswerENG').val(parent.find(".FaqAnswerENG span").text()); 
            
        })

    })

})