
$(document).ready(function () {
    $.ajax({
        url: '/Category/GetCategoryDropList',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            SetCategory(data);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching categories:', error);
        }
    })

});


function SetCategory(data)
{
    const CatagerySelect = document.querySelector("#Category_id");

    data.forEach(function (item)
    {
        const option = document.createElement("option");

        option.value = item.category_id;
        option.textContent = item.name;
        option.classList.add("SelectCategoryOption");
        CatagerySelect.appendChild(option);

    })
}

function GetSubCategory(event)
{
    const categoryId = event.target.value;
    $.ajax({
        url: '/Category/GetSubCategoryDropList',
        type: 'GET',
        data: { id: categoryId },
        dataType: 'json',
        success: function (data) {
            SetSubCategory(data);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching categories:', error);
        }
    });
}

function SetSubCategory(data) {

    const CatagerySelect = document.querySelector("#SubCategory_id");

    while (CatagerySelect.firstChild) {
        CatagerySelect.removeChild(CatagerySelect.lastChild);
    }
    const Firstoption = document.createElement("option");
    Firstoption.textContent = "Choose SubCategory";
    Firstoption.value = "";
    CatagerySelect.appendChild(Firstoption);
    data.forEach(function (item) {
        const option = document.createElement("option");

        option.value = item.category_id;
        option.textContent = item.name;
        option.classList.add("SelectCategoryOption");

        CatagerySelect.appendChild(option);

    })

}