$(document).ready(function () {
    $("#UKR").on("click", function () {
        document.cookie = "Culture=uk"
        location.reload();

    })
    $("#ENG").on("click", function () {
        document.cookie = "Culture=en"
        location.reload();

    })

})