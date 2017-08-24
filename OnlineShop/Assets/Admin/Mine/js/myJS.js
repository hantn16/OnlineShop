$(document).ready(function () {
    var idtablename = '#userindex,#ProductCategoryTable,#CategoryTable,#ContentTable';
    $(idtablename).DataTable({
        "lengthMenu": [5, 10, 25, 50, 75, 100]
    });

});