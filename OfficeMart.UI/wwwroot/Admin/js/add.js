
window.onload = function () {

    if (window.File && window.FileList && window.FileReader) {
        var filesInput = document.getElementById("files");

        filesInput.addEventListener("change", function (event) {

            var files = event.target.files;
            var output = document.getElementById("imgThumbnailPreview");
            var imgThumbnailPreview = $("#imgThumbnailPreview");
            imgThumbnailPreview.css("height", "240px");

            for (var i = 0; i < files.length; i++) {
                var file = files[i];

                if (!file.type.match('image'))
                    continue;

                var picReader = new FileReader();

                picReader.addEventListener("load", function (event) {

                    var picSrc = event.target.result;

                    var imgThumbnailElem = `<div id=${i} class='imgThumbContainer'><div class='IMGthumbnail' ><img style='object-fit: fill' src='${picSrc}'" +
                        "title='" + file.name + "'/>`;


                    output.innerHTML = output.innerHTML + imgThumbnailElem + `<span onclick='removeMe(${i})' class='appended-span btn btn-danger' style='display:block;text-align:center;color:white;'>SIL</span></div>`;

                });
                picReader.readAsDataURL(file);
            }

        });
    }
    else {
        alert("Your browser does not support File API");
    }
}

function removeMe(id) {
    var parent = document.getElementById(`${id}`);
    parent.remove();
    var mainParent = document.getElementById("imgThumbnailPreview");
    if (mainParent.childElementCount === 0) {
        mainParent.style.height = "0";
    }
}