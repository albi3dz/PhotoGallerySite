function createModel() {
    var modal = $(".modal");
    var span = $(".close");
    $("td .delete").on("click", function () {
        modal.css("display", "block");
        $("#yes").data("id", $(this).attr("id"));
        return false;
    });
    span.on("click", function () {
        modal.css("display", "none");
    });
    $("#no").on("click", function () {
        modal.css("display", "none");
    });
    $("#yes").on("click", function () {
        location.href = "MyAlbums/Delete/" + $(this).data("id");
    });
}
createModel();