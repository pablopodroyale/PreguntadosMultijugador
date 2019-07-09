var puntoEntregaId;
var preArticuloId;

$('[punto-entrega-elegir]').click(function () {
    preArticuloId = $(this).attr('punto-entrega-elegir');
    $('#modal-delivery-point').modal('show');
});

$('[punto-entrega-modificar]').click(function () {
    puntoEntregaId = $(this).attr('punto-entrega-modificar');
    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: core + '/Pedido/InsertarPuntoEntregaPedidoInicial/?puntoEntregaId=' + puntoEntregaId,
        success: function (data) {
            location.href = location.href + "?articuloId=" + preArticuloId;
        },
        error: function (ex) {
            console.log(ex)
        },
    });
});