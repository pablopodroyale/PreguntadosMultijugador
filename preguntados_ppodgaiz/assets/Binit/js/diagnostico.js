


function deleteAction(tableId, id, urlDelete, descripcionEntidad) {


    swal({
        title: "¿Está seguro de que desea eliminar " + descripcionEntidad + "?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sí",
        cancelButtonText: "No",
        closeOnConfirm: false
    }, function () {
        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: urlDelete + id,
            success: function (result) {
                swal("¡Eliminado!", descripcionEntidad + " fue eliminada.", "success");
                location.reload();
            },
            error: function (error) {
                swal("¡Error!", descripcionEntidad + " no fue eliminada.", "error");
            }
        });
    });

}
