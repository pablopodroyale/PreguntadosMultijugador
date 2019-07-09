//usage: $(document).ready(function(){ --- insertar llamado a Initiator ----  })
function DataTableInitiator(dataTableId, buscadorId ,urlDelete, descripcionEntidad, ocultarEdit, ocultarDelete, exportExcel) {
    $('#' + dataTableId).DataTable({
        responsive: true,
        dom: 'rt<"dataTables_footer"ip>',
        language: {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        initComplete: function () {
            var api = this.api(),
                searchBox = $('#' + buscadorId);

            // Bind an external input as a table wide search box
            if (searchBox.length > 0) {
                searchBox.on('keyup', function (event) {
                    api.search(event.target.value).draw();
                });
            }
        },
        drawCallback: function () {
            var table = $('#' + dataTableId).DataTable();
            $('.delete-action').on('click', function () {
                var id = table.row($(this).parents('tr')).id();
                //var id = table.row($(this).parents('tr')).data()[0];
                var data = table.row($(this).parents('tr')).data();
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
                        type: 'POST',
                        url: urlDelete,
                        dataType: 'json',
                        data: { Id: id }
                        ,
                        success: function (result) {
                            swal("¡Eliminado!", descripcionEntidad + " fue eliminada.", "success");
                            location.reload();
                        },
                        error: function (error) {
                            swal("¡Error!", descripcionEntidad + " no fue eliminada.", "error");
                        }
                    });
                });
            });
        }
    });

}