$(document).ready(function ()
{
    $(".DetermizeSizeContainer").on("change", ".ImgagePhotoInput", function (event)
    {
        if (event.target.files[0])
        {
            const url = URL.createObjectURL(event.target.files[0]);

            const PhotocontainerObject = $(this).parent().parent().parent();
            const PhotocontainerLangueage = $(this).parent().parent();

            //Set preview
            $(PhotocontainerLangueage).find(".ImagePreviewContainer").find(".ImagePrevieTag").prop('src', url);

            //Set UnHidden
            $(PhotocontainerLangueage).find(".ImagePreviewContainer").attr("hidden", false);

            //SetUnhidden ReuseCheckbox AnotherLanguage
            if ($(PhotocontainerLangueage).hasClass("PhotoInputUkr")) {
                const predicat = PhotocontainerObject.find(".PhotoInputEng").find(".ImagePreviewContainer").find(".ImagePrevieTag").attr("src") == "";
                if (predicat)
                {
                    PhotocontainerObject.find(".PhotoInputEng").find(".ReuseCheckBoxContainer").attr("hidden", false);
                }
            }
            else if ($(PhotocontainerLangueage).hasClass("PhotoInputEng"))
            {
                const predicat = PhotocontainerObject.find(".PhotoInputUkr").find(".ImagePreviewContainer").find(".ImagePrevieTag").attr("src") == '';
                if (predicat) {
                    PhotocontainerObject.find(".PhotoInputUkr").find(".ReuseCheckBoxContainer").attr("hidden", false);
                }
            }

            //Set Unhidden Delete Image Photo
            $(this).parent().find(".DeleteImageButtonContainer").attr("hidden", false);

            //Set ReuseCheackBoxAsHidden
            $(PhotocontainerLangueage).find(".ReuseCheckBoxContainer").attr("hidden", true);
            $(PhotocontainerLangueage).find(".ReusePhotoCheckBox").prop("checked", false);

            //Set CnageIfOldObject
            if ($(this).parent().find(".OldPhotoChangerInput").length > 0)
            {
                $(this).parent().find(".InputChangeDeleteForOld").prop('checked', false);
                $(this).parent().find(".InputChangeChangeForOld").prop('checked', false);
                $(this).parent().find(".InputChangeChangeForOld").prop('checked', true);
            }


        }

    });

    $(".DetermizeSizeContainer").on("click", ".DeleteImageFromInputButton", function () {
        $(this).parent().attr("hidden", true);
        $(this).parent().parent().find(".ImgagePhotoInput").val("");

        const PhotoContainerObject = $(this).parent().parent().parent().parent();
        const PhotoContainerLanguage = $(this).parent().parent().parent();

        $(PhotoContainerLanguage).find(".ImagePrevieTag").attr("src", "");

        //Set CnageIfOldObject
        if ($(this).parent().parent().find(".OldPhotoChangerInput").length > 0) {
            $(this).parent().parent().find(".InputChangeDeleteForOld").prop('checked', false);
            $(this).parent().parent().find(".InputChangeChangeForOld").prop('checked', false);
            $(this).parent().parent().find(".InputChangeDeleteForOld").prop('checked', true);
            console.log($(this).parent().parent().find(".InputChangeDeleteForOld").val());
        }

        $(PhotoContainerLanguage).find(".ImagePreviewContainer").attr("hidden", true);


        let anotherLanguageHavePhoto;

        if ($(PhotoContainerLanguage).hasClass("PhotoInputUkr"))
        {
            anotherLanguageHavePhoto = $(PhotoContainerObject).find(".PhotoInputEng").find(".ImagePrevieTag").attr("src") != '';

            if (anotherLanguageHavePhoto == true) {
                $(PhotoContainerLanguage).find(".ReuseCheckBoxContainer").attr("hidden", false);
            }
            else if (anotherLanguageHavePhoto == false) {
                $(PhotoContainerLanguage).find(".ReuseCheckBoxContainer").attr("hidden", true);
                $(PhotoContainerLanguage).find(".ReusePhotoCheckBox").prop("selected", false);
                $(PhotoContainerObject).find(".PhotoInputEng").find(".ReuseCheckBoxContainer").attr("hidden", true);
                $(PhotoContainerObject).find(".PhotoInputEng").find(".ReusePhotoCheckBox").prop("checked", false);

            }

        }
        else if ($(PhotoContainerLanguage).hasClass("PhotoInputEng"))
        {
            anotherLanguageHavePhoto = PhotoContainerObject.find(".PhotoInputUkr").find(".ImagePrevieTag").attr("src") != '';

            if (anotherLanguageHavePhoto == true) {
                $(PhotoContainerLanguage).find(".ReuseCheckBoxContainer").attr("hidden", false);
            }
            else if (anotherLanguageHavePhoto == false) {
                $(PhotoContainerLanguage).find(".ReuseCheckBoxContainer").attr("hidden", true);
                $(PhotoContainerLanguage).find(".ReusePhotoCheckBox").prop("selected", false);
                $(PhotoContainerObject).find(".PhotoInputUkr").find(".ReuseCheckBoxContainer").attr("hidden", true);
                $(PhotoContainerObject).find(".PhotoInputUkr").find(".ReusePhotoCheckBox").prop("checked", false);

            }
        }





    });

})