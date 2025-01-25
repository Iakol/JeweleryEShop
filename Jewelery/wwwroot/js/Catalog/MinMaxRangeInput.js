$(document).ready(function ()
{
    let MinPrice = parseInt($('#PriceRange').data("min"),10);
    let MaxPrice = parseInt($('#PriceRange').data("max"), 10) + 100;



    $("#slider-range").slider(
        {
            range: true,
            min: MinPrice,
            max: MaxPrice,
            values: [MinPrice, MaxPrice],
            slide: function (event, ui) {
                $("#amount").val(ui.values[0] + " = " + ui.values[1]);
                $("MinVal").val(ui.values[0]);
                $("MaxVal").val(ui.values[1]);
            },
            stop: function (event, ui)
            {
                const PriceRangeFilter = {
                    SelectMaxPrice : ui.values[0],
                    SelectMinPrice : ui.values[1]
                }
                const sendData = {
                    additionalparameter: JSON.stringify(PriceRangeFilter),
                    fillerid: 2
                }

                $.ajax({
                    type: 'POST',
                    url: SetFilterURL,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(sendData),
                    success: function () {
                    }
                })
                location.reload();
            }
        });

    $("#amount").val($("#slider-range").slider("values", 0) + " - " + $("#slider-range").slider("values",1))
})