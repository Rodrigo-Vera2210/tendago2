 
$.fn.datepickercustom = function () {
    var $this = $(this),
        opts = {
            language: attrDefault($this, 'language', 'es'),
            format: attrDefault($this, 'format', 'dd/mm/yyyy'),
            startDate: attrDefault($this, 'startDate', ''),
            endDate: attrDefault($this, 'endDate', ''),
            daysOfWeekDisabled: attrDefault($this, 'disabledDays', ''),
            startView: attrDefault($this, 'startView', 0),
            rtl: rtl()
        },
        $n = $this.next(),
        $p = $this.prev();

    $this.datepicker(opts);

    if ($n.is('.input-group-addon') && $n.has('a')) {
        $n.on('click', function (ev) {
            ev.preventDefault();

            $this.datepicker('show');
        });
    }

    if ($p.is('.input-group-addon') && $p.has('a')) {
        $p.on('click', function (ev) {
            ev.preventDefault();

            $this.datepicker('show');
        });
    }
};

$.fn.timepickercustom = function () {
    var $this = $(this),
        opts = {
            template: attrDefault($this, 'template', false),
            showSeconds: attrDefault($this, 'showSeconds', false),
            defaultTime: attrDefault($this, 'defaultTime', 'current'),
            showMeridian: attrDefault($this, 'showMeridian', false),
            minuteStep: attrDefault($this, 'minuteStep', 15),
            secondStep: attrDefault($this, 'secondStep', 15)
        },
        $n = $this.next(),
        $p = $this.prev();

    $this.timepicker(opts);

    if ($n.is('.input-group-addon') && $n.has('a')) {
        $n.on('click', function (ev) {
            ev.preventDefault();

            $this.timepicker('showWidget');
        });
    }

    if ($p.is('.input-group-addon') && $p.has('a')) {
        $p.on('click', function (ev) {
            ev.preventDefault();

            $this.timepicker('showWidget');
        });
    }
};

$.fn.formwizardcustom = function () {
    var $this = $(this),
        $progress = $this.find(".steps-progress div"),
        _index = $this.find('> ul > li.active').index();

    // Validation
    var checkFormWizardValidaion = function (tab, navigation, index) {
        if ($this.hasClass('validate')) {

            var $validator = $this.validate();
            var $valid = $this.valid();

            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
        }
        return true;
    };

    $this.bootstrapWizard({
        tabClass: "",
        onTabShow: function ($tab, $navigation, index) {
            var $total = $navigation.find('li').length;
            var $current = index + 1;
            setCurrentProgressTab($this, $navigation, $tab, $progress, index);
            if ($total === $current) {
                $('.wizard li.finish button').prop('disabled', false);
                $('.wizard li.finish button').removeClass('disabled');

                $('.wizard li.finish').prop('disabled', false);
                $('.wizard li.finish').removeClass('disabled');
            }
            else {
                $('.wizard li.finish button').prop('disabled', true);
                $('.wizard li.finish button').addClass('disabled');

                $('.wizard li.finish').prop('disabled', true);
                $('.wizard li.finish').addClass('disabled');
            }
        },
        onNext: checkFormWizardValidaion,
        onTabClick: function ($tab, $navigation, index) {
            return false;
        },
    });

    $this.data('bootstrapWizard').show(_index);
};
 
$.fn.spinnercustom = function () {

    return this.each(function (i, el) {
        var $this = $(el),
            $minus = $this.find('button:first'),
            $plus = $this.find('button:last'),
            $input = $this.find('input'),

            minus_step = attrDefault($minus, 'step', -1),
            plus_step = attrDefault($minus, 'step', 1),

            min = attrDefault($input, 'min', null),
            max = attrDefault($input, 'max', null);


        $this.find('button').on('click', function (ev) {
            ev.preventDefault();

            var $this = $(this),
                val = $input.val(),
                step = attrDefault($this, 'step', $this[0] == $minus[0] ? -1 : 1);

            if (!step.toString().match(/^[0-9-\.]+$/)) {
                step = $this[0] == $minus[0] ? -1 : 1;
            }

            if (!val.toString().match(/^[0-9-\.]+$/)) {
                val = 0;
            }

            $input.val(parseFloat(val) + step).trigger('keyup');
        });

        $input.keyup(function () {
            if (isNaN(parseFloat($input.val()))) {
                $input.val('0');
            }

            if (min != null && parseFloat($input.val()) < min) {
                $input.val(min);
            }
            else {
                if (max != null && parseFloat($input.val()) > max) {
                    $input.val(max);
                }
            }
        });
    });
};

/**
 * Creates a new Custom Select2 Control
 * @param {any} el
 * @param {string} url
 * @param {string} pl
 */
$.fn.select2Custom = function () {
    if ($.fn.select2 == null || $.fn.select2 == 'undefined') {
        console.error("Debe configurar select2 para poder utilizar esta función.");
        return false;
    }

    var $controls = $(this);
     
    for (var i = 0; i < $controls.length; i++) {
        var $control = $($controls[i]);

        var $url = $control.attr("data-action");
        var $plh = $control.attr("placeholder");

            $control.select2({
                ajax: {
                    url: $url,
                    type: "GET",
                    dataType: 'json',
                    data: function (params) {
                        return params;
                    },
                    cache: true
                },
                allowClear: true,
                placeholder: $plh,
                minimumInputLength: 1 
            });
        
    }
    
}

 /**
  * Convert String to Float
  * @param {String} text
  */
$.fn.toFloat = function (precision) {
    var text = this;

    var precision = precision || 2;

    if (this.length && this.length > 0 && typeof this[0] != 'object') {
        text = this[0];
    }

    var value = 0.00;

    if (typeof text == 'object')
        if (typeof text.value == 'string' || typeof text.value == 'number') {
            text = text.value;
        }
        else if (typeof text.val !== 'undefined') {
            text = text.val();
        }


    if (typeof text == 'number') {
        value = text;
    }
    else if (typeof text !== 'undefined') {
        text = text.toString();

        if (typeof text == 'string') {

            // Analizo si es un numero en especial:
            if (text.lastIndexOf(".") > 0) {

                while (text.lastIndexOf(",") > 0) {
                    text = text.replace(",", "");
                }
            }

            if (text.split(",").length > 2) {
                text = text.replace(",", "");
            }
            else {
                text = text.replace(",", ".");
            }


            // Elimino del String los caracteres especiales que contenga
            var mth = text.match(/[-?\d\.]+/);
            value = parseFloat(mth);
        }
    }

    if (isNaN(value)) {
        value = 0.00;
    }

    value = value.toFixed(precision);

    return parseFloat(value);
}


/* Demo purposes only */
$(".shopping-item.hover").mouseleave(
    function () {
        $(this).removeClass("hover");
    }
);
 


var isValid = function (result) {
    if (result) {
        if (result == '' || (result.includes && result.includes("login-page"))) {
            
            toastr.error("Debe volver a iniciar sesión para continuar...", "", optionsToastr);
            window.open(location.origin, "_login");
            return false;
        }
    }
    else {
        return false;
    }

    
    return true;
}

function select2_search($el, term) {
    $el.select2('open');

    // Get the search box within the dropdown or the selection
    // Dropdown = single, Selection = multiple
    var $search = $el.data('select2').dropdown.$search || $el.data('select2').selection.$search;
    // This is undocumented and may change in the future

    $search.val(term);
    $search.trigger('input');
    $el.select2('close');

}

$('#divModal').on('shown.bs.modal', function () {
    $('input:text:visible:first').focus();
})  

$(document).ready(function () {
    // Controla los valores de los checkboxes --fix bug for mvc checkboxes by dlanorok
    $("input[type='checkbox']").on("change", function () {
        var $me = $(this);
        var $name = $me.prop("name");
        var $check = $me.is(":checked");

        $("[name='" + $name + "']").val($check);
    });

    if ($.validator) {
        var opts = {

            required: $.validator.format("El campo {0} es requerido."),
            remote: "Debe corregir este campo.",
            email: "Debe ingresar una dirección de correo válida",
            url: "Debe ingresar una URL válida.",
            date: "Debe ingresar una fecha válida.",
            dateISO: "Debe ingresar una fecha (ISO) válida.",
            number: "Debe ingresar un número entero válido.",
            digits: "Debe ingresar sólo números.",
            creditcard: "Debe ingresar un número de tarjeta válido.",
            equalTo: "Debe ingresar el mismo valor de nuevo.",
            accept: "Debe ingresar un archivo con una extensión aceptada.",
            maxlength: $.validator.format("Debe ingresar máximo {0} caracteres."),
            minlength: $.validator.format("Debe ingresar mínimo {0} caracteres."),
            rangelength: $.validator.format("Debe ingresar un valor entre {0} y {1} caracteres."),
            range: $.validator.format("Debe ingresar un valor entre {0} y {1}."),
            max: $.validator.format("Debe ingresar un valor menor o igual a {0}."),
            min: $.validator.format("Debe ingresar un valor mayor o igual a {0}."),
            step: $.validator.format("Debe ingresar un multiplo de {0}.")

        };

        $.extend($.validator.messages, opts);
    }
});

var fixedEncodeURI = function (str) {
    return encodeURIComponent(str).replace(/[!'()]/g, escape).replace(/\*/g, "%2A");
}


var isNumber = function(evt, element) {

    var charCode = (evt.which) ? evt.which : event.keyCode

    if (
        (charCode != 45 || $(element).val().indexOf('-') != 1) && // “-” CHECK MINUS, AND ONLY ONE.
        (charCode != 46 || $(element).val().indexOf('.') != 1) && // “.” CHECK DOT, AND ONLY ONE.
        (charCode < 48 || charCode > 57))
        return false;

    return true;
}