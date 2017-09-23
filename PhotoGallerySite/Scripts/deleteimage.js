var imgCount = $("img").length;
function createModel() {
    var modal = $(".modal");
    var span = $(".close");
    $("body").on("click", ".delete", function () {
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
        var idToDel = $(this).data("id");
        var ToDel = $(".delete").filter(`[id=\"${idToDel}\"]`).parents("tr");
        var curr = ToDel;
        var i = idToDel;
        var curr = curr.next("TR");
        while (curr.prop("tagName") == "TR") {
            curr.find("a").attr("id", i);
            curr.find("input[type = text]").attr("name", `Pictures[${i}].PictureTitle`);
            if (curr.find("input[type=file]").length != 0)
                curr.find("input[type = file]").attr("name", `Pictures[${i}].Content`);
            else
                curr.find("input[type = hidden]").attr("name", `Pictures[${i}].PictureId`);
            curr = curr.next("tr");
            i++;
        }
        ToDel.remove();
        modal.css("display", "none");
        imgCount--;
    });
}
function newImage() {
    $("body").on("change", "input[type=file]", function () {
        input = $(this);
        input.attr("name", `Pictures[${imgCount}].Content`);
        input.css("display", "none");
        var title = $("<input/>", {
            "name": `Pictures[${imgCount}].PictureTitle`,
            "type": "text"
        })
        var img = $("<img/>");
        var file = document.querySelector("#new-image").files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            img.attr("src", reader.result);
        }
        if (file) {
            reader.readAsDataURL(file);
        } else {
            img.src = "";
        }
        var btn = $("<a/>", {
            "class": "action-button delete",
            "id": imgCount,
            "href": "/MyAlbums/LOL"
        });
        btn.text("Delete");
        var tr = $("<tr/>");
        var td = $("<td/>");
        td.append(title);
        tr.append(td);
        var td = $("<td/>", { "class": "padding-clean" });
        td.append(img);
        tr.append(td);
        var td = $("<td/>", { "class": "clean" });
        td.append(btn);
        tr.append(td);
        input.attr("id", "");
        tr.append(input);
        $("tbody").append(tr);
        var newInput = $("<input/>", { "type": "file", "id": "new-image" });
        $("#Save").before(newInput);
        imgCount++;
    });
}
$("#Save").on("click", function () {
    var list = $("#edit-table img");
    var images = 0;
    for (var it = 0; it < list.length; it++) {
        var re = /^(\/image|data:image)/g;
        var src = list.eq(it).attr("src");
        if (re.test(src)) {
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

createModel();
newImage();