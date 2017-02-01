$(document).ready(function () {
    $("#searchForm").submit(function (e) {
        e.preventDefault();
        var value = $("#searchQuery").val();

        if (value.length <= 0)
            showMessage("Пустой запрос", "error");
        else
            document.location = "/search/" + value;
    });
});