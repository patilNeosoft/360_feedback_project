function addComment(source, queryid) {
    globalFunctions.loadPopup(source, "/AdminQuery/AdminComment?queryid=" + queryid, "Comment");

}

function deleteQuery(source, queryId) {
    globalFunctions.deleteConfirm($(source));
    $(source).on("deleteConfirm", function () {
        $.ajax({
            type: "POST",
            url: '/AdminQuery/DeleteQuery?id=' + queryId,
            data: JSON.stringify({ id: queryId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    loadAdminQueries();
                    globalFunctions.onError();
                }
                else {
                    globalFunctions.onError();
                }
            }
        });
    });
}

$(document).on('click', '#deleteAll', function (source) {
    globalFunctions.deleteConfirm($(source));
    $(source).on("deleteConfirm", function () {
        var bulkDeleteVM = {
            idList: []
        }

        $('[type=checkbox].myRowCheck:checked').each(function (index, checkBox) {
            bulkDeleteVM.idList.push($(checkBox).data('checkid'));
        });

        $.ajax('/AdminQuery/DeleteAllQuery', {
            method: 'POST',
            data: bulkDeleteVM,
            traditional: true,
            success: function (data) {
            
                loadAdminQueries();
                globalFunctions.onError();
            }
        });
    });
});

$(document).ready(function () {
    loadAdminQueries();
    globalFunctions.enableCheckboxCascade('#myCheck', '.myRowCheck', '#deleteAll');
    $(document).on('click', '.trSelect', function () {
        var queryId = $(this).data('queryid');
        location.href = '/AdminQuery/GetQueryAndCommentDetails?queryid=' + queryId;
    });
});

const loadAdminQueries = () => {
    $("#AdminQueryGrid").load('/AdminQuery/Load', function (html) {
        globalFunctions.initializeDataTable('#queryTbl', {
            "columnDefs": [
                { "orderable": false, "targets": [0, 4] }
            ],
            "oLanguage": {
                "sLengthMenu": "Display Records MENU ",
            }
        });
    });
}







