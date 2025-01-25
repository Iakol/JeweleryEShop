
$(document).ready(function ()
{
    $(".CategoryCheckBox").each(function ()
    {
        $(this).on("change", function ()
        {
            if ($(this).is(":checked")) {
                $(this).parent().find(".SubCategoryCheckBox").each(function () {
                    $(this).prop('checked', true);
                });
            }
            else
            {
                $(this).parent().find(".SubCategoryCheckBox").each(function () {
                    $(this).prop('checked', false);
                });
            }
        })
    })

    $(".SubCategoryCheckBox").each(function ()
    {
        $(this).on("change", function ()
        {
            if ($(this).parent().find(".SubCategoryCheckBox").filter(":not(:checked)").length == 0)
            {
                $(this).parent().parent().find(".CategoryCheckBox").prop('checked', true);
            }
            else
            {
                $(this).parent().parent().find(".CategoryCheckBox").prop('checked', false);
            }


        })

    })
})