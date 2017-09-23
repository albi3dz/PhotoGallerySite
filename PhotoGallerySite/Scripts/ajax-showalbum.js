var currId;
function updateImage(newId) {
    if (newId) {
        if ($("#preview-box img").filter(`#${currId}`).length != 0) {
            $("#preview-box img").filter(`#${currId}`).removeClass("curr");
        }
        currId = newId;
    }
    $("#preview-box img").filter(`#${currId}`).addClass("curr");
}
function enableEvents() {
    currId = $("#preview-box img").eq(0).attr("Id");
    updateImage();
    $("body").on("click", ".ajax-btn", function () {
        btn = $(this);
        var back = false;
        if (btn.attr("id") == "left-btn" || btn.attr("id") == "prev-btn") {
            back = true;
        }
        var picId = $("#preview-box img").eq(1).attr("Id");
        var tab = $(".show-table");
        if ($.contains(tab[0], btn[0]))
            picId = currId;
        path = window.location.href.split("/");
        $.ajax({
            url: "/Albums/Index",
            type: "GET",
            data: {
                Id: path[path.length - 1],
                PicId: picId,
                backward: back
            },
            dataType: "html",
            success: function (data) {
                $(".preview-table").html(" ");
                $(".preview-table").html(data);
                var tab = $(".show-table");
                if ($.contains(tab[0], btn[0])) {
                    if ($("#preview-box img").eq(1).attr("id") == currId) {
                        if (btn.attr("id") == "prev-btn") {
                            $("#current-image").attr("src", $("#preview-box img").eq(0).attr("src"));
                            $(".show-table caption").text($("#preview-box img").eq(0).attr("alt"));
                            updateImage($("#preview-box img").eq(0).attr("id"));
                        }
                        else {
                            $("#current-image").attr("src", $("#preview-box img").eq(2).attr("src"));
                            $(".show-table caption").text($("#preview-box img").eq(2).attr("alt"));
                            updateImage($("#preview-box img").eq(2).attr("id"));
                        }
                    }
                    else if ($("#preview-box img").eq(0).attr("id") == currId && btn.attr("id") == "prev-btn") {
                        updateImage();
                    }
                    else if ($("#preview-box img").eq(2).attr("id") == currId && btn.attr("id") == "next-btn") {
                        updateImage();
                    }
                    else {
                        newImg = $("#preview-box img").eq(1);
                        updateImage($(newImg).attr("id"));
                        $("#current-image").attr("src", newImg.attr("src"));
                        $(".show-table caption").text(newImg.attr("alt"));
                    }
                }
                else
                    updateImage();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("body").html("");
                $("body").html(jqXHR.responseText);
            }
        });
    });
    $("body").on("click", ".preview-image", function () {
        updateImage($(this).attr("id"));
        $("#current-image").attr("src", $(this).attr("src"));
        $(".show-table caption").text($(this).attr("alt"));
    });
}

enableEvents();