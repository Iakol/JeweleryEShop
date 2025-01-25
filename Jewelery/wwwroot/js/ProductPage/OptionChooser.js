$(document).ready(function ()
{
    let price = parseInt($("[data-baseprice]").data("baseprice"), 10);

    let AdjustPrice = 0;
    $(".OptionChangeButtonActive").each(function (index) {
        AdjustPrice = AdjustPrice + parseInt($(this).data("adjustment"), 10);
    })

    price = price + AdjustPrice;


    $("[data-baseprice]").text(price + "$");

    $('.Atribute').each(function (index) {

        $(this).find("[id$='Option[0]']").addClass("OptionChangeButtonActive");

        $(this).find(".OptionChangeButton").each(function (OptIndex)
        {
            $(this).on('click', function (event)
            {
                event.preventDefault();

                $(this).parents('.Atribute').find(".OptionChangeButtonActive").removeClass('OptionChangeButtonActive');
                $(this).parents('.Atribute').find("input[name='OptionId']").attr('value', $(this).data("optionid"));

                $(this).addClass("OptionChangeButtonActive");
                price = parseInt($("[data-baseprice]").data("baseprice"), 10);
                AdjustPrice = 0;
                $(".OptionChangeButtonActive").each(function (index) {
                    AdjustPrice = AdjustPrice + parseInt($(this).data("adjustment"), 10);
                })
                price = price + AdjustPrice;
                $("[data-baseprice]").text(price + "$");
                

            })

        })

    });
})