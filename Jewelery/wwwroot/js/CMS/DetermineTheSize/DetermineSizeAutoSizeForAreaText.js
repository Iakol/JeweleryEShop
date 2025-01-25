function AutoHeightForAreaText(areaText)
{
    $(areaText).css('height', 'auto');
    $(areaText).css('height', areaText.scrollHeight + 'px');
}
function SetHint(areaText)
{
    const LanguageContainer = $(areaText).parent();
    const TextInputContainer = $(LanguageContainer).parent();

    if ($(areaText).val()) {
        if ($(LanguageContainer).hasClass("TextInputUkr")) {
            $(TextInputContainer).find(".TextInputEng").find(".TextAreaInputField").attr("placeholder", $(areaText).val());

        }
        else if ($(LanguageContainer).hasClass("TextInputEng")) {
            $(TextInputContainer).find(".TextInputUkr").find(".TextAreaInputField").attr("placeholder", $(areaText).val());
        }
    }
    else
    {
        if ($(LanguageContainer).hasClass("TextInputUkr")) {
            $(TextInputContainer).find(".TextInputEng").find(".TextAreaInputField").attr("placeholder", "Empty Paragraph");

        }
        else if ($(LanguageContainer).hasClass("TextInputEng")) {
            $(TextInputContainer).find(".TextInputUkr").find(".TextAreaInputField").attr("placeholder", "Empty Paragraph");
        }
    }

}

function SetHintTittle(tittle)
{
    const LanguageContainer = $(tittle).parent();
    const TextInputContainer = $(LanguageContainer).parent();

    if ($(tittle).val()) {
        if ($(LanguageContainer).hasClass("TitleUkrContainer")) {
            $(TextInputContainer).find(".TitleEngContainer").find(".TitleInput").attr("placeholder", $(tittle).val());

        }
        else if ($(LanguageContainer).hasClass("TitleEngContainer")) {
            $(TextInputContainer).find(".TitleUkrContainer").find(".TitleInput").attr("placeholder", $(tittle).val());
        }
    }
    else {
        if ($(LanguageContainer).hasClass("TitleUkrContainer")) {
            $(TextInputContainer).find(".TitleEngContainer").find(".TitleInput").attr("placeholder", "Tittle on English");

        }
        else if ($(LanguageContainer).hasClass("TitleEngContainer")) {
            $(TextInputContainer).find(".TitleUkrContainer").find(".TitleInput").attr("placeholder", "Tittle on Ukrainian");
        }
    }
}

$(document).ready(function () {

    $(".DetermizeSizeContainer").on('input',"textarea", function () {
        AutoHeightForAreaText(this); 
    });
    $(".DetermizeSizeContainer").on('change', "textarea", function () {
        SetHint(this);
    });
    $(".TitleInput").on('change', function () {
        SetHintTittle(this);
    });
});