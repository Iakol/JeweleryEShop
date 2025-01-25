var PostArr = [];
function SetCities(event)
{
    const input = event.target;

    const name = input.value;
    $.ajax({
        type: "GET",
        url: "/NovaPost/GetCityList",
        data: { city: name },
        dataType: 'JSON',
        success: function (data) {
            SetCitiesInInput(data);
        },
        error: function (xhr, status, error) {
            console.log("Error: " + error);
        }
    });

}

function SetCitiesInInput(cities)
{
    

    const transformedCities = cities.map(city => ({
        label: city.present,  // Для отображения в списке
        value: city.deliveryCity       // Значение для записи в поле
    }));

    $("#City").autocomplete({
        minLength: 2,
        source: transformedCities,
        select: function (event, ui) {
            $("#City").val(ui.item.label);
            $("#CityRef").val(ui.item.value);
            SetPostArray(ui.item.value);
            return false;
        },
    })
    $("#City").autocomplete("instance")._renderItem = function (ul, item) {
        ul.addClass("DropListElement");
        return $("<li>")
            .append("<div>" + item.label + "</div>")
            .appendTo(ul);
    };

    $("#City").autocomplete("enable")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<div>" + item.label + "</div>")
            .appendTo(ul);
    };


}

function SetPostArray(ref) {
    PostArr = [];

    $.ajax({
        type: "GET",
        url: "/NovaPost/GetPostList",
        data: { cityRef: ref },
        dataType: 'JSON',
        success: function (data) {
            PostArr = data.map(post => ({
                id: post.ref,
                text: post.description

            }));
            $("#PostRef").select2({
                placeholder: 'Select an Post',
                data: PostArr
            });

        },
        error: function (xhr, status, error) {
            console.log("Error: " + error);
        }
    });



}


$(document).ready(function () {
    $("#PostRef").select2({
        placeholder: 'Select an Post',
        data: PostArr
    });
});


