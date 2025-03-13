var ClienteData = [];
var Cobros = function () {

    var seleccionarCliente = function () {
        var $urlCliente = Cobros.searchCustomerUrl;
        var $divElement = $("#divListaClientes");

        blockUI($("body"));
        toastr.warning("Cargando clientes...");

        var searchTerm = $("#IdCliente").val();
        var url = $urlCliente + "?filtroBusqueda=&textoBusqueda=" + searchTerm;

        $.get(url, function (data) {
            var $divElement = $("#divListaClientes");
            unblockUI($("body"));
            toastr.clear();

            var $divBody = $("#divListaBody");

            $divBody.empty();

            if (data) {
                $divBody.html(data);
            }

            $divElement.modal({
                "focus": "true",
                "show": "true"
            });
        });

    };

    var setReceivableHandlers = function () {

        // Cuando un checkbox es seleccionado se configura para establecer el valor del saldo total:
        var selectallbox = $("#selectAllReceivables")
        var checkboxes = $(".check-cuota-selected");
        var valueboxes = $(".text-valor-pagado");

        for (var i = 0; i < checkboxes.length; i++) {
            var chkbx = $(checkboxes[i]).on("change click keyup", checkedHandler);
        }

        valueboxes.maskAsDecimal();
        valueboxes.on("change paste keyup", valueChangedHandler);
        valueboxes.val("0.00");
        // Ahora configuramos el boton seleccionar todo
        selectallbox.on("change keyup", selectAllHandler);

    }

    var valueChangedHandler = function () {
        var prefix = this.id.replace("valorPagado_", "");

        var valor = getNumericSafe($("#valorCuota_" + prefix).text());
        var pagado = getNumericSafe(this.value);

        if (pagado > valor) {
            pagado = valor;
        }

        if (pagado < 0) {
            pagado = 0;
        }

        var saldo = valor - pagado;
        var selectedCheckBox = $("#selected_" + prefix);
        var paymentTextBox = $("#valorPagado_" + prefix);
        var balanceTextBox = $("#saldoCuota_" + prefix);

        if (selectedCheckBox.length > 0) {
            selectedCheckBox[0].checked = (pagado && pagado > 0);
        }

        (paymentTextBox.length > 0) && paymentTextBox.val(pagado.toFixed(2));
        (balanceTextBox.length > 0) && balanceTextBox.text(saldo.toFixed(2))

        calculate();
    };

    /**
     * Devuelve un valor numerico seguro
     * @param {any} text
     */
    var getNumericSafe = function (text) {
        var number = 0.00;
        if (text) {
            if (typeof text == 'string') {
                text = text.replace("$", "");
                text = text.replace(",", "");
                text = text.replace(" ", "");
                var val = text * 1;
                if (!isNaN(val)) {
                    number = val;
                }
            }
        }

        return number;
    }


    var calculate = function () {
        // Calculo los totales:
        var valueboxes = $(".text-valor-pagado");
        var total = 0.00;
        var tsaldo = 0.00;
        for (var i = 0; i < valueboxes.length; i++) {
            total = total + getNumericSafe(valueboxes[i].value);
            tsaldo = tsaldo + getNumericSafe($("#saldoCuota_" + i).text());
        }

        $(".total").text(total.toFixed(2));
        $(".saldo").text(tsaldo.toFixed(2));


        // Ahora calculamos los pagos:
        var pagoboxes = $(".text-pago-valor");
        var tpago = 0.00;
        for (var i = 0; i < pagoboxes.length; i++) {
            tpago = tpago + getNumericSafe(pagoboxes[i].value);
        }

        // Actualizo el valor del primer cuadrante.
        if (pagoboxes.length == 1) {
            $(pagoboxes[0]).val(total.toFixed(2));
            tpago = total;
        }


        $(".total-pagos").text(tpago.toFixed(2));
        $("#totalRecibo").val(total);
        $("#totalPagos").val(tpago);
    };


    var checkedHandler = function () {
        var prefix = this.id.replace("selected_", "");

        var valor = "0.00";
        if (this.checked) {
            valor = $("#valorCuota_" + prefix).text();
        }

        $("#valorPagado_" + prefix).val(valor);
        $("#valorPagado_" + prefix).trigger("change");
    };

    var selectAllHandler = function () {
        var checkboxes = $(".check-cuota-selected");

        // Actualizo la seleccion para todos
        for (var i = 0; i < checkboxes.length; i++) {
            checkboxes[i].checked = this.checked;
            $(checkboxes[i]).val(this.checked);
            $(checkboxes[i]).trigger("change");
        }
    };

    var selectClientHandler = function ($baseUrl, $customer) {
        var $data = JSON.parse($customer);
        var $loadReceivablesUrl = $baseUrl + "/" + $data.Id;

        localStorage.setItem("client", $customer);
        setClientData($data);
        clienteData = $data;

        // Cargar las cuotas pendientes del cliente:

        var $divCuotas = $("#divConsultasCuotas");
        $divCuotas.find("tbody").empty();

        // Recalculamos los valores si habia:
        calculate();

        toastr.warning("Cargando datos... por favor espere...");
        blockUI($divCuotas); //, "Cargando...".padStart(10, " "));
        $divCuotas.addClass('loading');
        //$divCuotas.append('<div id="divLoading" class="overlay"><i class="fa fa-refresh fa-spin"></i></div>');

        $.get($loadReceivablesUrl, function (data) {
            var $divCuotas = $("#divConsultasCuotas");

            unblockUI($divCuotas);
            $divCuotas.removeClass('loading');
            $divCuotas.empty();

            if (data) {
                $divCuotas.html(data).ready(setReceivableHandlers);
                $(".input-money").maskAsDecimal();

                // Realiza el calculo de los valores existentes
                calculate();
            }

            $divCuotas.fadeIn();
        });
    }


    function setClientData(client) {
        var ciudad = (client.Ciudad != '' && client.Ciudad != null) ? client.Ciudad.Nombre : "No disponible";
        var correo = (client.Correo) ? client.Correo : "No disponible";
        var direccion = `Dirección: ${client.Direccion} | Ciudad: ${ciudad}`;
        var telefono = ((client.Telefono) ? client.Telefono + " " : "") + ((client.Celular) ? client.Celular : "");

        $("#IdCliente").val(client.Id);
        $("#IdCliente").removeClass('input-validation-error').addClass("valid").attr("aria-invalid", "false");
        $("#IdCliente").next().empty();

        $(".cliente-Identificacion").text(client.Identificacion);
        $(".cliente-RazonSocial").text(client.RazonSocial);
        $(".cliente-Direccion").text(direccion);
        $(".cliente-Telefono").text(telefono);
        $(".cliente-Correo").text(correo);

        $("#clienteIdentificacion").val(client.Identificacion);
        $("#clienteRazonSocial").val(client.RazonSocial);
        $("#clienteTelefono").val(telefono);
        $("#clienteCorreo").val(client.Correo);
        $("#clienteDireccion").val(direccion);

        $("#clientResult").removeClass('hidden').fadeIn('fast');
    }


    var paymentIndex = 0;

    var addPayment = function () {
        var html =
            "<tr>" +
            "<td><a href='javascript:;' class='btn btn-sm btn-outline btn-red delete-payment' id='deletePayment_" + paymentIndex + "' ><i class='entypo-trash'></i></a></td>" +
            "<td>" + getPaymentList() + "</td>" +
            "<td>" + getPaymentComent() + "</td>" +
            "<td>" + getPaymentValue() + "</td>" +
            "</tr>";

        $("#paymentBody").append(html);

        $("#deletePayment_" + paymentIndex).on("click", function () {
            if ($(".delete-payment").length == 1) {
                toastr.error("Se requiere al menos una forma de pago!", "", optionsToastr);
                return;
            }
            var me = this;
            var trElement = me.parentElement.parentElement
            trElement.remove();
        });

        var paymentTextBox = $("#ValorPago_" + paymentIndex);

        paymentTextBox.maskAsDecimal();
        paymentTextBox.on("change paste keyup", paymentChangedHandler);
        paymentTextBox.val("0.00");

        paymentIndex++;
    }

    var changing = false;
    var paymentChangedHandler = function () {
        if (changing) {
            return;
        }

        changing = true;

        var prefix = this.id.replace("ValorPago_", "");

        var total = getNumericSafe($("#tablaDetalleCompra .total").text());
        var pagado = getNumericSafe(this.value);

        var pagos = $(".text-pago-valor")
        var otherpay = 0.00;
        for (var i = 0; i < pagos.length; i++) {
            if (pagos[i].id != this.id) {
                otherpay += getNumericSafe(pagos[i].value);
            }
        }

        var saldo = (total - otherpay);

        if (pagado > saldo) {
            pagado = saldo;
        }

        if (pagado < 0) {
            pagado = 0;
        }

        this.value = pagado.toFixed(2);

        calculate();

        var tpago = getNumericSafe($(".total-pagos").text());
        total = getNumericSafe($("#tablaDetalleCompra .total").text());

        var stotal = total - tpago;
        $(".saldo-total").text(stotal.toFixed(2))

        if (tpago > total) {
            // mensaje
            toastr.error("Los pagos no son iguales al total del recibo!", "Advertencia", optionsToastr);
        }

        changing = false;

    }

    var getPaymentValue = function () {
        var html = "<input class='form-control text-right text-pago-valor input-money-2'  type='text' id='ValorPago_" + paymentIndex + "' name='Pagos[" + paymentIndex + "].Valor' value='0.00' />";

        return "<div class='input-group'><span class='input-group-addon'>$ </span>" + html + "</div>"
    }

    var getPaymentComent = function () {
        var html = "<input class='form-control' type='text' id='ComentaPago_" + paymentIndex + "' name='Pagos[" + paymentIndex + "].Descripcion' value='' />";

        return html
    }

    var getPaymentList = function () {
        var medios = Cobros.paymentMethods;

        var optionHtml = "";
        for (var i = 0; i < medios.length; i++) {
            optionHtml = optionHtml + "<option value='" + medios[i].Id + "'>" + medios[i].MedioPago + "</option>";
        }

        return "<select class='form-control' id='MetodoPago_" + paymentIndex + "' name='Pagos[" + paymentIndex + "].IdMedioPago'>" + optionHtml + "</select>";
    }

    var handlePayments = function () {
        $(".agregar-pago").on("click", addPayment);
        addPayment();
    }


    var handleValidation = function () {
        var $form = $("#guardarCobrosForm");
        $.validator.unobtrusive.parse($form);


        $form.validate({
            ignore: ":hidden",
            rules: {
                IdCliente: {
                    required: true,
                    min: 1
                },
                RazonSocial: "required",
                FechaEmision: "required",
                Detalles: {
                    required: true,
                    minlength: 1
                },
                Pagos: {
                    required: true,
                    minlength: 1
                },
                Total: {
                    required: true,
                    equalTo: "#totalPagos"
                }
            },
            messages: {
                IdCliente: "Por favor seleccione un cliente",
                FechaEmision: "Debe seleccionar una fecha de emision",
                Detalles: {
                    required: "Debe seleccionar algun valor como detalle",
                    minlength: "Debe realizar por lo menos el pago de alguna cuota"
                },
                Pagos: {
                    required: "Debe seleccionar un pago",
                    minlength: "Debe seleccionar al menos un metodo de pago"
                }
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                // Add the `help-block` class to the error element
                error.addClass("help-block");

                // Add `has-feedback` class to the parent div.form-group
                // in order to add icons to inputs
                element.parents("div").addClass("has-feedback");

                if (element.prop("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else {
                    error.insertAfter(element);
                }

                // Add the span element, if doesn't exists, and apply the icon classes to it.
                if (!element.next("span")[0]) {
                    $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
                }
            },
            success: function (label, element) {
                // Add the span element, if doesn't exists, and apply the icon classes to it.
                if (!$(element).next("span")[0]) {
                    $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
                }
            },
            highlight: function (element, errorClass, validClass) {
                $(element).parents("div").addClass("has-error").removeClass("has-success");
                $(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents("div").addClass("has-success").removeClass("has-error");
                $(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
            },
            submitHandler: function (form) {
                // do other things for a valid form
                saveReceipt();
            }
        });

    }

    var beforeSubmitHandler = function (event) {
        event.preventDefault();

        // Evitar múltiples clics deshabilitando el botón
        $(".finish button").prop("disabled", true);

        //removing maskmoney
        $(".input-money").each(function () {
            var valorUnmasked = $(this).toFloat();
            $(this).val(valorUnmasked);
        });

        var $form = $("#guardarCobrosForm");
        if ($form.validate()) {
            saveReceipt();
        }
    };

    var saveReceipt = function () {
        $form = $("#guardarCobrosForm");
        blockUI($form)
        $form.addClass('loading');

        $.post(Cobros.saveReceiptUrl, $form.serialize())
            .done(function (respuesta) {

                if (respuesta) {

                    unblockUI($form)
                    $form.removeClass('loading');

                    $(".finish button").prop("disabled", false);

                    if (respuesta.success) {

                        $("#receiptId").val(respuesta.result.Id)

                        // Actualizo la vista preliminar
                        previewHandler();

                        $("ul.pager").hide()

                        toastr.success(respuesta.message, "", optionsToastr);

                    } else {
                        toastr.error(respuesta.message, "", optionsToastr);
                    }
                }
                else {
                    unblockUI($form)
                    $form.removeClass('loading');

                    $(".finish button").prop("disabled", false);
                    toastr.error("Hubo un error al generar el recibo!", "", optionsToastr);
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                // En caso de error, habilitar el botón nuevamente
                $(".finish button").prop("disabled", false);
                toastr.error("Algo salió mal!", "", optionsToastr);
            });

    }

    var previewHandler = function () {
        $form = $("#guardarCobrosForm");

        $.post(Cobros.previewReceiptUrl, $form.serialize())
            .done(function (respuesta) {
                $("#tab3").empty().html(respuesta);

                if ($("#tab3").hasClass("active")) {
                    $(".finish").show();
                    $(".next").hide();
                }
            });


    };

    return {
        searchControl: {
            url: '',
            loadingContainer: '',
            resultContainer: ''
        },

        paymentMethods: [],

        searchCustomerUrl: '',
        loadReceivablesUrl: '',
        saveReceiptUrl: '',
        previewReceiptUrl: '',

        onSelectCustomer: function (customer) {
            selectClientHandler(Cobros.loadReceivablesUrl, customer);
        },

        configure: function () {
            var $wizard = $("#guardarCobrosForm").formwizardcustom();
            $(".datepicker").attr("data-format", "yyyy-mm-dd");

            $(".datepicker").datepickercustom();
            $("#clientResult").on("dblclick", seleccionarCliente)
            $("#btnBuscarCliente").on("click", seleccionarCliente);
            $("#guardarCobrosForm").on("submit", beforeSubmitHandler);
            $(".next").on("click", previewHandler);
            $(".previous").on("click", function () {
                $(".finish").hide();
                $(".next").show();
            });

            handlePayments();
            handleValidation();

        }
    };

}();



