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
    const CatagerySelectForSales = document.querySelector("#Sales");
    const CatagerySelectForSetificat = document.querySelector("#Sertificat");

    let SalesId = parseInt(CatagerySelectForSales.dataset.sales, 10);

    let sertificatId = parseInt(CatagerySelectForSetificat.dataset.sertificat, 10);

    const NulloptionForSales = document.createElement("option");
    const NulloptionForSertificat = document.createElement("option");
    NulloptionForSales.value = 0;
    NulloptionForSales.textContent = "Null";
    CatagerySelectForSales.appendChild(NulloptionForSales);


    NulloptionForSertificat.value = 0;;
    NulloptionForSertificat.textContent = "Null";
    CatagerySelectForSetificat.appendChild(NulloptionForSertificat);



    data.forEach(function (item) {
        const optionForSales = document.createElement("option");

        optionForSales.value = item.category_id;
        optionForSales.textContent = item.name;

        const optionForSertificat = document.createElement("option");

        optionForSertificat.value = item.category_id;
        optionForSertificat.textContent = item.name;

        if (item.category_id == SalesId) {
            optionForSales.setAttribute("selected");
            CatagerySelectForSales.appendChild(optionForSales);
        }
        else
        {
            CatagerySelectForSales.appendChild(optionForSales);
        }

        if (item.category_id == sertificatId) {
            optionForSertificat.setAttribute("selected");
            CatagerySelectForSetificat.appendChild(optionForSertificat);
        }
        else {
            CatagerySelectForSetificat.appendChild(optionForSertificat);
        }




    })
}