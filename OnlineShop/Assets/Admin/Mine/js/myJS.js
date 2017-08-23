$(document).ready(function () {

    $('#AlertBox').removeClass('hide');
    $('#AlertBox').delay(10000).slideUp(5000);

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