$(document).ready(function () {
    //Basic Example
    $("#data-table-usuario").bootgrid({
        caseSensitive: false,
        rowCount: 25,

        columnSelection: false,
        css: {
            icon: 'zmdi icon',
            iconColumns: 'zmdi-view-module',
            iconDown: 'zmdi-sort-amount-desc',
            iconRefresh: 'zmdi-refresh',
            iconUp: 'zmdi-sort-amount-asc'
        },
        formatters: {
            "commands": function (column, row) {
                commands = '<a href="/Frontend/DetalleUsuario/' + row.Id.trim() + '"><i class="pe-7s-look"></i></a>' +
                    '<a href="/Frontend/EditarUsuario/' + row.Id.trim() + '"><i class="pe-7s-note"></i></a>' +
                    '<a command-delete caller-parameter="' + row.Id.trim() + '" caller-nombre="el Usuario"><i class="pe-7s-trash"></i></a>';
                return commands;
            }
        },
        searchSettings: {
            delay: 500,
            characters: 3
        },
        labels: {
            noResults: "No se han encontrado resultados para su búsqueda",
            loading: "Cargando...",
            search: "Buscar",
            //infos: "fsdf {{ctx.end}} DE {{ctx.total}}",
            infos: "MOSTRANDO {{ctx.end}} RESULTADOS",
            all: "Todos",
            refresh: "Actualizar"
        },
        templates: {
            search: ""
        }
    });

}).on("loaded.rs.jquery.bootgrid", function () {
    var callerMethod = function () {
        var id = $(this).attr('caller-parameter');
        var nombre = $(this).attr('caller-nombre');
        swal({
            title: "¿Está seguro de que desea eliminar " + nombre + "?",
            type: "warning",
            showCloseButton: true,
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sí",
            cancelButtonText: "No",
            closeOnConfirm: false
        }, function () {
            $.ajax({
                dataType: 'json',
                type: 'GET',
                url: window.location.origin + '/Frontend/EliminarUsuario/' + id,
                success: function (result) {
                    swal("¡Eliminado!", "El usuario fue eliminado.", "success");
                    setTimeout(function () {
                        location.href = location.origin + location.pathname;
                    }, 3000);
                },
                error: function (error) {
                    swal("¡Ups!", "Hemos tenido un inconveniente. Contactese con el administrador.", "error");
                }
            });
        });
    }

    $("a[command-delete]").on('click', callerMethod);
});