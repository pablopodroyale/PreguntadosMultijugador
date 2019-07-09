// Almacenar lesiones
$("#data-table-lesiones").bootgrid({
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
            commands = '<a type="button" caller-method-edit caller-parameter="' + row.Id + '" class="btn btn-icon command-delete waves-effect waves-circle"><span class="zmdi zmdi-edit"></span></a>' +
                '<a type="button" caller-method-delete caller-parameter="' + row.Id + '" class="btn btn-icon command-delete waves-effect waves-circle"><span class=\"zmdi zmdi-delete\"></span></a>';
            return commands;
        }
    },
    labels: {
        noResults: "No hay lesiones cargadas",
        search: "Buscar",
        loading: "Cargando..."
    }
}).on("loaded.rs.jquery.bootgrid", function () {
    $("#Formulario").hide();

    // Variabilizacion
    $("#Guia").on("change", mostrarOcultarDatos);
    $("#Hora").on("change", activarDesactivarCuadrante);
    $("#Cuadrante").on("change", activarDesactivarHora);

    $("a[caller-method-delete]").on('click', function () {
        var id = $(this).attr('caller-parameter');
        swal({
            title: "¿Está seguro de que desea eliminar la lesión?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sí",
            cancelButtonText: "No",
            closeOnConfirm: true
        }, function () {
            $("#data-table-lesiones").bootgrid("remove", [id]);
            $.ajax({ url: "IntervencionismoMamario/EliminarLesion?id=" + id, method: "GET" })
        });
    });
    $("a[caller-method-edit]").on('click', function () {
        var id = $(this).attr('caller-parameter');
        var i = 0;
        while (i < $("#data-table-lesiones").bootgrid("getCurrentRows").length && $("#data-table-lesiones").bootgrid("getCurrentRows")[i].Id != id) i++;
        var lesion = $("#data-table-lesiones").bootgrid("getCurrentRows")[i];
        var guiaId = typeof lesion.TipoGuia == "object" ? lesion.TipoGuia.Id : JSON.parse(lesion.TipoGuia).Id;
        $("#tabla-archivos").load("/Archivo/ObtenerArchivos?lesionId=", id, function () {
            $(".fg-line").addClass("fg-toggled");
            $("#btnAgregarLesion").hide();
            $("#btnCancelLesion").show();
            $("#submit").hide();
            $("#idLesionEditando").val(id);
            $("#Guia").val(guiaId);
            LlenarFormularioCabeza(lesion);
        });
    });
    $("a[caller-method-details]").on('click', function () {
        $(".fg-line").addClass("fg-toggled");
        $("#btnCancelLesion").show();
        var id = $(this).attr('caller-parameter');
        var i = 0;
        while (i < $("#data-table-lesiones").bootgrid("getCurrentRows").length && $("#data-table-lesiones").bootgrid("getCurrentRows")[i].Id != id) i++;
        var lesion = $("#data-table-lesiones").bootgrid("getCurrentRows")[i];
        $("#idLesionEditando").val(id);
        var guiaId = typeof lesion.TipoGuia == "object" ? lesion.TipoGuia.Id : JSON.parse(lesion.TipoGuia).Id;
        $("#Guia").val(guiaId);
        MostrarFormularioCabeza(lesion)
        // tengo que esconder los N/A
    });


    function LlenarFormularioCabeza(lesion) {
        $.ajax({ url: "/ConfiguracionGuia/GetConfiguracionGuias?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
            .success(function (data) {
                if (data.MostrarNoMasa) $(".mostrarNoMasa").show(); else $(".mostrarNoMasa").hide();
                if (data.MostrarCurva) $(".mostrarCurva").show(); else $(".mostrarCurva").hide();
                if (data.MostrarCalcificaciones) $(".mostrarCalcificaciones").show(); else $(".mostrarCalcificaciones").hide();
                if (data.MostrarHallazgosAsociados) $(".mostrarHallazgoAsociado").show(); else $(".mostrarHallazgoAsociado").hide();
                if (data.MostrarCamposEspeciales) $(".mostrarCamposEspeciales").show(); else $(".mostrarCamposEspeciales").hide();
                if (data.MostrarAsimetria) $(".mostrarAsimetria").show(); else $(".mostrarAsimetria").hide();
                // CAMPOS MASA
                if (data.MostrarDensidad) $(".mostrarDensidad").show(); else $(".mostrarDensidad").hide();
                if (data.MostrarRealceInterno) $(".mostrarRealceInterno").show(); else $(".mostrarRealceInterno").hide();
                if (data.MostrarOrientacion) $(".mostrarOrientacion").show(); else $(".mostrarOrientacion").hide();
                if (data.MostrarPatronEcografico) $(".mostrarPatronEcografico").show(); else $(".mostrarPatronEcografico").hide();
                if (data.MostrarComportamientoPosterior) $(".mostrarComportamientoPosterior").show(); else $(".mostrarComportamientoPosterior").hide();
                // CAMPOS CALCIFICACIONES
                if (data.MostrarTipicamenteBenignas) $(".mostrarTipicamenteBenignas").show(); else $(".mostrarTipicamenteBenignas").hide();
                if (data.MostrarMorfologiaSospechosa) $(".mostrarMorfologiaSospechosa").show(); else $(".mostrarMorfologiaSospechosa").hide();
                if (data.MostrarDistribucion) $(".mostrarDistribucion").show(); else $(".mostrarDistribucion").hide();
                if (data.MostrarTiposDeCalcificacion) $(".mostrarTiposDeCalcificacion").show(); else $(".mostrarTiposDeCalcificacion").hide();

                $.ajax({ url: "/Margen/GetMargenes?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
                    .success(function (data) {
                        //if ($("#MargenAgrupado").val() == null || $("#MargenAgrupado").val() == "") {
                        var x = document.getElementById("MargenAgrupado");
                        while (x.length > 0)
                            x.remove(0);
                        agrupados = {};
                        noAgrupados = [];
                        grupos = [];
                        for (var i = 0; i < data.length; i++) {
                            if (!data[i].TipoMargen) {
                                noAgrupados.push({
                                    Nombre: data[i].Nombre,
                                    Id: data[i].Id
                                });
                            } else {
                                if (!agrupados[data[i].TipoMargen.Id]) {
                                    agrupados[data[i].TipoMargen.Id] = { Nombre: data[i].TipoMargen.Nombre, lista: [] };
                                    grupos.push({ Nombre: data[i].TipoMargen.Nombre, Id: data[i].TipoMargen.Id });
                                }
                                agrupados[data[i].TipoMargen.Id].lista.push(data[i]);
                            }
                        }
                        $("#MargenAgrupado").append($('<option>', {
                            value: "",
                            text: "N/A"
                        }));
                        for (var x = 0; x < noAgrupados.length; x++) {
                            $("#MargenAgrupado").append($('<option>', {
                                value: noAgrupados[x].Id,
                                text: noAgrupados[x].Nombre
                            }));
                        }

                        for (var x = 0; x < grupos.length; x++) {
                            var group = $('<optgroup label="' + grupos[x].Nombre + '" />');
                            for (var i = 0; i < agrupados[grupos[x].Id].lista.length; i++) {
                                $(group).append($('<option>', {
                                    value: agrupados[grupos[x].Id].lista[i].Id,
                                    text: agrupados[grupos[x].Id].lista[i].Nombre
                                }));
                            }

                            group.appendTo($("#MargenAgrupado"));
                        }
                        $("#MargenAgrupado").trigger("chosen:updated");
                        //}

                        $.ajax({ url: "/TipoProcedimiento/GetProcedimientos?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
                            .success(function (data) {
                                //if ($("#TipoProcedimiento").val() == "") {
                                var x = document.getElementById("TipoProcedimiento");
                                while (x.length > 0)
                                    x.remove(0);
                                noAgrupados = [];
                                for (var i = 0; i < data.length; i++) {
                                    noAgrupados.push({
                                        Nombre: data[i].Nombre,
                                        Id: data[i].Id
                                    });
                                }
                                $("#TipoProcedimiento").append($('<option>', {
                                    value: "",
                                    text: "N/A"
                                }));
                                for (var x = 0; x < noAgrupados.length; x++) {
                                    $("#TipoProcedimiento").append($('<option>', {
                                        value: noAgrupados[x].Id,
                                        text: noAgrupados[x].Nombre
                                    }));
                                }

                                $("#TipoProcedimiento").trigger("chosen:updated");
                                //}

                                $.ajax({ url: "/TipoLesion/GetTiposLesionBasicos?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
                                    .success(function (data) {
                                        if (data.length > 0) {
                                            $(".mostrarTiposBasicos").show();
                                        } else {
                                            $(".mostrarTiposBasicos").hide();
                                            $("#TiposBasicos").val(null);
                                        }
                                        //if ($("#TiposBasicos").val() == null || $("#TiposBasicos").val() == "") {
                                        var x = document.getElementById("TiposBasicos");
                                        while (x.length > 0)
                                            x.remove(0);
                                        noAgrupados = [];
                                        for (var i = 0; i < data.length; i++) {
                                            noAgrupados.push({
                                                Nombre: data[i].Nombre,
                                                Id: data[i].Id
                                            });
                                        }
                                        for (var x = 0; x < noAgrupados.length; x++) {
                                            $("#TiposBasicos").append($('<option>', {
                                                value: noAgrupados[x].Id,
                                                text: noAgrupados[x].Nombre
                                            }));
                                        }
                                        if (data.length > 0) $(".mostrarTiposBasicos").show(); else $(".mostrarTiposBasicos").hide();

                                        $("#TiposBasicos").trigger("chosen:updated");
                                        //}

                                        $.ajax({ url: "/HallazgoAsociado/GetHallazgos?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
                                            .success(function (data) {
                                                //if ($("#HallazgoAsociado").val() == null || $("#HallazgoAsociado").val() == "") {
                                                var x = document.getElementById("HallazgoAsociado");
                                                while (x.length > 0)
                                                    x.remove(0);
                                                agrupados = {};
                                                noAgrupados = [];
                                                grupos = [];
                                                for (var i = 0; i < data.length; i++) {
                                                    if (!data[i].TipoHallazgo) {
                                                        noAgrupados.push({
                                                            Nombre: data[i].Nombre,
                                                            Id: data[i].Id
                                                        });
                                                    } else {
                                                        if (!agrupados[data[i].TipoHallazgo.Id]) {
                                                            agrupados[data[i].TipoHallazgo.Id] = { Nombre: data[i].TipoHallazgo.Nombre, lista: [] };
                                                            grupos.push({ Nombre: data[i].TipoHallazgo.Nombre, Id: data[i].TipoHallazgo.Id });
                                                        }
                                                        agrupados[data[i].TipoHallazgo.Id].lista.push(data[i]);
                                                    }
                                                }
                                                $("#HallazgoAsociado").append($('<option>', {
                                                    value: "",
                                                    text: "N/A"
                                                }));
                                                for (var x = 0; x < noAgrupados.length; x++) {
                                                    $("#HallazgoAsociado").append($('<option>', {
                                                        value: noAgrupados[x].Id,
                                                        text: noAgrupados[x].Nombre
                                                    }));
                                                }

                                                for (var x = 0; x < grupos.length; x++) {
                                                    var group = $('<optgroup label="' + grupos[x].Nombre + '" />');
                                                    for (var i = 0; i < agrupados[grupos[x].Id].lista.length; i++) {
                                                        $(group).append($('<option>', {
                                                            value: agrupados[grupos[x].Id].lista[i].Id,
                                                            text: agrupados[grupos[x].Id].lista[i].Nombre
                                                        }));
                                                    }

                                                    group.appendTo($("#HallazgoAsociado"));
                                                }

                                                $("#HallazgoAsociado").trigger("chosen:updated");
                                                // }

                                                LlenarFormulario(lesion);
                                            });
                                    });
                            });
                    });
            });
    }

    function MostrarFormularioCabeza(lesion) {
        $.ajax({ url: "/ConfiguracionGuia/GetConfiguracionGuias?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
            .success(function (data) {
                if (data.MostrarNoMasa) $(".mostrarNoMasa").show(); else $(".mostrarNoMasa").hide();
                if (data.MostrarCurva) $(".mostrarCurva").show(); else $(".mostrarCurva").hide();
                if (data.MostrarCalcificaciones) $(".mostrarCalcificaciones").show(); else $(".mostrarCalcificaciones").hide();
                if (data.MostrarHallazgosAsociados) $(".mostrarHallazgoAsociado").show(); else $(".mostrarHallazgoAsociado").hide();
                if (data.MostrarCamposEspeciales) $(".mostrarCamposEspeciales").show(); else $(".mostrarCamposEspeciales").hide();
                if (data.MostrarAsimetria) $(".mostrarAsimetria").show(); else $(".mostrarAsimetria").hide();
                // CAMPOS MASA
                if (data.MostrarDensidad) $(".mostrarDensidad").show(); else $(".mostrarDensidad").hide();
                if (data.MostrarRealceInterno) $(".mostrarRealceInterno").show(); else $(".mostrarRealceInterno").hide();
                if (data.MostrarOrientacion) $(".mostrarOrientacion").show(); else $(".mostrarOrientacion").hide();
                if (data.MostrarPatronEcografico) $(".mostrarPatronEcografico").show(); else $(".mostrarPatronEcografico").hide();
                if (data.MostrarComportamientoPosterior) $(".mostrarComportamientoPosterior").show(); else $(".mostrarComportamientoPosterior").hide();
                // CAMPOS CALCIFICACIONES
                if (data.MostrarTipicamenteBenignas) $(".mostrarTipicamenteBenignas").show(); else $(".mostrarTipicamenteBenignas").hide();
                if (data.MostrarMorfologiaSospechosa) $(".mostrarMorfologiaSospechosa").show(); else $(".mostrarMorfologiaSospechosa").hide();
                if (data.MostrarDistribucion) $(".mostrarDistribucion").show(); else $(".mostrarDistribucion").hide();
                if (data.MostrarTiposDeCalcificacion) $(".mostrarTiposDeCalcificacion").show(); else $(".mostrarTiposDeCalcificacion").hide();

                $.ajax({ url: "/Margen/GetMargenes?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
                    .success(function (data) {
                        //if ($("#MargenAgrupado").val() == null || $("#MargenAgrupado").val() == "") {
                        var x = document.getElementById("MargenAgrupado");
                        while (x.length > 0)
                            x.remove(0);
                        agrupados = {};
                        noAgrupados = [];
                        grupos = [];
                        for (var i = 0; i < data.length; i++) {
                            if (!data[i].TipoMargen) {
                                noAgrupados.push({
                                    Nombre: data[i].Nombre,
                                    Id: data[i].Id
                                });
                            } else {
                                if (!agrupados[data[i].TipoMargen.Id]) {
                                    agrupados[data[i].TipoMargen.Id] = { Nombre: data[i].TipoMargen.Nombre, lista: [] };
                                    grupos.push({ Nombre: data[i].TipoMargen.Nombre, Id: data[i].TipoMargen.Id });
                                }
                                agrupados[data[i].TipoMargen.Id].lista.push(data[i]);
                            }
                        }
                        $("#MargenAgrupado").append($('<option>', {
                            value: "",
                            text: "N/A"
                        }));
                        for (var x = 0; x < noAgrupados.length; x++) {
                            $("#MargenAgrupado").append($('<option>', {
                                value: noAgrupados[x].Id,
                                text: noAgrupados[x].Nombre
                            }));
                        }

                        for (var x = 0; x < grupos.length; x++) {
                            var group = $('<optgroup label="' + grupos[x].Nombre + '" />');
                            for (var i = 0; i < agrupados[grupos[x].Id].lista.length; i++) {
                                $(group).append($('<option>', {
                                    value: agrupados[grupos[x].Id].lista[i].Id,
                                    text: agrupados[grupos[x].Id].lista[i].Nombre
                                }));
                            }

                            group.appendTo($("#MargenAgrupado"));
                        }
                        $("#MargenAgrupado").trigger("chosen:updated");
                        //}

                        $.ajax({ url: "/TipoProcedimiento/GetProcedimientos?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
                            .success(function (data) {
                                //if ($("#TipoProcedimiento").val() == "") {
                                var x = document.getElementById("TipoProcedimiento");
                                while (x.length > 0)
                                    x.remove(0);
                                noAgrupados = [];
                                for (var i = 0; i < data.length; i++) {
                                    noAgrupados.push({
                                        Nombre: data[i].Nombre,
                                        Id: data[i].Id
                                    });
                                }
                                $("#TipoProcedimiento").append($('<option>', {
                                    value: "",
                                    text: "N/A"
                                }));
                                for (var x = 0; x < noAgrupados.length; x++) {
                                    $("#TipoProcedimiento").append($('<option>', {
                                        value: noAgrupados[x].Id,
                                        text: noAgrupados[x].Nombre
                                    }));
                                }

                                $("#TipoProcedimiento").trigger("chosen:updated");
                                //}

                                $.ajax({ url: "/TipoLesion/GetTiposLesionBasicos?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
                                    .success(function (data) {
                                        if (data.length > 0) {
                                            $(".mostrarTiposBasicos").show();
                                        } else {
                                            $(".mostrarTiposBasicos").hide();
                                            $("#TiposBasicos").val(null);
                                        }
                                        //if ($("#TiposBasicos").val() == null || $("#TiposBasicos").val() == "") {
                                        var x = document.getElementById("TiposBasicos");
                                        while (x.length > 0)
                                            x.remove(0);
                                        noAgrupados = [];
                                        for (var i = 0; i < data.length; i++) {
                                            noAgrupados.push({
                                                Nombre: data[i].Nombre,
                                                Id: data[i].Id
                                            });
                                        }
                                        for (var x = 0; x < noAgrupados.length; x++) {
                                            $("#TiposBasicos").append($('<option>', {
                                                value: noAgrupados[x].Id,
                                                text: noAgrupados[x].Nombre
                                            }));
                                        }
                                        if (data.length > 0) $(".mostrarTiposBasicos").show(); else $(".mostrarTiposBasicos").hide();

                                        $("#TiposBasicos").trigger("chosen:updated");
                                        //}

                                        $.ajax({ url: "/HallazgoAsociado/GetHallazgos?tipoGuiaId=" + $("#Guia").val(), method: "GET" })
                                            .success(function (data) {
                                                //if ($("#HallazgoAsociado").val() == null || $("#HallazgoAsociado").val() == "") {
                                                var x = document.getElementById("HallazgoAsociado");
                                                while (x.length > 0)
                                                    x.remove(0);
                                                agrupados = {};
                                                noAgrupados = [];
                                                grupos = [];
                                                for (var i = 0; i < data.length; i++) {
                                                    if (!data[i].TipoHallazgo) {
                                                        noAgrupados.push({
                                                            Nombre: data[i].Nombre,
                                                            Id: data[i].Id
                                                        });
                                                    } else {
                                                        if (!agrupados[data[i].TipoHallazgo.Id]) {
                                                            agrupados[data[i].TipoHallazgo.Id] = { Nombre: data[i].TipoHallazgo.Nombre, lista: [] };
                                                            grupos.push({ Nombre: data[i].TipoHallazgo.Nombre, Id: data[i].TipoHallazgo.Id });
                                                        }
                                                        agrupados[data[i].TipoHallazgo.Id].lista.push(data[i]);
                                                    }
                                                }
                                                $("#HallazgoAsociado").append($('<option>', {
                                                    value: "",
                                                    text: "N/A"
                                                }));
                                                for (var x = 0; x < noAgrupados.length; x++) {
                                                    $("#HallazgoAsociado").append($('<option>', {
                                                        value: noAgrupados[x].Id,
                                                        text: noAgrupados[x].Nombre
                                                    }));
                                                }

                                                for (var x = 0; x < grupos.length; x++) {
                                                    var group = $('<optgroup label="' + grupos[x].Nombre + '" />');
                                                    for (var i = 0; i < agrupados[grupos[x].Id].lista.length; i++) {
                                                        $(group).append($('<option>', {
                                                            value: agrupados[grupos[x].Id].lista[i].Id,
                                                            text: agrupados[grupos[x].Id].lista[i].Nombre
                                                        }));
                                                    }

                                                    group.appendTo($("#HallazgoAsociado"));
                                                }

                                                $("#HallazgoAsociado").trigger("chosen:updated");
                                                // }

                                                LlenarFormulario(lesion, true);
                                                if (hayMasa()) $(".mostrarMasa").show(); else $(".mostrarMasa").hide();
                                                if (hayNoMasa()) $(".mostrarNoMasa").show(); else $(".mostraNorMasa").hide();
                                                if (hayCalcificacion()) $(".mostrarCalcificacion").show(); else $(".mostrarCalcificacion").hide();
                                                if (hayAsimetria()) $(".mostrarAsimetria").show(); else $(".mostrarAsimetria").hide();
                                                if (hayHallazgo()) $(".mostrarHallazgoAsociado").show(); else $(".mostrarHallazgoAsociado").hide();
                                                if (hayCurva()) $(".mostrarCurva").show(); else $(".mostrarCurva").hide();
                                                if (hayCasoEspecial()) $(".mostrarCamposEspeciales").show(); else $(".mostrarCamposEspeciales").hide();
                                                if ($("#TiposBasicos").val() && $("#TiposBasicos").val().length > 0) $(".mostrarTiposBasicos").show(); else $(".mostrarTiposBasicos").hide();
                                                // dentro del div de configurarLesion, ocultar campos N/A
                                            });
                                    });
                            });
                    });
            });
    }
})