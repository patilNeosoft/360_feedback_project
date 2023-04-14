function loadUserRoleForm(source, id) {
    globalFunctions.loadPopup(source, "/Admin/InitUserRoleForm?id=" + id, id ? "Edit User Role" : "Add User Role");
   
}
function setpermission(source, id) {
    globalFunctions.loadPopup(source, "/Permission/SetPermissions?id=" + id,"Set Permissions");

}
function removepermission(source, id) {
    globalFunctions.loadPopup(source, "/Permission/RemovePermissions?id=" + id, "Remove Permissions");

}

function onSaveSuccess(data) {
    globalFunctions.showSuccessMessage(data.message);
    globalFunctions.closeCommonModel();
    loadAllUserRoles();
}

function deleteUserRole(source, id) {
    globalFunctions.deleteConfirm($(source));
    $(source).on("deleteConfirm", function () {
        $.ajax({
            type: "POST",
            url: '/Admin/DeleteUserRole?id=' + id,
            data: JSON.stringify({ id: id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $(source).parents().closest('tr').remove();
                    loadAllUserRoles();
                    globalFunctions.showErrorMessage("user role removed !")

                }
                else {
                    globalFunctions.onError();
                }
            }
        });
    });
}

$(document).ready(function () {
    loadAllUserRoles();
});


function loadAllUserRoles() {
    $("#getAllUserRoles").load('/Admin/LoadUserRoles', function (html) {
        globalFunctions.initializeDataTable('#UserTbl', {
            "columnDefs": [
                { "orderable": false, "targets": [2, 3, 4, 5] }
            ],
            "oLanguage": {
                "sLengthMenu": "Display Records _MENU_ ",
            }
        });
    });
}









function checkRoleName() {
    var roleName = document.getElementById('RoleName').value;
    $.ajax({
        type: "POST",
        url: '/Admin/IsRoleNameAvailable?RoleName=' + roleName,
        data: JSON.stringify({ roleName: roleName }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        complete: function (data) {
            if (data.responseText == "false") {
                globalFunctions.showErrorMessage("User Role Name already Exists !");  
            }
        }
    });
}







