Dropzone.options.dropzoneJsForm = {
    maxFilesize: 1024,
    maxFiles: 1,
    acceptedFiles: "image/jpeg,image/png,image/gif",
    dictDefaultMessage: "Haga click para seleccionar el archivo a subir",
    init: function () {
        myDriozone = this;
        myDriozone.on("complete", function (data) {
            var res = JSON.parse(data.xhr.responseText);
            //$("#cerrar").on("click", function () {
            //    $('#modalCargaMasiva').modal('hide');
            //    myDriozone.removeFile(data);

            //})
            //res.Message devolución de mensajes dropzone
            //data.name nombre archivo subido

            //refrescarGrillaSmsEdit();
            //
        });

        myDriozone.on("success", function (data, response, xmlrequest) {
            //refrescarGrillaSmsEdit();
            $('#modalCargaMasiva').modal('hide');
            //myDriozone.removeFile(data);
            $(".data-table-archivos").bootgrid("append", [response]);
        })

        myDriozone.on("error", function (nose, error, xmlrequest) {
            console.log(error);
            console.log(nose);
            //$("#linkExcelError").show();
            //$("#btnLimpiarArchivos").show();
        })

        myDriozone.on("sending", function (file, xhr, formData) {
            //formData.append("TiendaId", $("#TiendaExcelId").val());
        });
    },
}