
$(document).ready(function () {

    loadFeedbackSummary();
    $(document).on("change", "#fYear", function () {
        loadFeedbackSummary();
    });
});


function loadFeedbackSummary(source) {
    //document.getElementById('fYear').addEventListener("change", (source) => {
       // var financialYear = document.getElementById('fYear').value;
    var financialYear = $('#fYear').val();
    $("#getFeedbackSummaryList").load('/SelfFeedback/LoadFeedbackSummary?financialYear=' + financialYear, function (html) {
            globalFunctions.initializeDataTable('#getFeedbackSummaryTbl', {
                "columnDefs": [
                    { "orderable": false, "targets": [0,1,5] }
                ]
            });
        });
    
}



