$(document).ready(function () {
    $('button[type="submit"]').on("click",function (event) {
        event.preventDefault();

        const data = new FormData();

        // Зібрати дані продукту
        data.append('Product_id', $('input[name$="Product_id"]').val());
        data.append('Name_UKR', $('input[name$="Name_UKR"]').val());
        data.append('Name_ENG', $('input[name$="Name_ENG"]').val());
        data.append('Description_UKR', $('textarea[name$="Description_UKR"]').val());
        data.append('Description_ENG', $('textarea[name$="Description_ENG"]').val());
        data.append('Price', $('input[name$="Price"]').val());
        data.append('Category_id', $('select[name$="Category_id"]').val());
        data.append('SubCategory_id', $('select[name$="SubCategory_id"]').val() ? $('select[name$="SubCategory_id"]').val() : null); //$('select[name$="SubCategory_id"]').val()
        data.append('Articul', $('input[name$="Articul"]').val());
        data.append('isExist', $('input[name$="isExist"]').is(':checked'));
        data.append('isDisplay', $('input[name$="isDisplay"]').is(':checked'));
        data.append('isPromotion', $('input[name$="isPromotion"]').is(':checked'));

        if ($('input[name$="isPromotion"]').is(':checked'))
        {
            data.append('Promotion_Price', $('input[name$="Promotion_Price"]').val());
        }
        else
        {
            data.append('Promotion_Price', '');
        }


        // Зібрати дані зображень

        let ImageCounter = 0;
        $('.Image_group').each(function (index) {
            ImageCounter++;
            const imageInput = $(this).find("input[type='file']")[0];
            
            data.append(`Images[${index}].ImageFile`, $(this).find("input[name$='Product_id']").val());
            if (imageInput.files[0]) {
                data.append(`Images[${index}].ImageFile`, imageInput.files[0]);

            } else
            {
                data.append(`Images[${index}].ImageFile`, null);
            }
            data.append(`Images[${index}].ImageFile`, imageInput.files[0]);
            data.append(`Images[${index}].Image_id`, $(this).find("input[name$='Image_id']").val());
            data.append(`Images[${index}].Alt_text`, $(this).find("input[name$='Alt_text']").val());
            
        });

        data.append(`ImageCounter`, ImageCounter);


        // Зібрати дані атрибутів

        let AtributeCounter = 0;

        $(".Atribute_group").each(function (index) {
            AtributeCounter++;
            let atribute_id = $(this).find("input[name$='Attributes[0].Atribute_id']").val();
            if (atribute_id === null || atribute_id === undefined || atribute_id === 0)
            {
                data.append(`Attributes[${index}].Atribute_id`, 0);
            }
            else
            {
                data.append(`Attributes[${index}].Atribute_id`, atribute_id);

            }
            data.append(`Attributes[${index}].Atribute_name_UKR`, $(this).find("input[name$='Atribute_name_UKR']").val());
            data.append(`Attributes[${index}].Atribute_name_ENG`, $(this).find("input[name$='Atribute_name_ENG']").val());
            data.append(`Attributes[${index}].Unit`, $(this).find("input[name$='Unit']").val());
            data.append(`Attributes[${index}].DetermineTheSize_Id`, $(this).find("select[name$='DetermineTheSize_Id']").val());
            let OptionCounter = 0;
            $(this).find(".Option_container .Option_group").each(function (OptionIndex) {
                OptionCounter++;
                const Option_id = $(this).find("input[name$='Option_id']").val();

                console.log(atribute_id);
                if (atribute_id === null || atribute_id === undefined || atribute_id === 0) {
                    data.append(`Attributes[${index}].Options[${OptionIndex}].Atribute_id`, 0);
                }
                else
                {
                    data.append(`Attributes[${index}].Options[${OptionIndex}].Atribute_id`, atribute_id);

                }
                if (Option_id === null || Option_id === undefined || Option_id === 0) {

                    data.append(`Attributes[${index}].Options[${OptionIndex}].Option_id`, 0);
                }
                else {

                    data.append(`Attributes[${index}].Options[${OptionIndex}].Option_id`, Option_id);

                }
                data.append(`Attributes[${index}].Options[${OptionIndex}].Size`, $(this).find("input[name$='Size']").val());
                data.append(`Attributes[${index}].Options[${OptionIndex}].PriceAdjustment`, $(this).find("input[name$='PriceAdjustment']").val());
            });
            data.append(`Attributes[${index}].Count`, OptionCounter);


            
        });

        data.append(`AttributesCount`, AtributeCounter);

        //data.forEach((value, key) => {
        //    console.log(`${key}: ${value}`);
        //});

        const actUrl = document.querySelector("#Head").dataset.url;


        $.ajax(
            {
                url: actUrl,
                type: 'POST',
                data: data,
                success: function (url)
                {
                    location.href = url;
                },
                error: function (xhr, status, error)
                {
                },
                cache: false,
                contentType: false,
                processData: false

            })

        
    });
});
