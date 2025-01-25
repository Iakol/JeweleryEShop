
function DetermineSizeSelect(indexOfAtribute)
{
    let Select;
    $.ajax({
        url: '/DetermineTheSize/getDetermibeSizeListComponent',
        data: { index: indexOfAtribute },
        async: false,
        success: function (html) {
            Select = $(html);
        }
    });
    
    return Select[0];
}



let AtributeCount = 0;
function AddAtribute()
{

    const container = document.querySelector("#Atribute_Container");

    var Atributeindex = parseInt(container.dataset.atributecount, 10);
    const index = Atributeindex;
    Atributeindex = Atributeindex + 1;

    container.dataset.atributecount = Atributeindex;


    const AtributeGroup = document.createElement('div');           //
                                                                   //
    AtributeGroup.className = "Atribute_group";                    //
    AtributeGroup.id = "Atribute_group_" + index;                  //

    const AtributeCred = document.createElement('div');          
    AtributeCred.classList.add("AtributeCred");   
    AtributeGroup.appendChild(AtributeCred);

    const NameOfAtributeContainer = document.createElement('div');
    NameOfAtributeContainer.classList.add("NameOfAtributeContainer");  
    
    const UnitAndDetermineSizeContainer = document.createElement('div');
    UnitAndDetermineSizeContainer.classList.add("UnitAndDetermineSizeContainer");   
    AtributeGroup.appendChild(NameOfAtributeContainer);
    AtributeGroup.appendChild(UnitAndDetermineSizeContainer);


    const AtributeNameContainerUKR = document.createElement('div');
    AtributeNameContainerUKR.classList.add("AtributeInputContainer");


    const AtributeNameLabelUKR = document.createElement('label');
    AtributeNameLabelUKR.htmlFor = "Attributes[" + index + "].Atribute_name_UKR" // додати локалізацію
    AtributeNameLabelUKR.textContent = "Назва атрибута на українській"
    AtributeNameLabelUKR.classList.add("LabelForInput")


    const AtributeNameInputUKR = document.createElement('input');
    AtributeNameInputUKR.type = "text"
    AtributeNameInputUKR.name = "Attributes[" + index + "].Atribute_name_UKR"
    AtributeNameInputUKR.id = "Attributes[" + index + "].Atribute_name_UKR"



    const AtributeNameContainerENG = document.createElement('div');
    AtributeNameContainerENG.classList.add("AtributeInputContainer");
    

    const AtributeNameLabelENG = document.createElement('label');
    AtributeNameLabelENG.htmlFor = "Attributes[" + index + "].Atribute_name_ENG" // додати локалізацію
    AtributeNameLabelENG.textContent = "Назва атрибута на англійській";
    AtributeNameLabelENG.classList.add("LabelForInput")


    
   

    const AtributeNameInputENG = document.createElement('input');
    AtributeNameInputENG.type = "text";
    AtributeNameInputENG.name = "Attributes[" + index + "].Atribute_name_ENG";
    AtributeNameInputENG.id = "Attributes[" + index + "].Atribute_name_ENG";

    const RequiredStar = document.createElement('span');
    RequiredStar.style.color = "red";
    RequiredStar.textContent = "*";


    AtributeNameLabelUKR.appendChild(RequiredStar.cloneNode(true));
    AtributeNameLabelENG.appendChild(RequiredStar.cloneNode(true));


    AtributeNameContainerUKR.appendChild(AtributeNameLabelUKR);
    AtributeNameContainerUKR.appendChild(AtributeNameInputUKR);

    AtributeNameContainerENG.appendChild(AtributeNameLabelENG);
    AtributeNameContainerENG.appendChild(AtributeNameInputENG);

    NameOfAtributeContainer.appendChild(AtributeNameContainerUKR);
    NameOfAtributeContainer.appendChild(AtributeNameContainerENG);

    


    const AtributeUnitContainer = document.createElement('div');
    AtributeUnitContainer.classList.add("AtributeInputContainer");




    const AtributeUnitLabel = document.createElement('label');
    AtributeUnitLabel.htmlFor = "Attributes[" + index + "].Unit" // додати локалізацію
    AtributeUnitLabel.textContent = "Одиниця вимірювання";
    AtributeUnitLabel.classList.add("LabelForInput")


    const AtributeUnitInput = document.createElement('input');
    AtributeUnitInput.type = "text";
    AtributeUnitInput.name = "Attributes[" + index + "].Unit";
    AtributeUnitInput.id = "Attributes[" + index + "].Unit";
    AtributeUnitInput.setAttribute('onchange', `UnitChanger(event,${index})`);


    AtributeUnitContainer.appendChild(AtributeUnitLabel);
    AtributeUnitContainer.appendChild(AtributeUnitInput);

    UnitAndDetermineSizeContainer.appendChild(AtributeUnitContainer);
    UnitAndDetermineSizeContainer.appendChild(DetermineSizeSelect(index));

    AtributeCred.appendChild(NameOfAtributeContainer);
    AtributeCred.appendChild(UnitAndDetermineSizeContainer);

    ///////////////////////////////////////////////////////////
    Option_containerAndAddButton = document.createElement('div');
    Option_containerAndAddButton.classList.add("Option_containerAndButton")

    const Option_container = document.createElement('div');

    Option_container.className = "Option_container";
    Option_container.id = "Option_container_for_atribute_" + index;
    Option_container.dataset.OptionCount = '0';

    //Добавить две Опции

    const AddOptionButtonContainer = document.createElement('div');

    const AddOptionButton = document.createElement('button');
    AddOptionButton.type = "button";
    AddOptionButton.innerHTML = "+ Додати опцію";
    AddOptionButton.classList.add("AddOptionButton");

    AddOptionButton.onclick = function ()
    {
        AddOption(index);
    }

    AddOptionButtonContainer.appendChild(AddOptionButton);


    const RemoveButtonContainer = document.createElement('div');

    const RemoveButton = document.createElement('button');
    RemoveButton.type = "button";
    RemoveButton.innerHTML = "<i class='bi bi-trash3-fill'></i> Видалити атрибут"; 
    RemoveButton.classList.add("RemoveAtributeButton");


    RemoveButton.onclick = function ()
    {
        RemoveAtribute(index);
    }



    container.appendChild(AtributeGroup);

    AtributeCred.appendChild(RemoveButtonContainer);

    AtributeGroup.appendChild(AtributeCred);
    //AtributeGroup.appendChild(Option_container);
    AtributeGroup.appendChild(Option_containerAndAddButton);

    Option_containerAndAddButton.appendChild(Option_container);

    Option_containerAndAddButton.appendChild(AddOptionButtonContainer);



    RemoveButtonContainer.appendChild(RemoveButton);

    AddOption(index);
    AddOption(index);
}

function RemoveAtribute(index)
{
    const AtributeToRemove = document.querySelector("#Atribute_group_" + index)
    if (AtributeToRemove) {

        AtributeToRemove.remove();
    }

}

function AddOption(index)
{
    
    const container = document.querySelector("#Option_container_for_atribute_" + index);

    var Optionindex = parseInt(container.dataset.OptionCount, 10);
    //container.dataset.OptionCount = Optionindex;
    Optionindex = Optionindex + 1;

    container.dataset.OptionCount = Optionindex;

    const OptionGroup = document.createElement("div");
    OptionGroup.className = "Option_group";
    OptionGroup.id = "Option_group_" + Optionindex;

    const OptionSizeContainer = document.createElement('div');
    OptionSizeContainer.innerHTML = `<div class="input-group mb-3">
        <span class="input-group-text">Розмір</span>        
        <input onchange="SizeChangerForCircle(event,${index},${Optionindex})"  id="Attributes[${index}].Options[${Optionindex}].Size" name="Attributes[${index}].Options[${Optionindex}].Size" type="text" class="form-control" aria-label="Dollar amount (with dot and two decimal places)">
        <span id="SpanForAttributes[${index}].Options[${Optionindex}].Size" class="input-group-text"></span>
    </div>`

    const OptionSizeLabel= document.createElement('label');
    OptionSizeLabel.htmlFor = "Attributes[" + index + "].Options[" + Optionindex + "].Size" // додати локалізацію
    OptionSizeLabel.textContent = "Size";
    OptionSizeLabel.classList.add("LabelForInput");

    const OptionSizeInput = document.createElement('input');
    OptionSizeInput.type = "text";
    OptionSizeInput.name = "Attributes[" + index + "].Options[" + Optionindex + "].Size";
    OptionSizeInput.id = "Attributes[" + index + "].Options[" + Optionindex + "].Size";

    const OptionPriceAdjustmentContainer = document.createElement('div');

    OptionPriceAdjustmentContainer.innerHTML = `<div class="input-group mb-3">
        <span class="input-group-text">+ До Ціни</span>        
        <input id="Attributes["${index}"].Options[" ${Optionindex}"].PriceAdjustment" name="Attributes["${index}"].Options[" ${Optionindex}"].PriceAdjustment" type="text" class="form-control" aria-label="Dollar amount (with dot and two decimal places)">
        <span class="input-group-text">₴</span>
    </div>`
  
    //const OptionPriceAdjustmentLabel = document.createElement('label');
    //OptionPriceAdjustmentLabel.htmlFor = "Attributes[" + index + "].Options[" + Optionindex + "].PriceAdjustment"; // додати локалізацію
    //OptionPriceAdjustmentLabel.textContent = "PriceAdjustment";
    //OptionPriceAdjustmentLabel.classList.add("LabelForInput")

    //const OptionPriceAdjustmentInput = document.createElement('input');
    //OptionPriceAdjustmentInput.type = "text";
    //OptionPriceAdjustmentInput.name = "Attributes[" + index + "].Options[" + Optionindex + "].PriceAdjustment";
    //OptionPriceAdjustmentInput.id = "Attributes[" + index + "].Options[" + Optionindex + "].PriceAdjustment";

    const RemoveButtonContainer = document.createElement('div');
    RemoveButtonContainer.classList.add("RemoveOptionButtonContainer");

    const RemoveButton = document.createElement('button');
    RemoveButton.innerHTML = "<i class='bi bi-trash3-fill'></i>"
    RemoveButton.classList.add("RemoveOptionButton");
    RemoveButton.type = "button";
    RemoveButton.onclick = function ()
    {
        RemoveOption(Optionindex);
    }
    const PreviewOptionCircleValue = document.createElement('span');
    PreviewOptionCircleValue.id = `CircleValueSpanForAttributes[${index}].Options[${Optionindex}]`;

    const PreviewOptionCircleUnit = document.createElement('span');
    PreviewOptionCircleUnit.id = `CircleUnitSpanForAttributes[${index}].Options[${Optionindex}]`;


    const PreviewOptionCircle = document.createElement('div');
    PreviewOptionCircle.classList.add("PreviewOptionCircle");

    const OptionCred = document.createElement('div');
    OptionCred.classList.add("OptionCred");



    container.appendChild(OptionGroup);
    
    OptionGroup.appendChild(PreviewOptionCircle);
    OptionGroup.appendChild(OptionCred);
    OptionGroup.appendChild(RemoveButtonContainer);

    PreviewOptionCircle.appendChild(PreviewOptionCircleValue);
    PreviewOptionCircle.appendChild(PreviewOptionCircleUnit);


    OptionCred.appendChild(OptionSizeContainer);
    OptionCred.appendChild(OptionPriceAdjustmentContainer);

    //OptionSizeContainer.appendChild(OptionSizeLabel);
    //OptionSizeContainer.appendChild(OptionSizeInput);

    //OptionPriceAdjustmentContainer.appendChild(OptionPriceAdjustmentLabel);
    //OptionPriceAdjustmentContainer.appendChild(OptionPriceAdjustmentInput);

    RemoveButtonContainer.appendChild(RemoveButton);
    
    const UnitInput = $(`input[id="Attributes[${index}].Unit"]`);
    UnitInput.trigger("change");
}

function RemoveOption(Optionindex)
{
    const OptionToremove = document.querySelector("#Option_group_" + Optionindex);
    if (OptionToremove)
    {
        OptionToremove.remove();
    }

}
