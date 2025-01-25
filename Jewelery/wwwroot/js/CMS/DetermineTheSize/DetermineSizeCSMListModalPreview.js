
function CheckLanguage()
{
    if ($(".ChangeLanguageButtonUKR").hasClass("ChangeLanguageButtonSelected")) {
        $(".DetermineSizePageContentUKR").each(function () {
            $(this).attr("hidden", false)

        });
        $(".DetermineSizePageContentENG").each(function () {
            $(this).attr("hidden", true)

        })
    }
    else if ($(".ChangeLanguageButtonENG").hasClass("ChangeLanguageButtonSelected")) {
        $(".DetermineSizePageContentENG").each(function () {
            $(this).attr("hidden", false)

        });
        $(".DetermineSizePageContentUKR").each(function () {
            $(this).attr("hidden", true)

        })
    }
}

$(document).ready(function ()
{
    console.log("Work");

    CheckLanguage();
    console.log("Work");

    
    $(".ModalContainer").on("click",".ChangeLanguageButton", function () {
        console.log("Work");

        if (!$(this).hasClass("ChangeLanguageButtonSelected") && $(this).hasClass("ChangeLanguageButtonUKR"))
        {
            $(".ChangeLanguageButtonUKR, .ChangeLanguageButtonENG").each(function ()
            {
                console.log("Work");
                $(this).toggleClass("ChangeLanguageButtonSelected");
            })
        }
        else if (!$(this).hasClass("ChangeLanguageButtonSelected") && $(this).hasClass("ChangeLanguageButtonENG")) {
            $(".ChangeLanguageButtonUKR, .ChangeLanguageButtonENG").each(function () {
                console.log("Work");

                $(this).toggleClass("ChangeLanguageButtonSelected");
            })
        }

        CheckLanguage();
        
    });

})