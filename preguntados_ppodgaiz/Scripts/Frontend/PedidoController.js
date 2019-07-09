$(document).ready(function () {
    var articuloId;

    $('[agregar-articulo]').click(function () {
        articuloId = $(this).attr('agregar-articulo');
        abrirModalArticulo(articuloId);
    });

    $('[aniadir-cantidad-valor]').click(function () {
        Aniadir($(this).attr('aniadir-cantidad-valor'));
    });

    $('[aniadir-articulo]').click(function () {
        Aniadir($('[aniadir-cantidad]').val());
    });

    $('[aniadir-articulo-mobile]').click(function () {
        Aniadir($('[aniadir-cantidad-mobile]').val());
    });

    $('[fecha-pedido]').change(function () {
        var thisClick = $(this).find('option:selected').val().split('|');
        var fecha = thisClick[1] + '/' + thisClick[0] + '/' + thisClick[2];

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + '/pedido/ModificarFechaPedidoInicial/?fecha=' + fecha,
            success: function (result) {

                if (result.Mensaje.AplicaAlertaAumento)
                {
                    location.href = location.origin + location.pathname;
                }
            },
            error: function (ex) {
                console.log(ex)
            },
        });
    });

});

var abrirModalArticulo = function (articuloId) {

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: core + '/Articulo/ObtenerPorCliente/?id=' + articuloId + "&clienteId=" + clienteId,
        success: function (data) {
            var articulo = data.Mensaje;
            $('[articulo-nombre]').html(articulo.Nombre);
            $('[articulo-precio-unitario]').html('$' + articulo.PrecioUnitario);
            $('[articulo-cantidad-unidades]').html(articulo.CantidadUnidades);
            $('[articulo-precio]').html('$' + articulo.Precio);
            $('[aniadir-imagen]').attr('src', articulo.ImagenPrincipal);
            $('[aniadir-articulo-id]').val(articuloId);

            $('input[type="radio"]').each(function () {
                $(this).removeAttr('checked');
            });
            $('[cantidad-uno]').attr('checked', 'checked');
            $('[aniadir-cantidad]').val('');
            $('#addCart').modal('show');
        },
        error: function (ex) {
            console.log(ex)
        },
    });
}

var Aniadir = function (cantidad) {

    var articuloId = $('[aniadir-articulo-id]').val();

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: core + '/pedido/AgregarArticuloPedidoInicial/?articuloId=' + articuloId + "&cantidad=" + cantidad,
        success: function (result) {
            ModificarCantidadCarrito(cantidad);
            refrescarPrecioFinalCompra();
            refrescarTablaArticulos();
            $('#addCart').modal('hide');
            swal({
                title: "Su producto ha sido agregado",
                icon: "success",
                timer: 2000
            });
        },
        error: function (ex) {
            console.log(ex)
        },
    });
}

var ModificarCantidadCarrito = function (cantidad) {

    var carrito = $('[articulos-pedido-inicial]');
    var cantidadArticulos = carrito.attr('articulos-pedido-inicial');
    var cantidadTotal = parseInt(cantidadArticulos) + parseInt(cantidad);

    carrito.attr('articulos-pedido-inicial', cantidadTotal);
    carrito.html(cantidadTotal);

    if (cantidadTotal == 0)
        carrito.addClass('hide');
    else
        carrito.removeClass('hide');
}

var refrescarPrecioFinalCompra = function () {
    var control = $('[precio-final-compra]');

    if (control.length == 0)
        return;

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: core + '/pedido/_PrecioFinalCompra',
        success: function (result) {
        },
        error: function (ex) {
            control.html(ex.responseText);

            console.log(ex)
        },
    });
}

var refrescarTablaArticulos = function () {
    var control = $('[item-tabla-articulo]');

    if (control.length == 0)
        return;

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: core + '/pedido/_ItemTablaArticulo',
        success: function (result) {
        },
        error: function (ex) {
            control.html(ex.responseText);
            console.log(ex)
        },
    });
}

$(document).ready(function () {
    if (location.href.indexOf('articuloId=') > 0) {
        abrirModalArticulo(getUrlVars()["articuloId"]);
    }
});

