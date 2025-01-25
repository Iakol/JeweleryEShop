function CheckLangueage()
{
    if ($("#languageButtonUKR").hasClass("languageButtonSelected")) {
        $(".TextInputEng , .PhotoInputEng , .TitleEngContainer").attr("hidden", true);
        $(".TextInputUkr , .PhotoInputUkr , .TitleUkrContainer").attr("hidden", false);

    }
    else if ($("#languageButtonENG").hasClass("languageButtonSelected")) {
        $(".TextInputUkr , .PhotoInputUkr , .TitleUkrContainer").attr("hidden", true);
        $(".TextInputEng , .PhotoInputEng , .TitleEngContainer").attr("hidden", false);

    }
}

$(document).ready(function ()
{
    if ($("#languageButtonUKR").hasClass("languageButtonSelected")) {
        $(".TextInputEng , .PhotoInputEng , .TitleEngContainer").attr("hidden", true);
    }
    else if ($("#languageButtonENG").hasClass("languageButtonSelected"))
    {
        $(".TextInputUkr , .PhotoInputUkr , .TitleUkrContainer").attr("hidden", true);

    }

    $("#languageButtonUKR").on("click", function () {
        if (!$("#languageButtonUKR").hasClass("languageButtonSelected")) {
            $("#languageButtonUKR").addClass("languageButtonSelected");
            $("#languageButtonENG").removeClass("languageButtonSelected");
            CheckLangueage();
        }
    });

    $("#languageButtonENG").on("click", function () {
        if (!$("#languageButtonENG").hasClass("languageButtonSelected")) {
            $("#languageButtonENG").addClass("languageButtonSelected");
            $("#languageButtonUKR").removeClass("languageButtonSelected");
            CheckLangueage();
        }
    });

})