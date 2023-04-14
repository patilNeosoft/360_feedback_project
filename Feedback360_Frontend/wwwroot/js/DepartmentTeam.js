$(document).ready(function () {
    loadAllUsers();
    loadAllTeamMembers();

});

function loadAllUsers() {
    $("#AvailableMembersList").load('/DepartmentTeam/GetAllAvailableMembersList', function (html) {
        globalFunctions.initializeDataTable('#getEmployeeListByDepAndBankIdTbl', {
            "columnDefs": [
                { "orderable": false, "targets": [0, 3, 4, 5] }
            ],
            "oLanguage": {
                "sLengthMenu": "Display Records _MENU_ ",
            }
        });
    });
}

function loadAllTeamMembers() {
    $("#MyTeamMembersList").load('/DepartmentTeam/loadMyTeam', function (html) {
        globalFunctions.initializeDataTable('#getMyTeamTbl', {
            "columnDefs": [
                { "orderable": false, "targets": [0, 1, 5,-1] }
            ],
            "oLanguage": {
                "sLengthMenu": "Display Records _MENU_ ",
            }
        });
    });
}

function loadAddMemberForm(source, UserId, UserName) {
    var userName = UserName.replaceAll(' ', '-');
    globalFunctions.loadPopup(source, "/DepartmentTeam/InitAddMemberForm?UserId=" + UserId + "&UserName=" + userName,"Add Member To Group");
}

function onSaveSuccess(data) {
    globalFunctions.showSuccessMessage(data.message);
    globalFunctions.closeCommonModel();
    loadAllUsers();
}

function removeTeamMember(source, DeptTeamId) {
    globalFunctions.deleteConfirm($(source));
    $(source).on("deleteConfirm", function () {
        $.ajax({
            type: "POST",
            url: '/DepartmentTeam/DeleteGroupMember?DepTeamId=' + DeptTeamId,
            data: JSON.stringify({ DeptTeamId: DeptTeamId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    $(source).parents().closest('tr').remove();
                    loadAllTeamMembers();
                    globalFunctions.showErrorMessage("team member removed !")
                }
                else {
                    globalFunctions.onError();
                }
            }
        });
    });
}

function dropGroup(source, LeaderId) {
    globalFunctions.deleteConfirm($(source),"droup complete group ?");
    $(source).on("deleteConfirm", function () {
        $.ajax({
            type: "POST",
            url: '/DepartmentTeam/DropGroup?leaderId=' + LeaderId,
            data: JSON.stringify({ LeaderId: LeaderId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    //$(source).parents().closest('tr').remove();
                    loadAllTeamMembers();
                    globalFunctions.showErrorMessage("team droped !")
                }
                else {
                    globalFunctions.onError();
                }
            }
        });
    });
}




