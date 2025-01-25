
$(document).ready(function ()
{
    $("#FilterForm","SearchForm").on("submit", function (event)
    {    
        event.preventDefault()
        const categorySelect = [];
        $(".CategoryCheckBoxContainer").each(function ()
        {
            const SubCategorySelect = [];

            $(this).find(".SubCategoryCheckBox").each(function () {
                const SubcategoryFileter = {
                    SubCategoryId: parseInt($(this).val(), 10),
                    Selected: $(this).is(":checked") ? true : false
                }
                SubCategorySelect.push(SubcategoryFileter);
            });

            const categoryFillter =
            {
                CategoryId: parseInt($(this).data("categoryid"), 10),
                selected: (SubCategorySelect.filter(function (SubCategory)
                {
                    return SubCategory.Selected
                }).length != 0) || $(this).find(".CategoryCheckBox").is(":checked") ? true : false,
                SelectedAllSubCategory: $(this).find(".CategoryCheckBox").is(":checked") ? true : false,
                SelectedSubCategory: SubCategorySelect
            }

            categorySelect.push(categoryFillter);
        });


        const sendData =
        {
            SelectedCategory: categorySelect,
            CategoryFiltering: (($(".SubCategoryCheckBox").filter(function (SubCategory) {
                return $(this).is(":checked");
            }).length === 0) && ($(".CategoryCheckBox").filter(function (Category) {
                return $(this).is(":checked");
            }).length === 0)) ? false : true,
            isExist: ($("#isExist").is(":checked") && $("#isNotExist").is(":checked") || $("#isExist").is(":not(:checked)") && $("#isNotExist").is(":not(:checked)")) ? null : ($("#isExist").is(":checked") && $("#isNotExist").is(":not(:checked)")) ? true : ($("#isExist").is(":not(:checked)") && $("#isNotExist").is(":checked")) ? false : true,
            isDispay: ($("#isDisplay").is(":checked") && $("#isNotDisplay").is(":checked") || $("#isDisplay").is(":not(:checked)") && $("#isNotDisplay").is(":not(:checked)")) ? null : ($("#isDisplay").is(":checked") && $("#isNotDisplay").is(":not(:checked)")) ? true : ($("#isDisplay").is(":not(:checked)") && $("#isNotDisplay").is(":checked")) ? false : true,
            isPromotion: ($("#isPromotion").is(":checked") && $("#isNotPromotion").is(":checked") || $("#isPromotion").is(":not(:checked)") && $("#isNotPromotion").is(":not(:checked)")) ? null : ($("#isPromotion").is(":checked") && $("#isNotPromotion").is(":not(:checked)")) ? true : ($("#isPromotion").is(":not(:checked)") && $("#isNotPromotion").is(":checked")) ? false : true,
            SearchString: $("#Search").val()
        };

        console.log(sendData);


        $.ajax({
            type: "POST",
            url: $("#FilterForm").data("url"),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(sendData),
            success: function () {
                console.log("sus")
            }

        });

        location.reload();


    });

})