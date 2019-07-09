// BASICAS
function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
}
function guid() {
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

function Convertir(lesion) {
    var result = {
        Id : lesion.Id,
        Nombre : lesion.Nombre,
        NroLesion : lesion.NroLesion,
        BiradsActual : lesion.BiradsActual,
        BiradsAnterior: lesion.BiradsAnterior,
        TipoGuia: typeof lesion.TipoGuia == "object" ? lesion.TipoGuia : JSON.parse(lesion.TipoGuia),
        TipoProcedimiento: typeof lesion.TipoProcedimiento == "object" ? lesion.TipoProcedimiento : JSON.parse(lesion.TipoProcedimiento),
        Ubicacion: typeof lesion.Ubicacion == "object" ? lesion.Ubicacion : JSON.parse(lesion.Ubicacion),
        Masa: typeof lesion.Masa == "object" ? lesion.Masa : JSON.parse(lesion.Masa),
        Calcificacion: typeof lesion.Calcificacion == "object" ? lesion.Calcificacion : JSON.parse(lesion.Calcificacion),
        NoMasa: typeof lesion.NoMasa == "object" ? lesion.NoMasa : JSON.parse(lesion.NoMasa),
        Asimetria: typeof lesion.Asimetria == "object" ? lesion.Asimetria : JSON.parse(lesion.Asimetria),
        HallazgoAsociado: typeof lesion.HallazgoAsociado == "object" ? lesion.HallazgoAsociado :  JSON.parse(lesion.HallazgoAsociado),
        CasoEspecial: typeof lesion.CasoEspecial == "object" ? lesion.CasoEspecial : JSON.parse(lesion.CasoEspecial),
        Curva: typeof lesion.Curva == "object" ? lesion.Curva : JSON.parse(lesion.Curva),
        TiposBasicos: typeof lesion.TiposBasicos == "object" ? lesion.TiposBasicos : JSON.parse(lesion.TiposBasicos),
        ObservacionesDuranteProcedimiento : lesion.ObservacionesDuranteProcedimiento,
        Archivos: typeof lesion.Archivos == "object" ? lesion.Archivos : JSON.parse(lesion.Archivos),
        Cierre: typeof lesion.Cierre == "object" ? lesion.Cierre : JSON.parse(lesion.Cierre),
        ResultadoIntervencionismo: typeof lesion.ResultadoIntervencionismo == "object" ? lesion.ResultadoIntervencionismo : JSON.parse(lesion.ResultadoIntervencionismo)
    }
    return result;
}

// VARIABILIZAR CAMPOS
function mostrarOcultarCampos() {
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
    });
}
function mostrarOcultarMargenes() {
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
        });
}
function mostrarOcultarProcedimientos() {
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
        });
}
function mostrarOcultarTiposLesion() {
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
        });
}
function mostrarOcultarHallazgos() {
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
        });
}
function mostrarOcultarDatos() {
    $(".chosen").trigger("chosen:updated");
    if ($("#Guia").val() != "") $("#ConfigurarLesiones").show(); else $("#ConfigurarLesiones").hide();
    mostrarOcultarCampos();
    mostrarOcultarMargenes();
    mostrarOcultarHallazgos();
    mostrarOcultarTiposLesion();
}

function activarDesactivarCuadrante() {
    if ($("#Hora").val() != "") {
        $("#Cuadrante").val("");
        $("#Cuadrante").attr("disabled", "").trigger("chosen:updated");
    } else {
        $("#Cuadrante").removeAttr("disabled").trigger("chosen:updated");    }
}
function activarDesactivarHora() {
    if ($("#Cuadrante").val() != "") {
        $("#Hora").val("");
        $("#Hora").attr("disabled", "").trigger("chosen:updated");
    } else {
        $("#Hora").removeAttr("disabled").trigger("chosen:updated");
    }
}

// MANEJO DEL FORMULARIO
function hayMasa() {
    var result = false;
    if ($('select[id="Guia"] option:selected').text() == "Mamografica") {
        result |= $("#Forma").val() != "";
        result |= $("#MargenAgrupado").val() != "";
        result |= $("#Densidad").val() != "";
    }
    if ($('select[id="Guia"] option:selected').text() == "Ecografica") {
        result |= $("#Forma").val() != "";
        result |= $("#Orientacion").val() != "";
        result |= $("#MargenAgrupado").val() != "";
        result |= $("#PatronEcografico").val() != "";
        result |= $("#ComportamientoPosterior").val() != "";
    }
    if ($('select[id="Guia"] option:selected').text() == "RMI") {
        result |= $("#Forma").val() != "";
        result |= $("#MargenAgrupado").val() != "";
        result |= $("#RealceInterno").val() != "";
    }
    return result;
}
function hayCalcificacion() {
    var result = false;
    if ($('select[id="Guia"] option:selected').text() == "Mamografica") {
        result |= $("#TipicamenteBenignas").val() != "";
        result |= $("#MorfologicaSospechosa").val() != "";
        result |= $("#DistribucionCalcificacion").val() != "";
    }
    if ($('select[id="Guia"] option:selected').text() == "Ecografica") {
        result |= $("#TipoCalcificacion").val() != "";
    }
    return result;
}
function hayAsimetria() {
    var result = false;
    if ($('select[id="Guia"] option:selected').text() == "Mamografica") {
        result |= $("#Asimetria").val() != "";
    }
    return result;
}
function hayHallazgo() {
    var result = false;
    if ($('select[id="Guia"] option:selected').text() == "Mamografica" || $('select[id="Guia"] option:selected').text() == "Ecografica") {
        result |= $("#HallazgoAsociado").val() != "";
    }
    return result;
}
function hayCasoEspecial() {
    var result = false;
    if ($('select[id="Guia"] option:selected').text() == "Ecografica") {
        result |= $("#CasoEspecial").val() != "";
    }
    return result;
}
function hayNoMasa() {
    var result = false;
    if ($('select[id="Guia"] option:selected').text() == "RMI") {
        result |= $("#DistribucionNoMasa").val() != "";
        result |= $("#Realce").val() != "";
    }
    return result;
}
function hayCurva() {
    var result = false;
    if ($('select[id="Guia"] option:selected').text() == "RMI") {
        result |= $("#Curva").val() != "";
    }
    return result;
}

function RecuperarDataFormulario(id) {
    var row = {
        Id: id ? id : guid(),
        Nombre: "Lesion",
        NroLesion: $("#NroLesion").val(),
        BiradsActual: $("#BiradsActual").val(),
        BiradsAnterior: $("#BiradsAnterior").val(),
        TipoGuia: { Id: $("#Guia").val() },
        TipoProcedimiento: { Id: $("#TipoProcedimiento").val() },
        Ubicacion: {
            Id: guid(),
            Nombre: "Ubicacion",
            Mama: document.getElementById("MamaEnum.izquierda").checked ? 0 : 1,
            Hora: $("#Hora").val(),
            Cuadrante: $("#Cuadrante").val() == "" ? null : { Id: $("#Cuadrante").val() },
            Profundidad: $("#Profundidad").val() == "" ? null : { Id: $("#Profundidad").val() }
        },
        Masa: !hayMasa() ? null : {
            Id: guid(),
            Nombre: "Masa",
            TipoGuia: { Id: $("#Guia").val() },
            Densidad: { Id: $("#Densidad").val() },
            Forma: { Id: $("#Forma").val() },
            Orientacion: { Id: $("#Orientacion").val() },
            Margen: { Id: $("#MargenAgrupado").val() },
            PatronEcografico: { Id: $("#PatronEcografico").val() },
            ComportamientoPosterior: { Id: $("#ComportamientoPosterior").val() },
            RealceInterno: { Id: $("#RealceInterno").val() }
        },
        Calcificacion: !hayCalcificacion() ? null : {
            Id: guid(),
            Nombre: "Calcificacion",
            TipoGuia: { Id: $("#Guia").val() },
            TipoCalcificacion: { Id: $("#TipoCalcificacion").val() },
            TipicamenteBenigna: $("#TipicamenteBenignas").val(),
            MorfologicaSospechosa: { Id: $("#MorfologicaSospechosa").val() },
            Distribucion: { Id: $("#DistribucionCalcificacion").val() },
        },
        NoMasa: !hayNoMasa() ? null : {
            Id: guid(),
            Nombre: "NoMasa",
            TipoGuia: { Id: $("#Guia").val() },
            PatronRealce: { Id: $("#PatronRealce").val() },
            Distribucion: { Id: $("#DistribucionNoMasa").val() }
        },
        Asimetria: !hayAsimetria() ? null : { Id: $("#Asimetria").val() },
        HallazgoAsociado: !hayHallazgo() ? null : { Id: $("#HallazgoAsociado").val() },
        CasoEspecial: !hayCasoEspecial() ? null : { Id: $("#CasoEspecial").val() },
        Curva: !hayCurva() ? null : { Id: $("#Curva").val() },
        TiposBasicos: !($("#TiposBasicos").val()) ? [] : $("#TiposBasicos").val(),
        ObservacionesDuranteProcedimiento: $("#ObservacionesDuranteProcedimiento").val(),
        Archivos: $(".data-table-archivos").bootgrid("getCurrentRows"),
        Cierre: {
            Id: guid(),
            Nombre: "Cierre",
            Conciliacion: $("#Conciliacion").val(),
            CierreDeHistoriaClinica: $("#CierreDeHistoriaClinica").val(),
            InteresCientifico: document.getElementById("ConInteresCientifico") ? (document.getElementById("ConInteresCientifico").checked == true ? true : false) : null
        },
        ResultadoIntervencionismo: (!($("#ResultadoIntervencionismo").val()) || $("#ResultadoIntervencionismo").val() == "") ? null : { Id: $("#ResultadoIntervencionismo").val() }
    }

    return row;
}
function LlenarFormulario(data, mostrando = false) {
        $("#Formulario").show();
        // Propiedades basicas
        $("#NroLesion").val(data.NroLesion);
        $("#BiradsActual").val(data.BiradsActual);
        $("#BiradsAnterior").val(data.BiradsAnterior);
        var TipoGuia = typeof data.TipoGuia == "object" ? data.TipoGuia : JSON.parse(data.TipoGuia);
        if (TipoGuia) $("#Guia").val(TipoGuia.Id);
        var TipoProcedimiento = typeof data.TipoProcedimiento == "object" ? data.TipoProcedimiento : JSON.parse(data.TipoProcedimiento);
        if (TipoProcedimiento) $("#TipoProcedimiento").val(TipoProcedimiento.Id);

        var Ubicacion = typeof data.Ubicacion == "object" ? data.Ubicacion : JSON.parse(data.Ubicacion);
        if (Ubicacion) {
            if (Ubicacion.Mama == 0) document.getElementById("MamaEnum.izquierda").checked = true
            if (Ubicacion.Mama == 1) document.getElementById("MamaEnum.derecha").checked = true
            $("#Hora").val(Ubicacion.Hora ? Ubicacion.Hora : null);
            $("#Cuadrante").val(Ubicacion.Cuadrante ? Ubicacion.Cuadrante.Id : null);
            $("#MamaEnum.izquierda").prop("checked") ? "MamaEnum.izquierda" : $("#MamaEnum.derecha").prop("checked") ? "MamaEnum.derecha" : null;
            $("#Profundidad").val(Ubicacion.Profundidad ? Ubicacion.Profundidad.Id : null);
            if (!mostrando) {
                activarDesactivarCuadrante();
                activarDesactivarHora();
            }
        }

        // Lesiones parametrizables
        var Masa = typeof data.Masa == "object" ? data.Masa : JSON.parse(data.Masa);
        if (Masa) {
            $("#Densidad").val(Masa.Densidad ? Masa.Densidad.Id : null);
            $("#Forma").val(Masa.Forma ? Masa.Forma.Id : null);
            $("#Orientacion").val(Masa.Orientacion ? Masa.Orientacion.Id : null);
            $("#MargenAgrupado").val(Masa.Margen ? Masa.Margen.Id : null);
            $("#PatronEcografico").val(Masa.PatronEcografico ? Masa.PatronEcografico.Id : null);
            $("#ComportamientoPosterior").val(Masa.ComportamientoPosterior ? Masa.ComportamientoPosterior.Id : null);
            $("#RealceInterno").val(Masa.RealceInterno ? Masa.RealceInterno.Id : null);
        }

        var Calcificacion = typeof data.Calcificacion == "object" ? data.Calcificacion : JSON.parse(data.Calcificacion);
        if (Calcificacion) {
            $("#TipoCalcificacion").val(Calcificacion.TipoCalcificacion ? Calcificacion.TipoCalcificacion.Id : null);
            $("#TipicamenteBenigna").val(Ubicacion.TipicamenteBenigna);
            $("#MorfologicaSospechosa").val(Calcificacion.MorfologicaSospechosa ? Calcificacion.MorfologicaSospechosa.Id : null);
            $("#DistribucionCalcificacion").val(Calcificacion.Distribucion ? Calcificacion.Distribucion.Id : null);
        }

        var NoMasa = typeof data.NoMasa == "object" ? data.NoMasa : JSON.parse(data.NoMasa);
        if (NoMasa) {
            $("#PatronRealce").val(NoMasa.PatronRealce ? NoMasa.PatronRealce.Id : null);
            $("#DistribucionNoMasa").val(NoMasa.Distribucion ? NoMasa.Distribucion.Id : null);
        }

        // Lesiones no parametrizables
        var Asimetria = typeof data.Asimetria == "object" ? data.Asimetria : JSON.parse(data.Asimetria)
        if (Asimetria) $("#Asimetria").val(Asimetria.Id);

        var Hallazgo = typeof data.HallazgoAsociado == "object" ? data.HallazgoAsociado : JSON.parse(data.HallazgoAsociado)
        if (Hallazgo) $("#HallazgoAsociado").val(Hallazgo.Id);

        var CasoEspecial = typeof data.CasoEspecial == "object" ? data.CasoEspecial : JSON.parse(data.CasoEspecial)
        if (CasoEspecial) $("#CasoEspecial").val(CasoEspecial.Id);

        var Curva = typeof data.Curva == "object" ? data.Curva : JSON.parse(data.Curva)
        if (Curva) $("#Curva").val(Curva.Id);

        var TiposBasicos = typeof data.TiposBasicos == "object" ? data.TiposBasicos : JSON.parse(data.TiposBasicos)
        $("#TiposBasicos").val(TiposBasicos);

        var Archivos = typeof data.Archivos == "object" ? data.Archivos : JSON.parse(data.Archivos)
        if (Archivos) $(".data-table-archivos").bootgrid("append", Archivos);

        var Cierre = typeof data.Cierre == "object" ? data.Cierre : JSON.parse(data.Cierre)
        if (Cierre) {
            if (Cierre.InteresCientifico == false) document.getElementById("SinInteresCientifico").checked = true; else document.getElementById("ConInteresCientifico").checked = true;
            $("#Conciliacion").val(Cierre.Conciliacion);
            $("#CierreDeHistoriaClinica").val(Cierre.CierreDeHistoriaClinica);
            $("#ConInteresCientifico").prop("checked") ? "MamaEnum.izquierda" : $("#MamaEnum.derecha").prop("checked") ? "MamaEnum.derecha" : null;
        }

        var ResultadoIntervencionismo = typeof data.ResultadoIntervencionismo == "object" ? data.ResultadoIntervencionismo : JSON.parse(data.ResultadoIntervencionismo)
        if (ResultadoIntervencionismo) $("#ResultadoIntervencionismo").val(ResultadoIntervencionismo.Id);

        $(".chosen").trigger("chosen:updated");
    }

// BOTONES LESION
$("#btnAgregarLesion").on("click", function () {
    $("#btnAgregarLesion").hide();
    $("#tabla-archivos").load("/Archivo/ObtenerArchivos?lesionId=", null, function() {
        $("#Formulario").show();
        $("#ConfigurarLesiones").append()
        $("#ConfigurarLesiones").hide();
        $("#submit").hide()
        $("select").val("");
        document.getElementById("MamaEnum.izquierda").checked = true
        $("#NroLesion").val("");
        $(".chosen").trigger("chosen:updated");
    });

});
$("#btnSaveLesion").on("click", function () {
    $("#btnAgregarLesion").show();
    if ($("#idLesionEditando").val()) {
        $("#data-table-lesiones").bootgrid("remove", [$("#idLesionEditando").val()]);
        $("#data-table-lesiones").bootgrid("append", [RecuperarDataFormulario($("#idLesionEditando").val())]);
    } else {
        var lesion = 1;
        var rows = $("#data-table-lesiones").bootgrid("getCurrentRows");
        if ($("#NroLesion").val()) {
            lesion = parseInt($("#NroLesion").val());
            for (var x = 0; x < rows.length; x++) {
                if (rows[x].NroLesion == lesion) {
                    swal({
                        title: "No se puede crear la lesión con número " + lesion + " dado que ya existe.",
                        type: "error"
                    });
                    return;
                }
            }
        }
        else {
            for (var x = 0; x < rows.length; x++) {
                if (rows[x].NroLesion >= lesion)
                    lesion = rows[x].NroLesion + 1;
            }
        }
        $("#NroLesion").val(lesion);

        var lesionJSON = RecuperarDataFormulario(null);
        var lesionBootgrid = {
            Id : lesionJSON.Id,
            Nombre : lesionJSON.Nombre,
            NroLesion : lesionJSON.NroLesion,
            BiradsActual : lesionJSON.BiradsActual,
            BiradsAnterior : lesionJSON.BiradsAnterior,
            TipoGuia : JSON.stringify(lesionJSON.TipoGuia),
            TipoProcedimiento : JSON.stringify(lesionJSON.TipoProcedimiento),
            Ubicacion : JSON.stringify(lesionJSON.Ubicacion),
            Masa : JSON.stringify(lesionJSON.Masa),
            Calcificacion : JSON.stringify(lesionJSON.Calcificacion),
            NoMasa : JSON.stringify(lesionJSON.NoMasa),
            Asimetria : JSON.stringify(lesionJSON.Asimetria),
            HallazgoAsociado : JSON.stringify(lesionJSON.HallazgoAsociado),
            CasoEspecial : JSON.stringify(lesionJSON.CasoEspecial),
            Curva : JSON.stringify(lesionJSON.Curva),
            TiposBasicos: JSON.stringify(lesionJSON.TiposBasicos),
            ObservacionesDuranteProcedimiento : lesionJSON.ObservacionesDuranteProcedimiento,
            Archivos: lesionJSON.Archivos,
            Cierre: lesionJSON.Cierre,
            ResultadoIntervencionismo: lesionJSON.ResultadoIntervencionismo
        }

        $("#data-table-lesiones").bootgrid("append", [lesionBootgrid]);
    }
    $("#Formulario").hide();
    $("#submit").show();
})
$("#btnCancelLesion").on("click", function () {
    $("#btnCancelLesion").hide();
    $("#idLesionEditando").val(null);
    $("#btnAgregarLesion").show();
    $("#Formulario").hide();
    $("select").val("");
    $("input").val("");
    $("#submit").show();
});

