$(document).ready(function()
{

    $("#UserName, #UserEmail, #UserTelephone").on("click", function ()
    {
        if ($("#UserName").is(":checked") && $("#UserEmail").is(":checked") && $("#UserTelephone").is(":checked"))
        {

            $("#UserName").prop("checked", false);
            $("#UserEmail").prop("checked", false);
            $("#UserTelephone").prop("checked", false);

        }

    });
    

    $("#WithOutOrder").on("click", function ()
    {
        if ($("#WithOutOrder").is(':checked'))
        {
            if ($("#WithOrder").is(':checked'))
            {
                $("#WithUnCloseOrder").prop("checked", false);
                $("#WithOutUnCloseOrder").prop("checked", false);
                $("#WithOrder").prop("checked", false);
                $("#WithOutOrder").prop("checked", false);

                if ($("#WithUnCloseOrder").is(":disabled") || $("#WithOutUnCloseOrder").is(":disabled"))
                {
                    $("#WithUnCloseOrder").attr('disabled', false);
                    $("#WithOutUnCloseOrder").attr('disabled', false);
                }
            }
        }

        if ($(this).is(':checked'))
        {
            $("#WithUnCloseOrder").prop("checked", false);
            $("#WithOutUnCloseOrder").prop("checked", false);
            $("#WithUnCloseOrder").attr('disabled', true);
            $("#WithOutUnCloseOrder").attr('disabled', true);
        }
        else
        {
            $("#WithUnCloseOrder").attr('disabled', false);
            $("#WithOutUnCloseOrder").attr('disabled', false);
        }
    });

    $("#WithOrder").on("click", function () {

        if ($("#WithOrder").is(':checked')) {
            if ($("#WithOutOrder").is(':checked'))
            {
                $("#WithOrder").prop("checked", false);
                $("#WithOutOrder").prop("checked", false);
                $("#WithUnCloseOrder").prop("checked", false);
                $("#WithOutUnCloseOrder").prop("checked", false);
                if ($("#WithUnCloseOrder").is(":disabled") || $("#WithOutUnCloseOrder").is(":disabled"))
                {              
                    $("#WithUnCloseOrder").attr('disabled', false);
                    $("#WithOutUnCloseOrder").attr('disabled', false);
                }
            }
        }

        if ($(this).is(':checked')) {
            $("#WithUnCloseOrder").attr('disabled', false);
            $("#WithOutUnCloseOrder").attr('disabled', false);
        }
    });
    $("#WithUnCloseOrder,#WithOutUnCloseOrder").on("click", function ()
    {
        if ($(this).is(':checked')) {
            $("#WithOrder").prop("checked", true);
            $("#WithOutOrder").prop("checked", false);
        }
    });
    $("#WithUnCloseOrder").on("click", function ()
    {
        if ($(this).is(':checked')) {
            if ($("#WithOutUnCloseOrder").is(':checked'))
            {
                $("#WithUnCloseOrder").prop("checked", false);
                $("#WithOutUnCloseOrder").prop("checked", false);
                $("#WithOrder").prop("checked", false);

            }

            if ($(this).is(':checked') && $("#WithOrder").is(":not(:checked)"))
            {
                $("#WithOutOrder").prop("checked", false);
                $("#WithOrder").prop("checked", true);
            }
        }

    });
    $("#WithOutUnCloseOrder").on("click", function ()
    {
        if ($(this).is(':checked')) {
            if ($("#WithUnCloseOrder").is(':checked'))
            {
                $("#WithUnCloseOrder").prop("checked", false);
                $("#WithOutUnCloseOrder").prop("checked", false);
                $("#WithOrder").prop("checked", false);

            }

            if ($(this).is(':checked') && $("#WithOrder").is(":not(:checked)"))
            {
                $("#WithOutOrder").prop("checked", false);
                $("#WithOrder").prop("checked", true);
            }
        }
    });

    $("#WithItemInCart").on("click", function ()
    {
        if ($("#WithItemInCart").is(':checked'))
        {
            if ($("#WithOutOItemInCart").is(':checked'))
            {
                $("#WithItemInCart").prop("checked", false);
                $("#WithOutOItemInCart").prop("checked", false);
            }
        }
    });

    $("#WithOutOItemInCart").on("click", function ()
    {
        if ($("#WithOutOItemInCart").is(':checked'))
        {
            if ($("#WithItemInCart").is(':checked'))
            {
                $("#WithItemInCart").prop("checked", false);
                $("#WithOutOItemInCart").prop("checked", false);
            }
        }
    });

    $("#UserForm").on('submit', function (e)
    {
        e.preventDefault();

        let BoolSearch = $("#UserName").is(":checked") && $("#UserEmail").is(":checked") && $("#UserTelephone").is(":checked") || $("#UserName").is(":not(:checked)") && $("#UserEmail").is(":not(:checked)") && $("#UserTelephone").is(":not(:checked)");

        const SearchUSerFillterDTO =
        {
            SearchValue: $("#SearchInput").text() ? $("#SearchInput").text() : "",
            UserName: $("#UserName").is(":checked") || BoolSearch ? true : false,
            UserEmail: $("#UserEmail").is(":checked") || BoolSearch ? true : false,
            UserTelephone: $("#UserTelephone").is(":checked") || BoolSearch ? true : false
        }

        const UserFilter = {
            SearchFilter: SearchUSerFillterDTO,
            WithOrder: ($("#WithOrder").is(":checked") && $("#WithOutOrder").is(":checked")) || ($("#WithOrder").is(":not(:checked)") && $("#WithOutOrder").is(":not(:checked)")) ? null : $("#WithOrder").is(":checked") && $("#WithOutOrder").is(":not(:checked)")? true : false,
            WithUnCloseOrder: ($("#WithUnCloseOrder").is(":checked") && $("#WithOutUnCloseOrder").is(":checked")) || ($("#WithUnCloseOrder").is(":not(:checked)") && $("#WithOutUnCloseOrder").is(":not(:checked)")) ? null : $("#WithUnCloseOrder").is(":checked") && $("#WithOutUnCloseOrder").is(":not(:checked)") ? true : false,
            WithItemInCart: ($("#WithItemInCart").is(":checked") && $("#WithOutOItemInCart").is(":checked")) || ($("#WithItemInCart").is(":not(:checked)") && $("#WithOutOItemInCart").is(":not(:checked)")) ? null : $("#WithItemInCart").is(":checked") && $("#WithOutOItemInCart").is(":not(:checked)") ? true : false
        }

        $.ajax({
            type: "POST", 
            dataType: 'json', 
            url: $(this).data("url"),
            async: false,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(UserFilter),
            success: function (msg) {
                location.reload();
            }
        })



    });
});