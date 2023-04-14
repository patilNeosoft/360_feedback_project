function loadBannerForm(source, id) {
    globalFunctions.loadPopup(source, "/Banner/InitBannerForm?id=" + id, id ? "Edit Banner" : "Add Banner");
    
}
$(document).ready(function () {
    loadAllBanners();
});


function loadAllBanners() {
    $("#getAllBannerList").load('/Banner/LoadAllBanners', function (html) {
        globalFunctions.initializeDataTable('#BannerTbl', {
            "columnDefs": [
                { "orderable": false, "targets": [2, 3, 4] }
            ],
            "oLanguage": {
                "sLengthMenu": "Display Records _MENU_ ",
            }
        });
    });
}

function deleteBanner(source, id) {
    globalFunctions.deleteConfirm($(source));
    $(source).on("deleteConfirm", function () {
        $.ajax({
            type: "POST",
            url: '/Banner/DeleteBanner?id=' + id,
            data: JSON.stringify({ id: id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $(source).parents().closest('tr').remove();
                    loadAllBanners();
                    globalFunctions.showErrorMessage("banner removed !")

                }
                else {
                    globalFunctions.onError();
                }
            }

        });
    });
}


function FileValidation(){
    var fi = document.getElementById('formFile');
    // Check if any file is selected.
  
    if (fi.files.length > 0) {
        for (const i = 0; i <= fi.files.length - 1; i++) {

            const fsize = fi.files.item(i).size;
            const file = Math.round((fsize / 1024));
            // The size of the file.
            if (file >= 2096) {


                document.getElementById("errorMessage").innerHTML = 'File too Big, please select a file less than 2mb';
                fi.value = '';
            }
            else {
                document.getElementById("errorMessage").innerHTML = '';

            }
        }
    }
}
