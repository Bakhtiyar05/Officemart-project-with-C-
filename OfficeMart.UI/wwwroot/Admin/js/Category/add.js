$(document).ready(function () {

    $("#choosePhoto").click(function () {
        $("#Image").click();
    });

});

function ShowPhoto(input) {
    let photoFile = input.files[0];

    let reader = new FileReader();
    reader.readAsDataURL(photoFile);

    reader.onload = function () {
        let photoBase64 = reader.result;
        let imgTag = document.getElementById("photoBase");
        imgTag.src = photoBase64;
    }
};