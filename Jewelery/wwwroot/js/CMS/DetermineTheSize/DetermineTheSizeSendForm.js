function SomeIsCheaked(element)
{ 
    return $(element).is(':checked');
}

$(document).ready(function () {

    $("#EditorForm").on("submit", function (event)
    {
        event.preventDefault();

        const paragrapgList = [];
        const PhoroList = [];

        $(".DetermizeSizeObject").each(function (index, element) {
            let objectType;

            if ($(this).find(".TextInpuntsContainer").length > 0) {
                objectType = true;
            }
            else if ($(this).find(".PhotoInpuntsContainer").length > 0) {
                objectType = false;
            }
            else
            {
                console.log("Un recognize type")
            }

            if (objectType == true) {

                const paragraph = {
                    ObjectId: $(this).find(".ObjectIdInput").val(),//recode
                    placeInOrder: index,
                    textUKR: $(this).find(".TextInputUkr").find(".TextAreaInputField").val(),
                    textENG: $(this).find(".TextInputEng").find(".TextAreaInputField").val()
                }
                paragrapgList.push(paragraph);

            }
            else if (objectType == false) {
                const Photo = {
                    ObjectId: $(this).find(".ObjectIdInput").val(),
                    placeInOrder: index,
                    imageUKR: $(this).find(".PhotoInputUkr").find(".ImgagePhotoInput").prop('files')[0] ? $(this).find(".PhotoInputUkr").find(".ImgagePhotoInput").prop('files')[0] : null,//recode
                    imageENG: $(this).find(".PhotoInputEng").find(".ImgagePhotoInput").prop('files')[0] ? $(this).find(".PhotoInputEng").find(".ImgagePhotoInput").prop('files')[0] : null,//recode
                    BothSame: ($(this).find(".PhotoInputUkr").find(".ReusePhotoCheckBox").is(':checked') || $(this).find(".PhotoInputEng").find(".ReusePhotoCheckBox").is(':checked')) ? true : false,
                    //ChangeUKR: $(this).find(".PhotoInputUkr").find(".ObjectIdInput").val() != 0 ? $(this).find(".PhotoInputUkr").find(".InputChangeDeleteForOld").toArray().some(SomeIsCheaked) ? $(this).find(".PhotoInputUkr").find(".InputChangeDeleteForOldDelete").is(":checked") ? "Delete" : "Change" : null : null,
                   // ChangeENG: $(this).find(".PhotoInputEng").find(".ObjectIdInput").val() != 0 ? $(this).find(".PhotoInputEng").find(".InputChangeDeleteForOld").toArray().some(SomeIsCheaked) ? $(this).find(".PhotoInputEng").find(".InputChangeDeleteForOldDelete").is(":checked") ? "Delete" : "Change" : null : null,
                    ChangeUKR: $(this).find(".PhotoInputUkr").find(".ObjectIdInput").val() != 0 ? $(this).find(".PhotoInputUkr").find(".InputChangeDeleteForOldDelete").is(":checked") ? "Delete" : $(this).find(".PhotoInputUkr").find(".InputChangeDeleteForOldChange").is(":checked") ? "Change" : null : null,
                    ChangeENG: $(this).find(".PhotoInputEng").find(".ObjectIdInput").val() != 0 ? $(this).find(".PhotoInputEng").find(".InputChangeDeleteForOldDelete").is(":checked") ? "Delete" : $(this).find(".PhotoInputEng").find(".InputChangeDeleteForOldChange").is(":checked") ? "Change" : null : null


                }

                PhoroList.push(Photo);

            }


        });

        const AllData = {
            id: parseInt( $("#DetermizeSizeId").val(),10),
            Name: $("#DetermizeSizeName").val(),
            TittleUkr: $(".TitleInputUkr").val(),
            TittleEng: $(".TitleInputEng").val(),
            Text: paragrapgList,
            Photos: PhoroList
        }


        console.log(AllData);

        const formData = new FormData();
        formData.append("id", AllData.id);
        formData.append("Name", AllData.Name);
        formData.append("TittleUkr", AllData.TittleUkr);
        formData.append("TittleEng", AllData.TittleEng);

        AllData.Text.forEach(function (element,index) {
            formData.append(`Text[${index}].ObjectId`, element.ObjectId);
            formData.append(`Text[${index}].placeInOrder`, element.placeInOrder);
            formData.append(`Text[${index}].textUKR`, element.textUKR);
            formData.append(`Text[${index}].textENG`, element.textENG);
        });

        AllData.Photos.forEach(function (element, index) {
            formData.append(`Photos[${index}].ObjectId`, element.ObjectId);
            formData.append(`Photos[${index}].placeInOrder`, element.placeInOrder);
            formData.append(`Photos[${index}].imageUKR`, element.imageUKR);
            formData.append(`Photos[${index}].imageENG`, element.imageENG);
            formData.append(`Photos[${index}].BothSame`, element.BothSame);
            if (element.ChangeUKR)
            {
                formData.append(`Photos[${index}].ChangeUKR`, element.ChangeUKR);
            }
            if (element.ChangeENG) {
                formData.append(`Photos[${index}].ChangeENG`, element.ChangeENG);
            }
        });


        formData.forEach((value, key) => {
            console.log("Key: " + key + " " + "value: " + value);
        });

        const redirectUrl = $("#EditorForm").data("redirecturl");

        $.ajax(
        {
                url: $("#EditorForm").data("url"),
                type: 'POST',
                data: formData,
                success: function () {
                    window.location.href = redirectUrl;
                },
                error: function (xhr, status, error) {
                },
                cache: false,
                contentType: false,
                processData: false

        })
    })


});