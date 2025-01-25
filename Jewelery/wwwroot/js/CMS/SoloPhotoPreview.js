function PreviewImage(event) {
    var input = event.target; 
    var reader = new FileReader();

    reader.onload = function () {
        var preview = document.querySelector("#Preview");
        preview.src = reader.result; 
        preview.style.display = "block";
    };

    if (input.files && input.files[0]) { 
        reader.readAsDataURL(input.files[0]); 
    }
}
