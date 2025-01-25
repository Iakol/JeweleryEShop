$(document).ready(function ()
{

    if ($('input[name$="isPromotion"]').is(':checked'))
    {
        $("#PromotionChangerInputContainer").attr("hidden", false)
    }
    else
    {
        $("#PromotionChangerInputContainer").attr("hidden", true)

    }

    $('input[name$="isPromotion"]').on('change', function () {
        if ($('input[name$="isPromotion"]').is(':checked')) {
            $("#PromotionChangerInputContainer").attr("hidden", false)
        }
        else {
            $("#PromotionChangerInputContainer").attr("hidden", true)

        }
    })

    $("#PromotionChangerInput").on("change", function ()
    {
        const Precent = $("#PrecentPromotionChanger").is(":checked");
        const price = $("#Price").val();
        const val = $(this).val();
        if (val && Precent) {
            const promotion = (price / 100) * (100 - val);
            $("#Promotion_Price").val(promotion);
        }
        else if (val && !Precent){
            const promotion = price - val;
            $("#Promotion_Price").val(promotion);
        }
        

    })
        

})