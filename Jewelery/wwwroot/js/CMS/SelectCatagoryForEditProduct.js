
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


function SetCategory(data) {
    const CatagerySelect = document.querySelector("#Category_id"); 
    const ProductCategory = parseInt(document.querySelector("#Category_id").dataset.category_id,10); 


    data.forEach(function (item) {
        const option = document.createElement("option");       
        option.value = item.category_id;
        option.textContent = item.name;
        if (item.category_id == ProductCategory) {
            option.setAttribute('selected', true);


        }
        CatagerySelect.appendChild(option);

        

    })

    if ($("#Category_id").find("option[selected]")) {
        $("#Category_id").trigger('change');

    }
}

function GetSubCategory(event) {
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
    const ProductCategory = parseInt(document.querySelector("#SubCategory_id").dataset.subcategory_id, 10); 

    data.forEach(function (item) {
        const option = document.createElement("option");
        if (item.category_id == ProductCategory) {
            option.setAttribute('selected', true);


        }
        option.value = item.category_id;
        option.textContent = item.name;
        CatagerySelect.appendChild(option);

    })

}