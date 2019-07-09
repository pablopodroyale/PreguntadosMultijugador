$(".data-table-archivos").bootgrid({
    columnSelection: false,
    navigation: 0,
    css: {
        icon: 'zmdi icon',
        iconColumns: 'zmdi-view-module',
        iconDown: 'zmdi-sort-amount-desc',
        iconRefresh: 'zmdi-refresh',
        iconUp: 'zmdi-sort-amount-asc'
    },
    formatters: {
        "commands": function (column, row) {
            commands =
                '<a type="button" caller-method-delete caller-parameter="' + row.Id + '" class="btn btn-icon command-delete waves-effect waves-circle"><span class=\"zmdi zmdi-delete\"></span></a>' +
                '<a href="@Url.Action("ObtenerArchivo","Archivo")/' + row.Id + '" target=/"_blank/" class="btn btn-icon waves-effect waves-circle"><span class=\"zmdi zmdi-eye\"></span></a>';
            return commands;
        }
    },
    labels: {
        noResults: "No se cargo el PDF",
        search: "Buscar",
        loading: "Cargando..."
    }
}).on("loaded.rs.jquery.bootgrid", function () {
    if ($(".data-table-archivos").bootgrid("getTotalRowCount") == 1) $("#cargarPDF").hide();

    $("a[caller-method-delete]").on('click', function () {
        var id = $(this).attr('caller-parameter');
        swal({
            title: "¿Está seguro de que desea eliminar el PDF?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sí",
            cancelButtonText: "No",
            closeOnConfirm: true
        }, function () {
            $(".data-table-archivos").bootgrid("remove", [id]);
            $.ajax({ url: "/Archivo/Eliminar?archivoID=" + id, method: "GET" });
            $("#cargarPDF").show();
        });
    });
});