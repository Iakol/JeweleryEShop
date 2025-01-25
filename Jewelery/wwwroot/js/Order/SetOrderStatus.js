$(document).ready(function () {

    const status = parseInt($('#StatusSpan').data("status"),10);

    $('#StatusButtonContainer').find(`input[value="${status}"]`).attr("checked");

    $("#SetStatusButton").on('click', function ()
    {
        const checkedStatus = parseInt($('#StatusButtonContainer').find("input:checked").val(), 10);
        const order = parseInt($('#OrderId').data("orderid"), 10);
        const url = $("#ModalFooterContainer").data("url");

        $.ajax({
            url: url,
            type: 'GET',
            data: {
                orderId: order,
                status: checkedStatus          
            },
            sucsses: function () {
            },
            error: function (xhr, status, error) {
            }

        });

        document.location.reload();


    })

})