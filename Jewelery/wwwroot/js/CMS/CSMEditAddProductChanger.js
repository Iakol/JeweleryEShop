
function UnitChanger(event,atributeId) {
    const CircleSpanList = document.querySelectorAll(`span[id^="CircleUnitSpanForAttributes[${atributeId}]"]`);
    const SizeSpanList = document.querySelectorAll(`span[id^="SpanForAttributes[${atributeId}]"]`);


    CircleSpanList.forEach((element) =>
    {
        element.textContent = event.target.value;

    });

    SizeSpanList.forEach((element) => {

        element.textContent = event.target.value;

    });
}

function SizeChangerForCircle(event,atributeId, optionId)
{
    const CircleSpan = document.querySelector(`span[ id="CircleValueSpanForAttributes[${atributeId}].Options[${optionId}]"]`);
    CircleSpan.textContent = event.target.value;
}