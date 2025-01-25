
$(document).ready(function () {
    $.fn.SendDataToOrderSetFillterAction = function (data)
    {
        const url = $(".OrderSortStastus").data("url");

        $.ajax(
            {
                url: url,
                type: "POST",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                },
                error: function (xhr, status, error) {
                }
            })

        location.reload();

    }

    $(".StatusButton").on("click", function () {
        const data = [];
        $(this).toggleClass("StatusButtonSelected");

        if ($(".StatusButtonSelected").length != $(".StatusButton").length)
        {
            $(".StatusButtonSelected").each(function (item) {
                data.push(parseInt($(this).data("enumid"), 10));
            })
        }

        $.fn.SendDataToOrderSetFillterAction({ partOfFilter: JSON.stringify(data), typeOfFiltering: 0 });

    });

    $("#OrderSelect").on("change", function ()
    {
        const OrderSelect = $(this).find(":selected").val();
        $.fn.SendDataToOrderSetFillterAction({ partOfFilter: OrderSelect, typeOfFiltering: 1 });
    })

    $("#SearchSubmitButton").on("click", function () {
        const searchValue = $(this).parent().find("#Searchinput").val();
        const SearchType = [];
        $(this).parent().find(".SearchCheckBox").find("input").each(function ()
        {
            if ($(this).is(':checked'))
            {
                SearchType.push(parseInt($(this).val(),10));
            }
        })

        let stringData;

        if (searchValue) {
            stringData = JSON.stringify({ SearchValue: searchValue, SearchType: SearchType });

        }
        else {
            stringData = JSON.stringify({ SearchValue: searchValue, SearchType: [] });

        }

        $.fn.SendDataToOrderSetFillterAction({ partOfFilter: stringData, typeOfFiltering: 2 })


    });

});