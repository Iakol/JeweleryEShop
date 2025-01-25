
function GetCityFromApi(CityName)
{
    var CityList = [];

    $.ajax(
        {
            url: "",
            type: "GET",
            dataType: "JSON",
            data: { SearchWord: CityName },
            success: function (response) {
                // Обработка ответа JSON
                CityList = response
            },
            error: function (xhr, status, error) {
                // Функция, которая выполняется в случае ошибки
                $('#result').html('Произошла ошибка: ' + error);
            }
        })

    $("#ListOfCity").empty();

    const container = document.querySelector("#ListOfCity");
    CityList.forEach(function (element)
    {
        option = document.createElement("option");
        option.value = element.name;
        option.dataset.id = element.id;
    })
}

SearchCity(event)
{
    input = event.target;

    SearchWord = input.value;

    GetCityFromApi(SearchWord);

}