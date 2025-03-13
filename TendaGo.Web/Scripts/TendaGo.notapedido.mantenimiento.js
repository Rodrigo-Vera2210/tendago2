var NotaPedidoMant = function () {
    var idOrigen = 3;
    var tableEmpaquetado;
    var tableDetalles;
    var $wizard;
    var processing = false;
    var crearNC = false;

    var delRec = function () {
        var rec = document.getElementById('Recibo').value;

        if (rec == "") {
            return false;
        }

        swal({
            title: "Recibo",
            html: true,
            text: "Devolución de recibo.<h3>¿Desea continuar?</h3>",
            type: "warning",
            showCancelButton: true,
            //closeOnConfirm: false,
            showLoaderOnConfirm: true,
            confirmButtonColor: "#921010",
            confirmButtonText: "Reversar",
            cancelButtonColor: "#F12F1F85",
            cancelButtonText: "Cancelar"
        }
            ,function () {
                $.ajax({
                    url: NotaPedidoMant.reversarCobrosUrl + '/' + rec,
                    type: 'POST'
                }).done(function (respuesta) {
                    if (isValid(respuesta)) {
                        document.getElementById('Recibo').value = "";
                        document.getElementById('recibovalor').value = 0;
                        swal({
                            title: "¡Exitoso!",
                            text: "El recibo ha sido eliminado",
                            type: "success",
                            showConfirmButton: false
                        })
                        document.getElementById('NotaPedidoForm').style.pointerEvents = 'none';
                        document.body.style.cursor = 'wait'
                        location.reload();
                    }
                });
            }
        );
        return false;
    };


    var addMeth = function () {
        var idMeth = dropmeth.options[dropmeth.selectedIndex].value;
        var desMetodoPago = dropmeth.options[dropmeth.selectedIndex].text;
        var existe = false;
        var rows = -1;
        //Valido que no existan las mismas metodos de pago
        var myTab = document.getElementById('tablaMetodosPago');
        for (i = 1; i < myTab.rows.length; i++) {
            var objCells = myTab.rows.item(i).cells;
            if (objCells[0]["innerText"] == idMeth) { existe = true; }
            rows++
        }

        if (!existe) {
            //if (idxMethod > 0) {
            //    idxMethod++;
            //}
            idxMethod = rows;

            var desMeth = $("<input data-val='true' id='CuentasPorCobrar_0__MetodosPago_" + idxMethod + "__IdMedioPago' name='CuentasPorCobrar[0].MetodosPago[" + idxMethod + "].IdMedioPago' type='hidden' value='" + idMeth + "' /><span>" + desMetodoPago + "</span> ");
            var txtDescr = $("<input data-val='true' id='CuentasPorCobrar_0__MetodosPago_" + idxMethod + "__Descripcion' name='CuentasPorCobrar[0].MetodosPago[" + idxMethod + "].Descripcion' value='' class='form-control' />");
            var txtValor = $("<input type='text' data-val='true' id='CuentasPorCobrar_0__MetodosPago_" + idxMethod + "__Valor' name='CuentasPorCobrar[0].MetodosPago[" + idxMethod + "].Valor' value='0.00' class='form-control text-right subtotal-cobro input-money' />");
            txtValor.maskMoney(optionsMaskMoney);
            txtValor.on("change paste keyup", recalcularMetodos);

            var icEliminar = CrearEliminarMetodo();

            var $tr = $("<tr></tr>");
            $tr.append($("<td></td>").append(idMeth))
                .append($("<td></td>").append(desMeth))
                .append($("<td></td>").append(txtDescr))
                .append($("<td></td>").append(txtValor))
                .append($("<td></td>").addClass("text-center").append(icEliminar));

            scntDiv.find("tbody").append($tr);

            scntDiv.append($tr)
        }
        else {
            swal({
                title: "Error",
                text: "Metodo de pago ya ingresado",
                type: "warning",
                dangerMode: true,
                confirmButtonColor: "#cc2424",
                closeOnConfirm: true
            })
        }
        return false;
    };

    function CrearEliminarMetodo() {
        var eliminar = $(`<a href="#" class="text-info btnElminarDetalle"><i class="entypo-trash"></i> </a>`);
        eliminar.on("click", eliminarDetallePago);
        return eliminar;
    }


    function setCantidadNotasPedidoTemp() {
        var cantidad = $("#cantNotasPedido").val();
        console.log(cantidad);
        $.ajax({
            url: creaCantidadUrl,
            type: 'post',
            data: { cantidad: cantidad },
            beforeSend: function () {
            },
            complete: function () {
            }
        }).done(function (respuesta) {
            if (isValid(respuesta)) {
                if (respuesta.success) {
                    actualizarCarritoCompras();
                    toastr.success(respuesta.mensaje, "", optionsToastr);
                } else {
                    toastr.error(respuesta.mensaje, "", optionsToastr);
                }
            }
        });
    }

    function existenciaProducto(idProducto) {
        $('#divModal').empty();
        $('#divModal').modal('show', { backdrop: 'static' });

        $.ajax({
            url: NotaPedidoMant.existenciaUrl,
            data: { idProducto: idProducto },
            type: 'post',
            beforeSend: function () { },
            complete: function () { }
        }).done(function (respuesta) {
            if (isValid(respuesta)) {
                $('#divModal').html(respuesta);
                $('#tablaExistenciaProducto').DataTable(opcionesDataTable);
            }
        });
    }


    function generarTransferencia() {
        $('#divModal').empty();
        $('#divModal').modal('show', { backdrop: 'static' });

        $data = $("#NotaPedidoForm").serialize();


        $.ajax({
            url: NotaPedidoMant.transferenciaUrl,
            data: $data,
            type: 'post',
            beforeSend: function () { },
            complete: function () { }
        }).done(function (respuesta) {
            if (isValid(respuesta)) {
                $('#divModal').html(respuesta);
                $('#detalleTransferencia').DataTable(opcionesDataTable);
            }
        }).fail(function (e, x) {
            console.log(e);
        });
    }


    function mostrarProducto(idProducto) {
        $.ajax({
            url: NotaPedidoMant.mostrarProductoUrl,
            data: { idProducto: idProducto },
            type: 'POST',
            beforeSend: function () {
            },
            complete: function () {
            },
            error: function (xhr, status, error) {
                console.log(`error: ${error}, status: ${status}`);
            }
        }).done(function (respuesta) {
            console.log(respuesta);
            if (isValid(respuesta)) {
                if (respuesta.Success != undefined) {
                    if (respuesta.Success == true) {
                        $('#divModal').empty();
                        $('#divModal').modal('show', { backdrop: 'static' });
                        $('#divModal').html(respuesta);
                    } else {
                        toastr.error(respuesta.Mensaje, "", optionsToastr);
                    }

                    console.log(`error: ${respuesta.Mensaje}`);
                }
            }
        });
    }

    function crearCliente() {
        $('#divModal').empty();
        $('#divModal').modal('show', { backdrop: 'static' });

        $.ajax({
            url: NotaPedidoMant.crearClienteUrl,
            type: 'get',
            beforeSend: function () {
            },
            complete: function () {
            }
        }).done(function (respuesta) {
            if (isValid(respuesta)) {
                $('#divModal').html(respuesta);

                if (respuesta.includes("crearClienteForm")) {
                    $.validator.unobtrusive.parse($("#crearClienteForm"));
                }

            }
        });
    }

    function consultarClientes() {
        //$('#divModalClientes').empty();
        $('#divModalClientes').modal('show', { backdrop: 'static' });

        $.ajax({
            url: NotaPedidoMant.consultaClientesUrl,
            type: 'get',
            beforeSend: function () {
                $("#divModalClientes").append('<div id="divLoading" class="overlay"><i class="fa fa-refresh fa-spin"></i></div>');
            },
            complete: function () {
                $("#divLoading").remove();
            }
        }).done(function (respuesta) {
            if (isValid(respuesta)) {
                $('#divModalClientesBody').html(respuesta);
            }
        });
    }

    function mostrarConsultaProductos() {
        $("#searchControl").fadeIn('fast');
        $("#divNotaPedido").fadeOut('fast');
    }

    function crearCantidadNotasPedido() {
        //$('#divNotasPedido').empty();
        $('#divNotasPedido').modal('show', { backdrop: 'static' });
    }

    function limpiarClientes() {
        localStorage.removeItem("client");
        localStorage.removeItem("checkoutData");
        setClientData({ Id: null, Identificacion: "", RazonSocial: "" });

        //   $("#clientResult").addClass('hidden').fadeIn('fast');
    }


    $(".btnElminarDetalle").click(function () {
        var trId = $(this).closest('tr').attr('id');
        var td = $(this).closest('td');

        tableDetalles.row("#" + trId).remove().draw();

        var total = 0;
        $.each($("#tablaDetalleNotasPedido tbody tr"), function () {
            total += $(this).find('td:eq(4) input').toFloat();
        });

        total = parseFloat(total.toFixed(2));

        $("#Total").val(total.toFixed(2));

        recalcularTotales();

    });

    function generarCuentasCobrar(formaPago, numeroPagos, plazodias, valorNotaPedido, valorAbono, valRecivo) {
        console.log('aqui')
        var $detallePagosDiv = $('#detallePagos');
        if ($detallePagosDiv.length == 0) {
            return;
        }

        $detallePagosDiv.empty();

        //toastr.warning("Por favor espere...", "Cargando cuotas");
        blockUI($detallePagosDiv.parent());

        return $.ajax({
            url: NotaPedidoMant.generarCuentasCobrarUrl,
            data: { formaPago: formaPago, numeroPagos: numeroPagos, plazoDias: plazodias, valorNotaPedido: valorNotaPedido, valorPagado: valorAbono, valRecivo: valRecivo, crearNC: crearNC },
            type: 'post',
            beforeSend: function () {
            },
            complete: function () {
            }
        }).done(function (respuesta) {
            var $detallePagosDiv = $('#detallePagos');

            if (isValid(respuesta)) {
                $detallePagosDiv.html(respuesta);
            }
        }).always(function () {
            toastr.clear();
            unblockUI($detallePagosDiv.parent());
            processing = false;
        });
    }

    function recalcularTotales(element) {
        console.log(element)
        if (!element) {
            element = $("#tablaDetalleNotasPedido > tbody > tr").first();
        }

        var $tr = $(element).closest('tr');
        var cantidad = $tr.find('input.cantidad-detalle').toFloat();

        if (cantidad < 1) {
            cantidad = 1;
        }

        var precio = $tr.find('input.precio-detalle').toFloat(4);
        var subtotal = $(cantidad * precio).toFloat();

        if (!subtotal) {
            subtotal = 0.00;
        }

        $tr.find('input.subtotal-detalle').val(subtotal.toFixed(2));

        var total = 0.00;
        $.each($("input.subtotal-detalle"), function () {
            total += $(this).toFloat();
        });

        total = parseFloat(total.toFixed(2));
        $(".text-total").text(total.toFixed(2));

        $("#Total").val(total.toFixed(2));
        $("#strTotal").text(total.toFixed(2));
        inicializarCuentasCobrar();
    }

    function recalcularMetodos(element) {
       if (!element) {
            element = $("#tablaMetodosPago > tbody > tr").first();
        }
        var cobros = 0.00;

        var valrec = 0.00;
        var rec = document.getElementById('recibovalor');
        valrec = (rec !== null ? rec.value : 0.00) * 1;

        $.each($("input.subtotal-cobro"), function () {
            cobros += $(this).toFloat();
        });

        cobros = parseFloat(cobros.toFixed(2)) + parseFloat(valrec);
        /*cobros = parseFloat(cobros.toFixed(2));*/

        $("#ValorAbono").val(cobros.toFixed(2));
    }


    $(document).ready(function () {
        $('.cantidad-detalle').keypress(function (event) {
            return isNumber(event, this)
        });
    });

    function isNumber(evt, element) {

        var charCode = (evt.which) ? evt.which : event.keyCode

        if (
            (charCode != 45 || $(element).val().indexOf('-') != 1) && // “-” CHECK MINUS, AND ONLY ONE.
            (charCode != 46 || $(element).val().indexOf('.') != 1) && // “.” CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57))
            return false;

        return true;
    }


    function inicializarCuentasCobrar(e) {
        //if (processing) return;

        if ($('#detallePagos').length == 0) return false;

        var numPagos = $("#Cuotas").toFloat();
        var plazoDias = $("#Plazo").toFloat();
        var valorAbono = $("#ValorAbono").toFloat();
        var valorTotal = $("#Total").toFloat();
        var valorPagado = $("#ValorPagado").toFloat();
        var valRecivo = $("#recibovalor").toFloat();

        var formaPago = $("#IdFormaPago").val();

        if (formaPago) {
            // Iniciamos el procesamiento
            processing = true;

            if (e) {

                // Si el valor de abono es igual al valor total entonces es CONTADO
                if (formaPago == "2" && (valorAbono + valRecivo) == valorTotal) {
                    valorAbono = 0;
                }

                // Si el valor de abono es igual al valor total entonces es CONTADO
                if ((valorAbono + valRecivo) == valorTotal) {
                    formaPago == "1"
                }
            }
            else {

                // Si es credito y el abono es cero. Se asigna el abono automatico.
                if ((valorAbono + valRecivo) == 0 && valorPagado > 0) {
                    valorAbono = valorPagado;
                }

                if (valorPagado < valorTotal) {
                    formaPago = "2";
                }

                if (valorPagado > valorTotal) {
                    formaPago = "1";
                }
            }

            if (formaPago == "1") {
                if (valorAbono < valorTotal && valorAbono != 0) {
                    valorAbono = valorTotal;
                }

                numPagos = 1;
                plazoDias = 0;

                $("#spinnerNumeroPagos").find(":input").prop("disabled", true);
                $("#spinnerPlazoDias").find(":input").prop("disabled", true);
                /* $("#ValorAbono").prop("disabled", true);*/
                if (valorPagado == valorTotal) {
                    $("#btnCuentasCobrar").prop("disabled", false);
                } else {
                    $("#btnCuentasCobrar").prop("disabled", false);
                }
                
            }
            else {

                $("#spinnerNumeroPagos").find(":input").prop("disabled", false);
                $("#spinnerPlazoDias").find(":input").prop("disabled", false);
                /* $("#ValorAbono").prop("disabled", false);*/
                if (valorPagado == valorTotal) {
                    $("#btnCuentasCobrar").prop("disabled", false);
                } else {
                    $("#btnCuentasCobrar").prop("disabled", false);
                }
            }

            $('#detallePagos').empty();
            $("#Cuotas").val(numPagos);
            $("#Plazo").val(plazoDias);
            //$("#ValorAbono").val(valorAbono.toFixed(2));

            recalcularMetodos();

            // Validación: Si se le pone un valor mayor al de la orden no debería calcular y mostrar una notificacion
            //if (valorAbono > valorTotal) {
            //    toastr.warning("El valor pagado es mayor al valor total del pedido!");
            //    return false;
            //}

            if (currentHandler && currentHandler.readyState < 4) {
                currentHandler.abort("Cancelado para continuar el proceso");
            }

            currentHandler = generarCuentasCobrar(formaPago, numPagos, plazoDias, valorTotal, valorAbono);
        }
    }

    var currentHandler;

    var saveAction = function (e) {
        var $accion = $("[name='Action']").val();

        if (processing) {
            return false;
        }

        if (this && this.parentElement) {
            var parent = this.parentElement;

            if (this.disabled || parent.className.includes("disabled")) {
                e.preventDefault();
                return false;
            }
        }

        var $valid_form = $("#NotaPedidoForm").valid();

        if ($valid_form) {

            if ($accion == "Empaquetar" || $accion == "Revisar") {
                var cantidad = 0;
                var validar = false;

                $.each($("#tablaDetalleEmpaquetado tbody tr"), function () {
                    cantidad = $(this).find('td:eq(1) input').toFloat();
                    if (cantidad <= 0) {
                        validar = true;
                        return false;
                    }
                });

                if (validar) {
                    toastr.error("Existe un Paquete con cantidad 0", "", optionsToastr);
                    return;
                }
            }

            var entregaInmediata = $("#EntregaInmediata").val();
            if (e != 'guardar' && ($accion == "Entregar" || entregaInmediata == "True") && validarAnticipos()) {
                return false;
            }
            //removing maskmoney
            $(".input-money").each(function () {
                var valorUnmasked = parseFloat(($(this).maskMoney('unmasked')[0]));
                $(this).val(valorUnmasked);
            });

            var $form = $("#NotaPedidoForm");
            var $div = $($form).closest('div');
            var action_url = NotaPedidoMant.accionUrl;
            var request_method = $form.attr("method");
            var form_data = $form.serializeArray();

            //Nota de Credito
            var chargeDetails = {
                name: "GeneraNc",
                value: crearNC
            };
            form_data.push(chargeDetails);
            //console.log(form_data);
            processing = true;

            return $.ajax({
                url: action_url,
                type: "post",
                data: form_data,
                timeout: 600000,
                beforeSend: function () {
                    blockUI($div)
                    $div.addClass('loading');
                },
                complete: function () {
                    unblockUI($div)
                    $div.removeClass('loading');
                }
            }).done(function (respuesta) {
                if (isValid(respuesta)) {
                    if (respuesta.success == true) {
                        toastr.success(respuesta.mensaje, "", optionsToastr);
                        actualizarConsultaNotaPedido();
                        $("#divModal").modal('hide');

                        if (respuesta.url && respuesta.url != '') {
                            location.assign(respuesta.url);
                        }
                        else {
                            actualizarConsultaNotaPedido();
                        }

                    }
                    else {
                        toastr.warning(respuesta.mensaje, "", optionsToastr);
                        $(".input-money").maskMoney('mask');
                    }
                }
                else {
                    toastr.error("Ocurrió un error al guardar el pedido", "", optionsToastr);
                }
            }).always(function () {
                processing = false;
            });

        }

        processing = false;
    };

    var validarAnticipos = function () {

        var valorTotal = $("#Total").toFloat();//V.cuota
        var valorAbono = $("#ValorAbono").toFloat();
        var saldo = valorAbono - valorTotal;

        //console.log(saldo);

        if (saldo > 0) {
            swal({
                title: "Nota de Pedido",
                html: true,
                text: "Esta nota de pedido tiene pagos anticipados mayores al valor de la orden<br>Deben ser devueltos antes de continuar<br><h3>¿Desea devolverlos ahora?</h3>",
                type: "warning",
                showCancelButton: true,
                closeOnConfirm: false,
                //showLoaderOnConfirm: true,
                confirmButtonColor: "#921010",
                confirmButtonText: "Devolver $" + saldo.toFixed(2),
                cancelButtonColor: "#F12F1F85",
                cancelButtonText: "Regresar"
            }, function () {
                swal({
                    title: "¿Está seguro que desea generar una devolución?",
                    type: "warning",
                    showCancelButton: true,
                    //closeOnConfirm: false,
                    //showLoaderOnConfirm: true,
                    confirmButtonColor: "#921010",
                    confirmButtonText: "Devolver $" + saldo.toFixed(2),
                    cancelButtonColor: "#F12F1F85",
                    cancelButtonText: "Cancelar",
                }, function () {
                    crearNC = true;
                    $("#ValorAbono").val(valorTotal);
                    inicializarCuentasCobrar();

                    if (currentHandler) {
                        currentHandler.done(function () {
                            var saveHandler = saveAction("guardar");
                            saveHandler && saveAction().done(function () {
                                swal("La orden ha sido guardada!", "", "success");
                            });
                        });
                    }
                    //if () {
                        

                    //} else {
                    //    crearNC = false;
                    //    $("#ValorAbono").val(valorTotal);
                    //    inicializarCuentasCobrar();

                    //    if (currentHandler) {
                    //        var saveHandler = saveAction("guardar");
                    //        saveHandler && currentHandler.done(function () {
                    //            var saveHandler = saveAction("guardar");
                    //            saveHandler && saveAction().done(function () {
                    //                swal("La orden ha sido guardada!", "", "success");
                    //            });
                    //        });
                    //    }
                    //}
                });

            });

            return true;
        }

        return false;
    }

    var handleStock = function (e) {
        var $this = $(this);
        var cantidad = $this.toFloat();

        if (cantidad < 1) {
            $this.val(1);
        }

        var $parent = $this.parents("tr");
        var idProduct = $parent.prop("id");
        var idOutput = $("input[name='Id']").val();

        $.ajax({
            url: NotaPedidoMant.consultaStockUrl,
            data: { idProducto: idProduct, idSalida: idOutput },
            type: 'GET',
            complete: function () {
            }
        }).done(function (respuesta) {
            if (isValid(respuesta)) {
                var stock = respuesta * 1;

                if (cantidad > stock) {
                    toastr.warning("La cantidad seleccionada para el producto es mayor a la cantidad disponible (" + stock + ")");
                    $this.val(stock);
                    recalcularTotales($this);
                }
            }
        });

    };

    var handleFindCust = function () {
        //$('#divConsultaClienteModal').empty();
        $('#divModalClientes .close').css('display', 'none');
        $('#divModalClientes').modal('show');

        $.ajax({
            url: NotaPedidoMant.consultaClientesUrl,
            type: 'GET',
            beforeSend: function () {
            },
            complete: function () {
            }
        }).done(function (respuesta) {
            if (isValid(respuesta)) {
                $('#divModalClientesBody').html(respuesta);
            }
        });
    };



    var handleDeletePacking = function (event) {
        event.preventDefault();

        var trId = $(this).closest('tr').attr('id');
        var td = $(this).closest('td');

        tableEmpaquetado.row("#" + trId).remove().draw();
    };


    var handleAddPacking = function () {
        var index = tableEmpaquetado.rows().count();
        var selectTipoPaquete = crearSelectTipoPaquete(index);
        var spinnerCantidad = crearSpinnerCantidad(index);
        var IconoEliminar = CrearIconoEliminar();

        tableEmpaquetado.row.add($("<tr id=" + "'" + index + "'" + "><td>" + selectTipoPaquete + "</td><td>" + spinnerCantidad + "</td>" + "<td " + IconoEliminar + "</td>" + "</tr>")[0]).draw();

        $(".spinner-custom").spinnercustom();
        $(".btneliminadetalleEmp").on('click', handleDeletePacking);
    };


    var HandleMant = function ($accion) {

        $(".transfer-button").on("click", generarTransferencia);

        $("#btnLimpiaCliente").on("click", function () {
            limpiarClientes();
        });

        $(".print-button").on("click", function () {
            actualizarConsultaNotaPedido();
        });

        $.validator.unobtrusive.parse($("#NotaPedidoForm"));
        $wizard = $("#NotaPedidoForm").formwizardcustom();
        $("#NotaPedidoForm").submit(function (e) {
            e.preventDefault();
            return saveAction();
        });

        tableDetalles = $('#tablaDetalleNotasPedido').basicDatatable();

        $('#btnAddMeth').click(addMeth);

        $('#btnDelRec').click(delRec);

        $(".input-money").maskMoney(optionsMaskMoney);

        $(".input-money4").maskMoney({
            thousands: ",",
            decimal: ".",
            precision: 4,
            allowZero: false,
            allowNegative: false,
            doubleClickSelection: true,
            allowEmpty: false,
            bringCaretAtEndOnFocus: true
        });

        $(".input-spinner-cantidad input").on('change paste keyup', function (e) {
            recalcularTotales(this);
        });

        $(".text-cantidad").on('change paste keyup', function (e) {
            recalcularTotales(this);
        });

        $(".text-pre").on('change paste keyup', function (e) {
            recalcularTotales(this);
        });

        $("#ValorAdicional").on('change paste keyup', function (e) {
            recalcularTotales();
        });

        $(".subtotal-cobro").on('change paste keyup', function (e) {
            recalcularMetodos(this);
        });

        $("#IdFormaPago").on("change keyup", inicializarCuentasCobrar);

        $("#Total").on('change', function (e) {
            var $total = $(this).val();

            $(".text-total").text(total)
        });

        $(".cantidad-detalle").on("change keyup", handleStock);

        $(".save-button").on("click", saveAction);

        $("#btnBuscarCliente").on("click", handleFindCust);

        $("#btnCuentasCobrar").on("click", function (e) {
            var formaPago = $("#IdFormaPago").val();
            var valorAbono = $("#ValorAbono").toFloat();
            var valorTotal = $("#Total").toFloat();
            var valRecivo = $("#recibovalor").toFloat();

            if (formaPago == 2 && (valorAbono + valRecivo) == valorTotal) {
                $("#IdFormaPago").val(1);
            }

            if (formaPago == 1 && valorAbono > valorTotal){
                swal({
                    icon: "error",
                    title: "¡Error!",
                    text: `La sumatoria de los pagos $${valorAbono} no es igual al valor de la cuota $${valorTotal}`
                })
                return false;
            }

            inicializarCuentasCobrar(e);
        });



        $(".control-revisado").on("click", function () {
            var $ctrl = $(this);
            var $tr = $(this).parents("tr");
            var $id_detalle = $tr.find("[name$='.Id']").val();
            var $empaquetado = $tr.find("[name$='.Empaquetado']").val();
            var $alistado = $(this).is(":checked");
            var $idSalida = $("[name='Id']").val();

            $.post(NotaPedidoMant.detalleUrl, {
                Id: $id_detalle, Revisado: $alistado, Empaquetado: $empaquetado, IdSalida: $idSalida
            }, function (result) {
                if (isValid(result)) {
                    var $row = $("[value='" + result.id + "']").parents("tr");
                    var $chk = $row.find("div.checkbox");

                    $chk.removeClass("color-red");
                    $chk.removeClass("color-blue");

                    if (result.success) {
                        if ($chk.find("input").val() == "true") {
                            $chk.addClass("color-green");
                        }
                        else {
                            $chk.addClass("color-blue");
                        }
                    }
                    else {
                        $chk.addClass("color-red");
                    }
                }
            });
        });

        $(".control-empaquetado").on("click", function () {
            var $ctrl = $(this);
            var $tr = $(this).parents("tr");
            var $id_detalle = $tr.find("[name$='.Id']").val();
            var $revisado = $tr.find("[name$='.Revisado']").val();
            var $alistado = $(this).is(":checked");
            var $idSalida = $("[name='Id']").val();

            $.post(NotaPedidoMant.detalleUrl, {
                Id: $id_detalle, Revisado: $revisado, Empaquetado: $alistado, IdSalida: $idSalida
            }, function (result) {
                if (isValid(result)) {
                    var $row = $("[value='" + result.id + "']").parents("tr");
                    var $chk = $row.find("div.checkbox");

                    $chk.removeClass("color-red");
                    $chk.removeClass("color-blue");

                    if (result.success) {
                        if ($chk.find("input").val() == "true") {
                            $chk.addClass("color-green");
                        }
                        else {
                            $chk.addClass("color-blue");
                        }
                    }
                    else {
                        $chk.addClass("color-red");
                    }
                }
            });
        });

        $("#ValorAbono").on("change", function () {
            var valorAbono = $("#ValorAbono").toFloat();
            var valorTotal = $("#Total").toFloat();

            // Validación: Si se le pone un valor mayor al de la orden no debería calcular y mostrar una notificacion
            if (valorAbono > valorTotal) {
                toastr.warning("El valor pagado es mayor al valor total del pedido!");
                $("#ValorAbono").val(valorTotal);
            }
            else if (valorAbono < 0) {
                $("#ValorAbono").val(0);
            }
        });

        $("#btnAgregarCliente").on("click", crearCliente);

        if ($accion = "Facturar") {

            $("[name='PorcentajeFactura']").on("change", generarFactura);
            $("[name='ConsumidorFinal']").on("change", generarFactura);
            $("[name='IdFormaPagoSri']").on("change", generarFactura);
            $("[name='Observaciones']").on("change", generarFactura);
            $("[name='IdCliente']").on("change", generarFactura);
            $("[name='Ruc']").on("change", generarFactura);
            $("[name='FechaFactura']").on("change", generarFactura);

        }


        //********************************************************************************//


        // var tableDetalleNotaPedido = $("#tablaDetalleNotasPedido").basicDatatable();
        tableEmpaquetado = $("#tablaDetalleEmpaquetado").basicDatatable();

        $("#linkAgregarEmpaquetado").on('click', handleAddPacking);
        $(".btneliminadetalleEmp").on('click', handleDeletePacking);

        $(function () {
            getSearchControl(idOrigen);
            SetActiveMenu();
        });

    };

    var $ajaxFact;

    function generarFactura() {
        if ($ajaxFact != null) {
            $ajaxFact.abort();
        }

        var $porcentajeFactura = $("#PorcentajeFactura");

        var $ptj = $porcentajeFactura.val();

        if ($ptj < 0) {
            $ptj = 1;
            $porcentajeFactura.val($ptj);
        }

        if ($ptj > 100) {
            $ptj = 100;
            $porcentajeFactura.val($ptj);
        }
        var $data = $("#NotaPedidoForm").serializeArray();

        $ajaxFact = $.ajax({
            url: NotaPedidoMant.generarFacturaUrl,
            method: "post",
            data: $data
        }).done(function (result) {
            if (isValid(result)) {
                var $div = $("#facturaDiv");
                $div.empty();
                $div.html(result);
            }
        }).always(function () {
            $ajaxFact = null;
        });

    }

    function crearSelectTipoPaquete(id) {
        var select = "<select name='DetalleEmpaquetado[" + id + "].IdTipoPaquete' class='form-control' style='width:100%'>";
        $.each(NotaPedidoMant.tiposPaquete, function (index, item) {
            select += '<option value="' + item.Id + '">' + item.Nombre + '</option>';
        });
        select += '</select>';
        return select;
    }

    function crearSpinnerCantidad(id) {
        var spinner = "<div id='spinnerCantidad" + id + "' class='spinner-custom input-spinner input-spinner-sx'>";
        spinner += "<button type='button' class='btn btn-default'>-</button>";
        spinner += "<input type='text' name='DetalleEmpaquetado[" + id + "].Cantidad' value='1' class='form-control' />";
        spinner += "<button type='button' class='btn btn-default'>+</button>";
        spinner += "</div>";
        return spinner;
    }

    function CrearIconoEliminar(id) {
        var eliminar = "class='text-center btneliminadetalleEmp'>";
        eliminar += " <a href='#' class='text-info btneliminadetalleEmp'>";
        eliminar += "<i class='entypo-trash'></i> </a>";
        return eliminar;
    }


    function actualizarConsultaNotaPedido() {
        blockUI($("body"));
        $("#resumenForm").submit();
    }


    return {
        creaCantidadUrl: "",
        existenciaUrl: "",
        consultaStockUrl: "",
        mostrarProductoUrl: "",
        crearClienteUrl: "",
        consultaClientesUrl: "",
        generarCuentasCobrarUrl: "",
        accionUrl: "",
        transferenciaUrl: "",
        tiposPaquete: [],
        generarFacturaUrl: "",
        detalleUrl: "",
        reversarCobrosUrl: "",
        init: function (action) {
            HandleMant(action);
        }
    };

}();

var onSelectClient = function (clientItemJson) {
    //$('#divModalClientesBody').empty();
    $('#divModalClientes').modal('hide');

    var client = JSON.parse(clientItemJson);
    localStorage.setItem("client", clientItemJson);
    setClientData(client);
    $("#clientResult").removeClass('hidden').fadeIn('fast');
};

var setClientData = function (client) {
    //console.log(client);
    $("#IdCliente").val(client.Id);
    $("#identificacionCli").text(client.Identificacion);
    $("#razonSocialCli").text('').text(client.RazonSocial);
    $("#IdCliente").trigger("change");
}


