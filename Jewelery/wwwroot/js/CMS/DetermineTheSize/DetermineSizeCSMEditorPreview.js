$(document).ready(function () {


    $("#PreviewButton").on("click", function () {

        $("#ContentContainer").empty();
        const PreviewLanguage = $(".languageButtonSelected").is("#languageButtonUKR") ? "UKR" : "ENG";

        const objectList = [];


        $(".DetermizeSizeObject").each(function () {
            if ($(this).find(".PhotoInpuntsContainer").length > 0) {

                if (PreviewLanguage === "UKR") {
                    objectList.push({
                        type: true,
                        value: $(this).find(".PhotoInputUkr").find(".ImagePrevieTag").prop('src')

                    })
                }
                else if (PreviewLanguage === "ENG")
                {
                    objectList.push({
                        type: true,
                        value: $(this).find(".PhotoInputEng").find(".ImagePrevieTag").prop('src')

                    })
                }
            }
            else if ($(this).find(".TextInpuntsContainer").length > 0) {
                if (PreviewLanguage === "UKR") {
                    objectList.push({
                        type: false,
                        value: $(this).find(".TextInputUkr").find(".TextAreaInputField").val()

                    })
                }
                else if (PreviewLanguage === "ENG") {
                    objectList.push({
                        type: false,
                        value: $(this).find(".TextInputEng").find(".TextAreaInputField").val()

                    })
                }
            }

        });

        let Tittle = "";

        if (PreviewLanguage === "UKR") {
            Tittle = $(".TitleInputUkr").val();

        }
        else if (PreviewLanguage === "ENG") {
            Tittle = $(".TitleInputEng").val();

        }

        $("#ContentContainer").append(`<h3>${Tittle}<h3>`)

        objectList.forEach(function (element) {
            if (element.type) {
                $("#ContentContainer").append(`<div class="PreviewPhotoContainer" ><img src="${element.value}" /></div>`)

            }
            else {
                $("#ContentContainer").append(`<p > ${element.value}</p>`)
            }

        });


    });

    

});