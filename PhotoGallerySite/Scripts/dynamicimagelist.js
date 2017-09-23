var imgCount = 0;
var prev = null;
function createImageInput() {
    var div = $("<div\>", {
        "class": "image-input"
    });
    var label = $("<label\>", {
        for: "imagetitle" + imgCount
    })
    label.text("Image Title");
    var title = $("<input\>", {
        type: "text",
        name: `Pictures[${imgCount}].PictureTitle`,
        id: "imagetitle" + imgCount
    })
    var image = $("<input\>", {
        type: "file",
        name: `Pictures[${imgCount}].Content`,
        id: "image" + imgCount,
    })
    var button = $("<img\>", {
        src: "/Images/delete.png",
        "class": "cross",
        "style": "display: none",
    })
    div.append([label, title, image, button])
    if (prev) {
        prev.css("display", "inline");
    }
    prev = button;
    imgCount++;
    return div;
}
function dynamicInput() {
    $("fieldset").on("click", "img", function () {
        var title = $(this).siblings("input[type = text]").attr("name");
        var file = $(this).siblings("input[type = file]").attr("name");
        var curr = $(this).parent().next("div");
        while (curr.prop("tagName") == "DIV") {
            var title2 = curr.children("input[type = text]").attr("name");
            var file2 = curr.children("input[type = file]").attr("name");
            curr.children("input[type = text]").attr("name", title);
            curr.children("input[type = file]").attr("name", file);
            title = title2;
            file = file2;
            curr = curr.next("div");
        }
        $(this).parent().remove();
        imgCount--;
    });
    $("#create").before(createImageInput);
    $("fieldset").on("change", "input[type=file]", function () {
        if ($(this).data('clicked')) {
            return false;
        }
        $("#create").before(createImageInput);
        $(this).data('clicked', true);
    });
}
$("#create").on("click", function () {
    var list = $("input[type=file]");
    var images = 0;
    var allowed = ["jpg", "png", "gif", "bmp", "jpeg"];
    for (var it = 0; it < list.length - 1; it++) {
        var ext = list[it].value.split(".").pop().toLowerCase();
        if ($.inArray(ext, allowed) > -1) {
            images++;
        }
    }
    if (images < 3) {
        var mesg = "* Album must have at least three pictures";
        $("div .error").html(mesg);
        return false;
    }
    else {
        $("div .error").html();
    }
});

dynamicInput();