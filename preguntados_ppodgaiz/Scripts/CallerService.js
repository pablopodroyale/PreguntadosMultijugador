$(document).ready(function () {
    $("[caller-method]").on('click', callerMethod);
    $("[caller-method-get]").on('click', callerMethodGet);
    $("[caller-method-change]").on('change', callerMethodChange);
});


var callerMethodInServer = false;
var callerMethod = function () { }

var callerMethodGetInServer = false;
var callerMethodGet = function () { }

var callerMethodChangeInServer = false;
var callerMethodChange = function () {
    // La variable "callerMethodChangeInServer" tiene la utilidad de no llamar al servidor en paralelo.
    if (!callerMethodChangeInServer) {
        callerMethodChangeInServer = true;

        var method = $(this).attr('caller-method-change');
        var parametro1 = $(this).attr('caller-parameter-name');
        var control = $(this).attr('caller-control');
        var valor1 = $(this).val();
        
        limpiarControl(control);

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + method + '?' + parametro1 + '=' + valor1,
            success: function (data) {

                callerMethodChangeInServer = false;
            },
            error: function () {
                callerMethodChangeInServer = false;
            },
        });
    }
}

// FUNCIONES SUCCESS \\
