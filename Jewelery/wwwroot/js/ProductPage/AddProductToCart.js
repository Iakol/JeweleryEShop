$(document).ready(function ()
{
    $('#BuyButton').on('click', function (event) {
        event.preventDefault();

        var Attributes = [];

        $(".Atribute").each(function (index) {

            let Attribute_id = parseInt($(this).data('atributeid'), 10);

            let Option_id = parseInt($(this).find('.OptionId').attr('value'), 10);


            let Atribute = {
                Atribute_id: Attribute_id,
                Options: [{ Option_id: Option_id }]
            }

            Attributes.push(Atribute);
        });

        console.log(Attributes);

        product_id = parseInt($('#ProductId').attr('value'), 10);

        product = {
            product_id: product_id,
            Attributes: Attributes
        }

        const url = $('.BuyButtonContainer').data('url');

        $.ajax({
            url: "/Shop/Cart/AddProductToCart",
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(product),
            success: function (response) {
                console.log("sus");
            },
            error: function (xhr, status, error) {
                console.log("error");
            }
        })
    });




})