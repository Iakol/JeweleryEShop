function CheckLangueage() {
    if ($("#languageButtonUKR").hasClass("languageButtonSelected")) {
        $(".TextInputEng , .PhotoInputEng , .TitleEngContainer").attr("hidden", true);
        $(".TextInputUkr , .PhotoInputUkr , .TitleUkrContainer").attr("hidden", false);

    }
    else if ($("#languageButtonENG").hasClass("languageButtonSelected")) {
        $(".TextInputUkr , .PhotoInputUkr , .TitleUkrContainer").attr("hidden", true);
        $(".TextInputEng , .PhotoInputEng , .TitleEngContainer").attr("hidden", false);

    }
}

$(document).ready(function () {
    $("#AddParagraphButton").on("click", function () {

        const paragraph = $(`
                <div class="DetermizeSizeObject" data-id="0">
                    <div class="IconContainer">
                        <i class="bi bi-list"></i>
                    </div>
                    <div class="TextInpuntsContainer">
                        <div hidden>
                            <input type="text" value="0" />
                        </div>
                        <div class="TextInputUkr TextInputObject">
                            <textarea class="TextAreaInputField" placeholder="Empty Paragraph"></textarea>
                        </div>
                        <div class="TextInputEng TextInputObject">
                            <textarea class="TextAreaInputField" placeholder="Empty Paragraph"></textarea>
                        </div>
                    </div>
                    <div>
                        <button class="DeletePageObject">X</button>
                    </div>
                </div>
        `);

        $(".DetermizeSizeContainer").append(paragraph);
        CheckLangueage();

    });

    $("#AddPhotoButton").on("click", function () {
        const photo = $(`
                <div class="DetermizeSizeObject">
                    <div class="IconContainer">
                        <i class="bi bi-list"></i>
                    </div>
                    <div class="PhotoInpuntsContainer">
                        <div hidden>
                            <input type="text" value="0" />
                        </div>
                        <div class="PhotoInputUkr PhotoInputObject">
                            <div class="PhotoInputUkrPrewView ImagePreviewContainer" hidden>
                                <img class="ImagePrevieTag" src="" alt="prewview" />
                            </div>
                            <div class="ImageInputContainer">
                                <input class="ImgagePhotoInput" accept="image/*" type="file" />
                                <div class="DeleteImageButtonContainer" hidden>
                                    <button class="DeleteImageFromInputButton" type="button">Delete image</button>
                                </div>
                            </div>
                            <div class="ReuseCheckBoxContainer" hidden>
                                <input class="ReusePhotoCheckBox" type="checkbox" />
                            </div>
                        </div>
                        <div class="PhotoInputEng PhotoInputObject">
                            <div class="PhotoInputEngPrewView ImagePreviewContainer" hidden>
                                <img class="ImagePrevieTag" src="" alt="prewview" />
                            </div>
                            <div class="ImageInputContainer">
                                <input class="ImgagePhotoInput" accept="image/*" type="file" />
                                <div class="DeleteImageButtonContainer" hidden>
                                    <button class="DeleteImageFromInputButton" type="button">Delete image</button>
                                </div>
                            </div>
                            <div class="ReuseCheckBoxContainer" hidden>
                                <input class="ReusePhotoCheckBox" type="checkbox" />
                            </div>
                        </div>
                    </div>
                    <div>
                        <button class="DeletePageObject">X</button>
                    </div>
                </div>
                `);

        $(".DetermizeSizeContainer").append(photo);
        CheckLangueage();
    });

    $(".DetermizeSizeContainer").on("click", ".DeletePageObject", function ()
    {
        $(this).parent().parent().remove();
    });


})