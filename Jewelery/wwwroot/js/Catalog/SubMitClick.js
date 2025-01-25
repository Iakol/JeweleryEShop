$(document).ready(function ()
{
    const SetFilterURL = $(".Fillters").data('filterurl');
    $("#OrderSelect").on("change", function () {
        const selectid = $(this).find("option:selected").data('selectid');
        const OrderSelectFilterDTO =
        {
            OrderString: selectid
        }
        const sendData =
        {
            additionalparameter: JSON.stringify(OrderSelectFilterDTO),
            fillerid:0
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
    });

    $("input[name='IsExist']").on("change", function (event)
    {
        const input = $(event.target);

        let isExist;
        const value = input.val();

        if (value === "IsExist") {
            isExist = true;
        }
        else if (value === "IsNotExist") {
            isExist = false;
        }
        else
        {
            isExist = null;
        }

        const sendData =
        {
            additionalparameter: JSON.stringify({ IsExist: isExist }),
            fillerid: 1
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


    })

    

})