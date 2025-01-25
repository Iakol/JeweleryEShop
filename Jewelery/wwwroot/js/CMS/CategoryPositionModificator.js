$(document).ready(function () {
    var positionList = [];

    $('#sortable').sortable({
        update: function () {
            positionList = [];
            $(".itemOfList").each(function (index, element) {
                positionList.push({
                    CategoryId: $(element).data('id'),
                    Position: index + 1
                });
            });

            console.log(positionList); // Перевірте в консолі, чи правильно заповнюється positionList
        }
    });

    $('button[type="submit"]').click(function (event) {
        event.preventDefault(); // Запобігти стандартній відправці форми

        // Оновити positionList перед відправкою
        positionList = [];
        $(".itemOfList").each(function (index, element) {
            positionList.push({
                CategoryId: $(element).data('id'),
                Position: index + 1
            });
        });

        console.log(positionList); // Перевірте в консолі перед відправкою

        $.ajax({
            url: $(this).data("url"),
            type: 'POST',
            contentType: 'application/json', // Вказати тип контенту
            data: JSON.stringify(positionList), // Перетворити дані в JSON
            success: function () {
                console.log("Order updated successfully.");
            },
            error: function (xhr, status, error) {
                console.log("Error updating order: " + error);
                console.log(xhr.responseText); // Перевірте деталі помилки
            }
        });
    });
});
