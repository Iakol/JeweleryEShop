var table;
$(document).ready(function () {
    table = $('<table>').addClass('display').addClass('CMSTable').appendTo('.category-table-container').DataTable({
        "ajax": {
            "url": '/Category/GetAll',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "category_id", "title": "Category ID" },
            { "data": "name", "title": "Name" },
            { "data": "description", "title": "Description" },
            {
                "data": "image",
                "title": "Image"
                //@,
                //    "render": function (data, type, row) {
                //        return '<img src="' + data + '" alt="Category Image" />';
                //    }*@
                },
    {
        "data": null,
        "title": "Actions",
        "render": function (data, type, row) {
            return '<button class="edit-btn" data-category_id="' + data.category_id + '">Edit</button>' +
                '<button class="delete-btn" data-category_id="' + data.category_id + '">Delete</button>';
        }
    }
            ]
        });

$('.category-table-container').on('click', '.edit-btn', function () {
    var id = $(this).data('category_id');
    var url = '/Category/UpsertAutoGenericGetViewCategoryCMS?id=' + id + '';
    window.location.href = url;
});

$('.category-table-container').on('click', '.delete-btn', function () {
    var id = $(this).data('category_id');
    var url = '"/Category/DeleteDinamicCategoryCMS?id=' + id + '';
    $.ajax({
        url: url,
        type: 'POST',
        data: { id: id },
        success: function () {
            // Видалити рядок таблиці або оновити таблицю
            alert('Category deleted successfully');
            table.ajax.reload();
        },
        error: function (xhr, status, error) {
            // Обробка помилки
            console.error('Error deleting category:', error);
        }
    });
});
        });

//$('.category-table-container').on('click', 'tr', function () {
//    var data = table.row(this).data();
//    console.log(data);
//    if (data) {
//        var url = '@Url.Action("SubCategoryCMS", "SubCategory")?id=' + 0;
//        window.location.href = url;
//    }
//});

$('#Create').on('click', function () {
    var url = '/Category/UpsertAutoGenericGetViewCategoryCMS';
    window.location.href = url;
})