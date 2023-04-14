

function loadAnnouncementForm(source, id) {
    globalFunctions.loadPopup(source, "/Announcements/InitAnnouncementForm?id=" + id, id ? "Edit Announcement" : "Add Announcement");
}

function onSaveSuccess(data) {
    globalFunctions.showSuccessMessage(data.message);
    globalFunctions.closeCommonModel();
    loadAllAnnouncements();
} 

$(document).ready(function () {
    loadAllAnnouncements();
});



function loadAllAnnouncements() {
    $("#getAllAnnouncementList").load('/Announcements/LoadAllAnnouncements', function (html) {
        globalFunctions.initializeDataTable('#tblAnnouncements', {
            "columnDefs": [
                { "orderable": false, "targets": [3, 4] }
            ],
            "oLanguage": {
                "sLengthMenu": "Display Records _MENU_ ",
            }
        });
    });
}

function deleteAnnouncement(source, id) {
  
    globalFunctions.deleteConfirm($(source));
    $(source).on("deleteConfirm", function () {
       
        $.ajax({
            type: "POST",
            data: JSON.stringify({ id: id }),
            url: '/Announcements/DeleteAnnouncement?id=' + id,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
              
                if (data) {
                    $(source).parents().closest('tr').remove();
                    loadAllAnnouncements();
                    globalFunctions.showErrorMessage("Announcement removed !")
                   

                }
                else {
                    globalFunctions.onError();
                }
            }

        });
    });
}


