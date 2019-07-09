$(document).ready(function () {

    var formulario = $('form.formulario-paginado');

    if (formulario.length == 0)
        formulario = $('form');

    var hfAccionSubmit = $('#AccionSubmit');
    var hfPagina = $('#Pagina');
    $('a[btn-buscar]').click(function () {

        if ($(this).parent('li.disabled').length >= 1)
            return;

        var valor = $(this).attr('btn-buscar');

        hfAccionSubmit.val(valor);

        switch (valor)
        {
            case 'buscar':
                break;
            case 'anterior':
                var pagina = parseInt(hfPagina.val()) - 1;
                hfPagina.val(pagina);
                break;
            case 'siguiente':
                var pagina = parseInt(hfPagina.val()) + 1;
                hfPagina.val(pagina);
                break;
            default:
                hfPagina.val(valor);
                break;
        }

        formulario.submit();
    });
});