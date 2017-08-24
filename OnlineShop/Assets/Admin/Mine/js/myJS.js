$(document).ready(function () {

    $('#AlertBox').removeClass('hide');
    $('#AlertBox').delay(1000).slideUp(500);

    $('#userindex').DataTable({
        "lengthMenu": [5, 10, 25, 50, 75, 100]
    });

    $('#CategoryTable').DataTable({
        "lengthMenu": [5, 10, 25, 50, 75, 100]
    });

    $('#ContentTable').DataTable({
        "lengthMenu": [5, 10, 25, 50, 75, 100]
    });
});