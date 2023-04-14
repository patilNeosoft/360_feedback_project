function loadUserQueryForm(source) {
    globalFunctions.loadPopup(source, "/UserQuery/UserQueryForm","Create New Query");

}
$(document).ready(function () {
    loadAllUserQueries();
});

function onSaveSuccess(data) {
    globalFunctions.showSuccessMessage(data.message);
    globalFunctions.closeCommonModel();
    loadAllUserQueries();
}

function loadAllUserQueries() {
    $("#getAllUserQueries").load('/UserQuery/LoadUserQueries', function (html) {
        globalFunctions.initializeDataTable('#QueriesTable', {

            "columnDefs": [
                { "orderable": false, "targets": [3, 4] }
            ],
            "oLanguage": {
                "sLengthMenu": "Display Records _MENU_ ",
            }
        });
    });
}

function deleteUserQuery(source, id) {
    globalFunctions.deleteConfirm($(source));
    $(source).on("deleteConfirm", function () {
        $.ajax({
            type: "POST",
            url: '/UserQuery/DeleteUserQuery?queryId=' + id,
            data: JSON.stringify({ id: id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $(source).parents().closest('tr').remove();
                    loadAllUserQueries();
                    globalFunctions.showErrorMessage("query removed !")
                    

                }
                else {
                    globalFunctions.onError();
                }
            }

        });
    });
}

