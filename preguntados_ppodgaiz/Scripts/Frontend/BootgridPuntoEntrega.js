 $(document).ready(function(){
        //Basic Example
     $("#data-table-punto-entrega").bootgrid({
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
                    commands = '<a href="#"><i class="pe-7s-look"></i></a>' +
                        '<a href="#"><i class="pe-7s-note"></i></a>' +
                        '<a href="#"><i class="pe-7s-trash"></i></a>' ;
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
                infos: "MOSTRANDO {{ctx.end}} DE {{ctx.total}} USUARIOS",
                all: "Todos",
                refresh: "Actualizar"
            },
            templates: {
                search: ""
            }
     });

    });