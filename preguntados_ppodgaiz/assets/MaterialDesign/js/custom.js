 $(document).ready(function(){
        //Basic Example
     $("#data-table-basic").bootgrid({
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
                    commands = "<a type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" href=\"/Cuenta/Edit/" + row.Id + "\"><span class=\"zmdi zmdi-edit\"></span></a> " +
                            "<a type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" href=\"/Cuenta/Delete/" + row.Id + "\"><span class=\"zmdi zmdi-delete\"></span></a>";
                    return commands;
                },
                "commands-episodios": function (column, row) {
                    commands = "<a type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" href=\"/Episodio/Editar?id=" + row.Id + "&programaId= "+ row.programaId +"\"><span class=\"zmdi zmdi-edit\"></span></a> ";
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
                infos: "Mostrando {{ctx.start}} a {{ctx.end}} de {{ctx.total}} registros",
                all: "Todos",
                refresh: "Actualizar"
            }
        });
    });