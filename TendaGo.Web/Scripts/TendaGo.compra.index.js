var providers = {
    searchControl: {
        url: '',
        loadingContainer:'',
        resultContainer:''
    },
    createControl: {
        url: '',
        loadingContainer: '',
        resultContainer: ''
    },
    onSelect: function(provider) {
        console.log("onSelectProvider" + provider);
    },
    onCreate: function(newProvider) {
        console.log("onCreate" + newProvider);
    }
};

$(".page-container").addClass("sidebar-collapsed");


function onSelectProvider(provider) {
    providers.onSelect(provider);
}
function onCreateProvider(newProvider) {
    providers.onCreate(newProvider);
    $(`#${providers.searchControl.resultContainer}`).modal('hide');
}

function searchProvider() {
    var $div = $(`#${providers.searchControl.loadingContainer}`);
    $(`#${providers.searchControl.resultContainer}`).empty();
    $(`#${providers.searchControl.resultContainer}`).modal('show', { backdrop: 'static' });

    $.ajax({
        url: `${providers.searchControl.url}`,
        type: 'GET',
        beforeSend: function () {
            blockUI($div);
            $div.addClass('loading');
        },
        complete: function () {
            unblockUI($div);
            $div.removeClass('loading');
        }
    }).done(function (respuesta) {
        if (respuesta !== "") {
            $(`#${providers.searchControl.resultContainer}`).html(respuesta);
        }
    });
}

function getCreateProviderForm() {
    var $div = $(`#${providers.createControl.loadingContainer}`);
    $(`#${providers.createControl.resultContainer}`).empty();
    $(`#${providers.createControl.resultContainer}`).modal('show', { backdrop: 'static' });
    $.ajax({
        url: `${providers.createControl.url}`,
        type: 'GET',
        beforeSend: function () {
            blockUI($div);
            $div.addClass('loading');
        },
        complete: function () {
            unblockUI($div);
            $div.removeClass('loading');
        }
    }).done(function (respuesta) {
        if (respuesta !== "") {
            $(`#${providers.createControl.resultContainer}`).html(respuesta);
            $('#IdEstado').cmodevCheckToogle();
        }
    });
}

function crearDeleteButton(id) {
    return "<a class='btn text-danger delete-btn'><i class='entypo-trash'></i></a>";
}

function crearSerieButton(id) {
    return "<a class='btn text-info serie-btn'><i class='entypo-doc-text-inv'></i></a>";
}

function crearSpinnerCantidad(id) {
    var spinner = "<div id='spinnerCantidad" + id + "' class='input-spinner input-spinner-cantidad input-spinner-sx'>";
    spinner += "<button type='button' class='btn btn-default'>-</button>";
    spinner += "<input type='text' name='DetalleCompra[" + id + "].Cantidad' value='1' data-min='1' class='form-control text-cantidad' />";
    spinner += "<button type='button' class='btn btn-default'>+</button>";
    spinner += "</div>";
    return spinner;
}

function crearInputGroup(id, nombrePropiedadOculta, nombrePropiedad, nombreBotonBusqueda) {
    var input = "<div class='input-group' style='width:100%;'>";
    input += "<input class='" + nombrePropiedadOculta + "' id='DetalleCompra_" + id + "__" + nombrePropiedadOculta + "' name='DetalleCompra[" + id + "]." + nombrePropiedadOculta + "' type='hidden' >";
    input += "<input class='form-control " + nombrePropiedad + "' id='DetalleCompra_" + id + "__" + nombrePropiedad + "' name='DetalleCompra[" + id + "]." + nombrePropiedad + "' readonly='readonly' type='text' >";
    input += "<div class='input-group-btn'>";
    input += "<button type='button' class='btn btn-blue " + nombreBotonBusqueda + "'>";
    input += "<i class='entypo-search'></i>";
    input += "</button>";
    input += "</div>";
    input += "</div>";
    return input;
}

function crearInputDate(id, nombrePropiedad) {
    var input = "<input class='form-control text-" + nombrePropiedad + "' type='date' id='DetalleCompra_" + id + "__" + nombrePropiedad + "' name='DetalleCompra[" + id + "]." + nombrePropiedad + "'  >";
    return input;
}

function crearInputHidden(id, nombrePropiedad) {
    var input = "<input class='text-" + nombrePropiedad +"' type='hidden' id='DetalleCompra_" + id + "__" + nombrePropiedad + "' name='DetalleCompra[" + id + "]." + nombrePropiedad + "'  >";
    return input;
}

function crearInputDecimal(id, nombrePropiedad, readonly) {
    var classReadonly = "";
    var propiedadReadonly = "";
    if (readonly == true) {
        classReadonly = "transparente";
        propiedadReadonly = "disabled='disabled' readonly='readonly'";
    }
/*    debugger;*/
    var claseMaskDecimales = (nombrePropiedad === 'CostoVenta' || nombrePropiedad === 'FactorDistribucion') ? 'input-money-4': 'input-money';

    var input = "<input class='form-control " + classReadonly + " text-right text-" + nombrePropiedad +" "+claseMaskDecimales+"' id='DetalleCompra_" + id + "__" + nombrePropiedad + "' name='DetalleCompra[" + id + "]." + nombrePropiedad + "' " + propiedadReadonly + " type='text' >";
    return input;
}

function crearSelect(id, nombrePropiedad, readonly, jsonElements) {
    var propiedadReadonly = "";
    if (readonly == true) {
        propiedadReadonly = "disabled='disabled'";
    }
    var select = "<select id='DetalleCompra_" + id + "__" + nombrePropiedad + "' name='DetalleCompra[" + id + "]." + nombrePropiedad + "' class='form-control' " + propiedadReadonly + ">";
    $.each(JSON.parse(jsonElements), function (index, item) {
        select += '<option value="' + item.Id + '">' + item.Nombre + '</option>';
    });
    select += '</select>';
    return select;
}


function crearInputSerie(id, nombrePropiedad,texto) {    
    var input = "<input class='form-control " + nombrePropiedad + "' id='Serie_" + id + "__" + nombrePropiedad + "' name='Serie[" + id + "]." + nombrePropiedad + "' readonly type='text' value='"+texto+"' >";
    return input;
}

function crearDeleteButtonSe(id) {
    return "<a class='btn text-danger deleteSe-btn'><i class='entypo-trash'></i></a>";
}

function setProvidersSettings() {
    providers.searchControl.url = searchProvidersUrl;
    providers.searchControl.loadingContainer = 'tab2';
    providers.searchControl.resultContainer = 'divConsultaProveedoresModal';
    providers.onSelect = onSelectProviderFunction;
    providers.onCreate = onCreatedProviderFunction;
    providers.createControl.url = createProviderFormUrl;
    providers.createControl.loadingContainer = 'tab2';
    providers.createControl.resultContainer = 'divConsultaProveedoresModal';
}

var onSelectProviderFunction = function (prov) {
    //console.log("onSelectProvider" + prov);
    var provider = JSON.parse(prov);
    setProviderData(provider);
}

var onCreatedProviderFunction = function (prov) {
    setProviderData(prov);
}
function setProviderData(provider) {
    $("#idProvider").val(provider.Id);
    $("#idProvider").removeClass('input-validation-error').addClass("valid").attr("aria-invalid", "false");
    $("#idProvider").next().empty();
    $("#identificacionProv").text(provider.Identificacion);
    $("#razonSocialProv").text(provider.RazonSocial);
    $("#direccionProv").text(`Ciudad: ${provider.Ciudad} | Dirección: ${provider.Direccion} | Correo: ${provider.Correo}`);
    $("#providerResult").removeClass('hidden').fadeIn('fast');
}

function onSelectProduct(product) {
    console.log("onSelectProduct" + product);
    var pro = JSON.parse(product);
    productoSeleccionado = pro;
    var $tr = $btnBuscarProducto.closest('tr');
    var $td = $tr.find('td');
    $td.eq(1).find('.IdProducto').val(pro.Id);
    $td.eq(1).find('.NombreProducto').val(pro.CodigoInterno + " " + pro.Nombre);
    $td.eq(1).find('.TipoProducto').val(pro.TipoProducto);

    llenarSelectTipoUnidad($tr.attr('id'), pro.Id);
}


var rowIndex = 0;
var compraImportacion = 1;
var rowIndexSe = 0;

$("#btnNuevoDetalle").click(function () {
    var index = rowIndex++;
    var inputNombreProducto = crearInputGroup(index, "IdProducto", "NombreProducto", "btnBuscarProducto");
    var selectLocalBodega = crearSelect(index, "IdLocal", false, jsonLocales);
    var selectTipoUnidad = crearSelect(index, "IdTipoUnidad", true, '[]');
    var inputValorFOB = crearInputDecimal(index, "ValorFOB", false);
    var inputFactorDistribucion = crearInputDecimal(index, "FactorDistribucion", false);
    var inputCostoUnitarioImportacion = crearInputDecimal(index, "CostoUnitarioImportacion", false);
    var inputCostoVenta = crearInputDecimal(index, "CostoVenta", false);
    var spinnerCantidad = crearSpinnerCantidad(index);
    var inputSubtotal = crearInputDecimal(index, "Subtotal", true);
    var inputCaducidad = crearInputDate(index, "FechaCaducidad");
    var deleteButton = crearDeleteButton(index);
    var serieButton = crearSerieButton(index);
    var $table = $("#tablaDetalleCompra tbody");

    var TpProduc = "<input class='TipoProducto' id='DetalleCompra_" + index + "__TipoProducto' name='DetalleCompra[" + index + "].TipoProducto' type='hidden' >";
    /////////////////////////////////////////////////////
    if (compraImportacion) {
        $table.append(`<tr id="${index}"><td>${deleteButton}</td><td>${inputNombreProducto} ${TpProduc}</td><td>${selectLocalBodega}</td><td>${spinnerCantidad}</td>
                <td>${selectTipoUnidad}</td><td>${inputValorFOB}</td><td>${inputFactorDistribucion}</td><td>${inputCostoUnitarioImportacion}</td>
                <td>${inputCostoVenta}</td><td>${inputCaducidad}</td><td>${serieButton}</td><td>${inputSubtotal}</td></tr>`);
    }
    else {
        inputValorFOB = crearInputHidden(index, "ValorFOB");
        inputCostoUnitarioImportacion = crearInputHidden(index, "CostoUnitarioImportacion");
        inputCostoVenta = crearInputHidden(index, "CostoVenta");

        $table.append(`<tr id="${index}"><td>${deleteButton} ${inputValorFOB} ${inputCostoUnitarioImportacion} ${inputCostoVenta}</td>
                <td>${inputNombreProducto}</td><td>${selectLocalBodega}</td><td>${spinnerCantidad}</td>
                <td>${selectTipoUnidad}</td><td>${inputFactorDistribucion}</td><td>${serieButton}</td><td>${inputSubtotal}</td></tr>`);
    }
    /////////////////////////////////////////////////////

    $("#spinnerCantidad" + index).spinnercustom();
    $(".input-money").maskAsDecimal();
    $(".input-money-4").maskAsDecimal({ precision: 4 });

    $(".delete-btn").on("click", function () {
        $(this).parent().parent().remove();
    });

    $(".deleteSe-btn").on("click", function () {
        debugger;
        console.log("Voy a borrar");
        $(this).parent().parent().remove();
    });

    $(".serie-btn").on("click", function () {
        $("#miModal").modal("show");
    });

    $("#btnAddSerie").on("click", function () {
        var serie = $("#lserie").val();
        if (serie.length <= 0) {
            swal("","Debe ingresar serie.!", "error")
            return;
        }

        var indexSe = rowIndexSe++;
        var $table = $("#tablaSerie tbody");
        var inputSerie = crearInputSerie(indexSe, "Serie", serie);
        var deleteButtonSe = crearDeleteButtonSe(indexSe);

        $table.append(`<tr id="${indexSe}"><td></td><td>${inputSerie}</td><td>${deleteButtonSe}</td></tr>`);

        $("#lserie").val() = "";
    });

    $('.input-spinner-cantidad button').on('click', function () {
        recalcularTotales(this);
    });

    $('.input-spinner-cantidad input').on('change paste keyup', function () {
        recalcularTotales(this);
    });

    $('.text-FactorDistribucion').on('change paste keyup', function () {
        recalcularTotales(this);
    });

    $('.text-CostoVenta').on('change paste keyup', function () {
        recalcularTotales(this);
    });

});

function llenarSelectTipoUnidad(index, idProducto) {
    var $tipoUnidadDropDown = $("#DetalleCompra_" + index + "__IdTipoUnidad");
    $tipoUnidadDropDown.empty();
    $tipoUnidadDropDown.attr("disabled", "disabled");

    $.ajax({
        url: tipoUnidadUrl,
        type: 'post',
        data: { idProducto: idProducto }
    }).done(function (respuesta) {
        if (respuesta.length > 0) {
            $tipoUnidadDropDown.removeAttr("disabled");
            $.each(respuesta,
                function (index, item) {
                    var p = new Option(item.Nombre, item.Id);
                    $tipoUnidadDropDown.append(p);
                });
        }
        else {
            var p = new Option("Debe configurar las unidades de venta del producto", null);
            $tipoUnidadDropDown.append(p);
            toastr.warning("El producto especificado no tiene configuradas las unidades de venta en el modulo de Precios");
        }
    });
}

$("#IdFormaPago").change(function () {
    if ($(this).val() == "2") {
        $("#ValorPagado").val(0);
    }

    inicializarCuentasPagar();
});

var processing = false;

function inicializarCuentasPagar() {

    if (processing) return;

    if ($('#detallePagos').length == 0) return false;

    var numPagos = $("#numeroPagos").toFloat();
    var plazoDias = $("#numeroPlazoDias").toFloat();
    var valorPagado = $("#ValorPagado").toFloat();
    var valorTotal = $("#Total").toFloat();

    var formaPago = $("#IdFormaPago").val();
    if (formaPago) {
        // Iniciamos el procesamiento
        processing = true;

        if (valorPagado >= valorTotal) {
            formaPago = "1";
        }

        if (formaPago == "1") {
            numPagos = 1;
            plazoDias = 0;
            valorPagado = valorTotal;

            $("#spinnerNumeroPagos").find(":input").prop("disabled", true);
            $("#spinnerPlazoDias").find(":input").prop("disabled", true);
            $("#ValorPagado").prop("disabled", true);
            $("#btnCuentasCobrar").prop("disabled", true);
        }
        else {
            $("#spinnerNumeroPagos").find(":input").prop("disabled", false);
            $("#spinnerPlazoDias").find(":input").prop("disabled", false);
            $("#ValorPagado").prop("disabled", false);
            $("#btnCuentasCobrar").prop("disabled", false);
            $("#ValorPagado").val(valorPagado.toFixed(2));
        }

        $('#detallePagos').empty();
        $("#numeroPagos").val(numPagos);
        $("#numeroPlazoDias").val(plazoDias);
        $("#IdFormaPago").val(formaPago);


        // Validación: Si se le pone un valor mayor al de la orden no debería calcular y mostrar una notificacion
        //if (valorAbono > valorTotal) {
        //    toastr.warning("El valor pagado es mayor al valor total del pedido!");
        //    return false;
        //}

        generarCuentasPagar(formaPago, numPagos, plazoDias, valorTotal, valorPagado);

    }

    processing = false;
}

function generarCuentasPagar(formaPago, numeroPagos, plazodias, valorCompra, valorPagado) {
    $('#detallePagos').empty();
    $.ajax({
        url: generarCuentasUrl,
        data: {
            formaPago: formaPago,
            numeroPagos: numeroPagos,
            plazoDias: plazodias,
            valorCompra: valorCompra,
            valorPagado: valorPagado
        },
        type: 'post',
        beforeSend: function () {
        },
        complete: function () {
        }
    }).done(function (respuesta) {
        if (respuesta != "") {
            $('#detallePagos').html(respuesta);
        }
    });
}

function recalcularTotales(element) {
    var $tr = $(element).closest('tr');
    var cantidad = $($tr.find('.text-cantidad:first')).toFloat(4);
    //var costoVenta = parseFloat($tr.find('.text-CostoVenta:first').maskMoney('unmasked')[0]);
    var costoVenta = $($tr.find('.text-FactorDistribucion:first')).toFloat(4);
    var subtotal = $((cantidad * costoVenta) ).toFloat(2);

    if (!compraImportacion) {
        $tr.find('.text-CostoVenta:first').val(costoVenta);
    }
     
    $tr.find('.text-Subtotal:first').maskMoney('mask', subtotal);

    var subtotal = 0;
    $.each($("#tablaDetalleCompra tbody tr"),
        function () {
            subtotal += parseFloat($(this).find('.text-Subtotal:first').val());
        });

    subtotal = parseFloat(subtotal.toFixed(4));

    if (!subtotal) {
        subtotal = 0.0000;
    }

    var valorAdicional = parseFloat($("#ValorAdicional").val());
    if (!valorAdicional) {
        valorAdicional = 0.0000;
    }

    var total = subtotal + valorAdicional;

    $("#Total").val(total);

    $(".valor-adicional").text(valorAdicional.toFixed(2));
    $(".valor-subtotal").text(subtotal.toFixed(2));
    $(".valor-total").text(total.toFixed(2));

    inicializarCuentasPagar();
}

$('#guardarCompraForm').on('click', '.btnBuscarProducto', function () {
    $btnBuscarProducto = $(this);
    $('#divConsultaProductosModal').empty();
    $('#divConsultaProductosModal').modal('show', { backdrop: 'static' });
    getSearchProductsModal(idOrigen);
});

$("#btnCuentasPagar").click(function () {
    //var formaPago = $("#FormaPago").val();
    //var numeroPagos = $("#numeroPagos").toFloat();
    //var plazoDias = $("#numeroPlazoDias").toFloat();
    //var valorPagado = $("#ValorPagado").toFloat();
    //var valorTotal = $("#Total").toFloat();

    //if (valorPagado > valorTotal) {
    //    valorPagado = valorTotal;
    //    $("#ValorPagado").val(valorPagado);
    //}

    inicializarCuentasPagar();
});

$("#ValorPagado").on("change", function () {
    var valorAbono = $("#ValorPagado").toFloat();
    var valorTotal = $("#Total").toFloat();

    // Validación: Si se le pone un valor mayor al de la orden no debería calcular y mostrar una notificacion
    if (valorAbono > valorTotal) {
        toastr.warning("El valor pagado es mayor al valor total del pedido!");
        $("#ValorPagado").val(valorTotal);
    }
    else if (valorAbono < 0) {
        $("#ValorPagado").val(0);
    }
});

$("#btnConsultarProveedor").click(function () {
    var $div = $("#guardarCompraForm").closest('div');
    $('#divConsultaProveedoresModal').empty();
    $('#divConsultaProveedoresModal').modal('show', { backdrop: 'static' });

    $.ajax({
        url: buscarProveedoresUrl,
        type: 'get',
        beforeSend: function () {
            blockUI($div)
            $div.addClass('loading');
        },
        complete: function () {
            unblockUI($div)
            $div.removeClass('loading');
        }
    }).done(function (respuesta) {
        if (respuesta !== "") {
            $('#divConsultaProveedoresModal').html(respuesta);
        }
    });
});

$("#divConsultaProveedoresModal").on("hidden.bs.modal", function () {
    var $filaSeleccionada = $("#tablaProveedores tbody tr.selected:first");
    if ($filaSeleccionada.length) {
        var $columnasSeleccionadas = $filaSeleccionada.find('td');
        $("#IdProveedor").val($columnasSeleccionadas.eq(1).text());
        $("#divRazonSocial").empty().append('<span>' + $columnasSeleccionadas.eq(2).text() + '</span>');
        $("#divIdentificacion").empty().append('<span>' + $columnasSeleccionadas.eq(3).text() + '</span>');
        $("#divCiudad").empty().append('<span>' + $columnasSeleccionadas.eq(4).text() + '</span>');
        $("#divDireccion").empty().append('<span>' + $columnasSeleccionadas.eq(5).text() + '</span>');
    }
});

$("#guardarCompraForm").submit(function (event) {
    event.preventDefault();
    //removing maskmoney
    $(".input-money").each(function () {
        var valorUnmasked = parseFloat(($(this).maskMoney('unmasked')[0]));
        $(this).val(valorUnmasked);
    });
    var formGuardarCompra = $("#guardarCompraForm");
    var $div = $(formGuardarCompra).closest('div');
    var action_url = formGuardarCompra.attr("action");
    var request_method = formGuardarCompra.attr("method");
    var form_data = formGuardarCompra.serialize();

    $.ajax({
        url: action_url,
        type: request_method,
        data: form_data,
        beforeSend: function () {
            blockUI($div);
            $div.addClass('loading');
        },
        complete: function () {
            unblockUI($div);
            $div.removeClass('loading');
        }
    }).error(function (error, a, b) {
        console.log(error);
        console.log({ a, b });

        toastr.error(error.Mensaje, "", optionsToastr);
    }).done(function (respuesta) {
        if (respuesta !== "") {
            if (respuesta.Success == true) {
                toastr.success(respuesta.Mensaje, "", optionsToastr);
                setTimeout(function () {
                    location.href = selfUrl;
                }, 2000);
            } else {
                toastr.warning(respuesta.Mensaje, "", optionsToastr);
                $(".input-money").maskMoney('mask');
            }
        }
    });
});

var nextValidationFunc = function onNextValidation($tab, $navigation, index) {
    console.log("index: " + index);

    if (index === 2) {
        var formToValidate = $("#guardarCompraForm");
        var $validator = formToValidate.validate();
        var $valid = formToValidate.valid();
        if (!$valid) {
            $validator.focusInvalid();
            return false;
        }
    }
    return true;
};

$(function () {
    //$("#guardarCompraForm").purchaseWizard();

    $("#guardarCompraForm").purchaseWizard({
        nextValidation: nextValidationFunc
    });


});