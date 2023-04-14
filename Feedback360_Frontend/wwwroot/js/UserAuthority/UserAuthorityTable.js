$(document).ready(function () {

    LoadUserAuthorityList();
    $(document).on("change", "#BankId", function () {
        LoadUserAuthorityList();
    });
});

function LoadUserAuthorityList() {
    const bankId = parseInt($('#BankId').val());
    console.log(bankId);
    if (!isNaN(bankId)) {
        $('#UserAuthorityGrid').load('/UserAuthority/LoadUserAuthorityList?bankId=' + bankId, function (html) {
            globalFunctions.initializeDataTable('#UserAuthorityTbl', {
                "columnDefs": [
                    { "orderable": false, "targets": [3] }
                ],
                "oLanguage": {
                    "sLengthMenu": "Display Records _MENU_ ",
                }
            });
        });
    }
   
}

function loadUserAuthorityForm(source, id) {
    globalFunctions.loadPopup(source, "/UserAuthority/SetUserAuthority?id=" + id, "Set Authority");

}

function onSaveSuccess(data) {
    globalFunctions.showSuccessMessage(data.message);
    globalFunctions.closeCommonModel();
    LoadUserAuthorityList();
}
