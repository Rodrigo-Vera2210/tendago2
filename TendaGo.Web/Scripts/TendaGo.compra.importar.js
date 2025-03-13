var PurchaseImport = function () {
    const noFileSelectedMessage = "Seleccione el archivo que desea importar para continuar.";
    const noFileImportedMessage = "Debe importar el archivo para continuar.";
    const fileImportedWithErrors = "El archivo importado contiene errores, por favor corrija e importe nuevamente el archivo.";
    const excelFileValidationMessage = "Por favor seleccione un archivo Excel con el formato indicado para importar.";
    const textFileValidationMessage = "Por favor seleccione un archivo txt con el formato indicado para importar.";

    var
        import_main = function () {
            setProvidersSettings();

            $("#btnAddProvider").on("click", getCreateProviderForm);
            $("#btnSearchProvider").on("click", searchProvider);
            $("#btnImportButton").on("click", UploadExcel);
            $("#btnResetButton").on("click", Reset);
            $("#ValorAdicional").on("change keyup", calcularTotales);
            $("#Subtotal").on("change keyup", calcularTotales);
            $("#btnImportButtonText").on("click", UploadText);

            $("#ExcelFileUpload").on("change",
                function () {
                    $("#divData").empty();
                    fileErrorMessage.text('');
                    var vtrUpload = $("#ExcelFileUpload").val().toLowerCase();
                    var regexVTRUpload = new RegExp("(.*?)\.(xlsx|xls)$");
                    if (!(regexVTRUpload.test(vtrUpload))) {
                        var msg = excelFileValidationMessage;
                        $(fileErrorMessage).text(msg);
                        console.error(msg);
                        toastr.error(msg);
                    }
                });

            $("#TxtFileUpload").on("change",
                function () {
                    $("#divData").empty();
                    fileErrorMessage.text('');
                    var vtrUpload = $("#TxtFileUpload").val().toLowerCase();
                    var regexVTRUpload = new RegExp("(.*?)\.(txt)$");
                    if (!(regexVTRUpload.test(vtrUpload))) {
                        var msg = textFileValidationMessage;
                        $(fileErrorMessage).text(msg);
                        console.error(msg);
                        toastr.error(msg);
                    }
                });

            $("#IdFormaPago").on("change", function () {
                $('#btnCuentasPagar').prop('disabled', false);
                if ($(this).val() == "2") {
                    $("#ValorPagado").val(0);
                }

                inicializarCuentasPagar();
            });

            $("#btnCuentasPagar").click(function () {
                inicializarCuentasPagar();

                //var formaPago = $("#FormaPago").val();
                //var numeroPagos = $("#numeroPagos").toFloat();
                //var plazoDias = $("#numeroPlazoDias").toFloat();
                //var valorPagado = $("#ValorPagado").toFloat();
                //var valorTotal = $("#TotalCompra").toFloat();

                //if (valorPagado > valorTotal) {
                //    valorPagado = valorTotal;
                //    $("#ValorPagado").val(valorPagado);
                //}

                //console.log($("#TotalCompra").val());
                //generarCuentasPagar(formaPago, numeroPagos, plazoDias, valorTotal, valorPagado);
            });

            $("#ValorPagado").on("change", function () {
                var valorAbono = $("#ValorPagado").toFloat();
                var valorTotal = $("#TotalCompra").toFloat();

                // Validación: Si se le pone un valor mayor al de la orden no debería calcular y mostrar una notificacion
                if (valorAbono > valorTotal) {
                    toastr.warning("El valor pagado es mayor al valor total del pedido!");
                    $("#ValorPagado").val(valorTotal);
                }
                else if (valorAbono < 0) {
                    $("#ValorPagado").val(0);
                }
            });

            $("#formImpCompra").on("submit", function (event) {
                event.preventDefault();

                $(".input-money").each(function () {
                    var valorUnmasked = $(this).toFloat();
                    $(this).val(valorUnmasked);
                });

                var formGuardarCompra = $("#formImpCompra");
                var $div = $(formGuardarCompra).closest('div');

                $.ajax({
                    url: $(this).attr("action"),
                    type: $(this).attr("method"),
                    data: $(this).serialize(),
                    beforeSend: function () {
                        blockUI($div);
                        $div.addClass('loading');
                    },
                    complete: function () {
                        unblockUI($div)
                        $div.removeClass('loading');
                    }
                }).done(function (respuesta) {
                    if (respuesta != "") {
                        if (respuesta.Success == true) {
                            toastr.success(respuesta.Mensaje, "", optionsToastr);
                            Reset();
                            $('.finish button').prop('disabled', true);
                            $('#btnCuentasPagar').prop('disabled', true);
                        }
                        else {
                            toastr.warning(respuesta.Mensaje, "", optionsToastr);
                            $(".input-money").maskMoney('mask');
                        }
                    }
                });
            });

            $(".valor-subtotal").on("change", calcularTotales);
            $(".valor-adicional").on("change", calcularTotales);

            $("#formImpCompra").purchaseWizard();
        },



        setProvidersSettings = function () {
            providers.searchControl.url = PurchaseImport.SearchSupplierUrl;
            providers.searchControl.loadingContainer = 'tab2';
            providers.searchControl.resultContainer = 'divConsultaProveedoresModal';
            providers.onSelect = onSelectProviderFunction;
            providers.onCreate = onCreatedProviderFunction;
            providers.createControl.url = PurchaseImport.CreateSupplierUrl;
            providers.createControl.loadingContainer = 'tab2';
            providers.createControl.resultContainer = 'divConsultaProveedoresModal';
        },

        onSelectProviderFunction = function (prov) {
            console.log("onSelectProvider" + prov);
            var provider = JSON.parse(prov);
            setProviderData(provider);
        },

        onCreatedProviderFunction = function (prov) {
            setProviderData(prov);
        },

        setProviderData = function (provider) {
            $("#idProvider").val(provider.Id);
            $("#idProvider").removeClass('input-validation-error').addClass("valid").attr("aria-invalid", "false");
            $("#idProvider").next().empty();
            $("#identificacionProv").text(provider.Identificacion);
            $("#razonSocialProv").text(provider.RazonSocial);
            $("#direccionProv").text(`Ciudad: ${provider.Ciudad} | Dirección: ${provider.Direccion} | Correo: ${provider.Correo}`);
            $("#providerResult").removeClass('hidden').fadeIn('fast');
        },

        searchProvider = function () {
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
        }, 

        calcularTotales = function () {
            var subtotal = $("#Subtotal").val() * 1;
            var valorAdicional = $("#ValorAdicional").val() * 1;
            var total = parseFloat((subtotal + valorAdicional).toFixed(2));

            $(".valor-total").text(total.toFixed(2))
            $("#TotalCompra").val(total);
        },

        UploadExcel = function () {
            fileErrorMessage.text('');
            var fileInput = document.getElementById('ExcelFileUpload');
            if (fileInput.files.length !== 0) {
                var formdata = new FormData(); //FormData object
                var filename = fileInput.files[0].name;
                var extension = filename.split('.').pop().toUpperCase();
                if (extension !== "XLS" && extension !== "XLSX") {
                    var msg = excelFileValidationMessage;
                    $(fileErrorMessage).text(msg);
                    console.error(msg);
                    toastr.error(data.error, '', optionsToastr);
                } else {
                    for (var i = 0; i < fileInput.files.length; i++) {
                        formdata.append(fileInput.files[i].name, fileInput.files[i]);
                    }
                    var $div = $("#formImpCompra");
                    $.ajax({
                        url: PurchaseImport.ImportExcelUrl,
                        type: 'POST',
                        data: formdata,
                        //async: false,
                        async: true,
                        beforeSend: function () {
                            blockUI($div);
                            $div.addClass('loading');
                        },
                        complete: function (dataReceived) {
                            unblockUI($div);
                            $div.removeClass('loading');
                            //console.log(JSON.stringify(dataReceived));
                        },
                        success: function (data) {
                            if (data) {
                                if (data.error) {
                                    toastr.error(data.error, "", optionsToastr);
                                    $("#hasUploadedFile").val(false);
                                    $("#hasSuccessFile").val(false);
                                }
                                else {
                                    $("#divData").empty().html(data);
                                    $("#compraTable").basicDatatable();
                                    $("#hasUploadedFile").val(true);
                                    var isFileValid = $("#isFileValid").val();
                                    var hasSuccessFile = isFileValid === 'True';
                                    console.info("isFileValid: " + isFileValid);
                                    console.info("hasSuccesFile: " + hasSuccessFile);
                                    $("#hasSuccessFile").val(hasSuccessFile);
                                }
                            }
                            else {
                                toastr.error(data.error);
                                $("#hasUploadedFile").val(false);
                                $("#hasSuccessFile").val(false);
                            }

                            calcularTotales();
                        },
                        error: function (xhr, status, error) {
                            unblockUI($div);
                            $div.removeClass('loading');
                            if (xhr.responseText) {
                                var err = JSON.parse(xhr.responseText);
                                console.log(JSON.stringify(xhr));
                                toastr.error(err.Message);
                            }
                            else {
                                toastr.error("Hubo un error al procesar el comando... " + xhr.responseText);
                            }
                        },
                        cache: false,
                        contentType: false,
                        processData: false
                    });
                }
            } else {
                $("#fileErrorMessage").text(noFileSelectedMessage);
                toastr.error(noFileSelectedMessage, '', optionsToastr);
            }
        },

        UploadText = function () {
            fileErrorMessage.text('');
            var fileInput = document.getElementById('TxtFileUpload');
            if (fileInput.files.length !== 0) {
                var formdata = new FormData(); //FormData object
                var filename = fileInput.files[0].name;
                var extension = filename.split('.').pop().toUpperCase();
                if (extension !== "TXT") {
                    var msg = textFileValidationMessage;
                    $(fileErrorMessage).text(msg);
                    console.error(msg);
                    toastr.error(data.error, '', optionsToastr);
                } else {
                    for (var i = 0; i < fileInput.files.length; i++) {
                        formdata.append(fileInput.files[i].name, fileInput.files[i]);
                    }
                    var $div = $("#formImpGasto");
                    $.ajax({
                        url: PurchaseImport.ImportTextUrl,
                        type: 'POST',
                        data: formdata,
                        //async: false,
                        async: true,
                        beforeSend: function () {
                            blockUI($div);
                            $div.addClass('loading');
                        },
                        complete: function (dataReceived) {
                            unblockUI($div);
                            $div.removeClass('loading');
                            //console.log(JSON.stringify(dataReceived));
                        },
                        success: function (data) {
                            if (data) {
                                if (data.error) {
                                    toastr.error(data.error, "", optionsToastr);
                                    $("#hasUploadedFile").val(false);
                                    $("#hasSuccessFile").val(false);
                                }
                                else {
                                    $("#divData").empty().html(data);
                                    $("#compraTable").basicDatatable();
                                    $("#hasUploadedFile").val(true);
                                    var isFileValid = $("#isFileValid").val();
                                    var hasSuccessFile = isFileValid === 'True';
                                    console.info("isFileValid: " + isFileValid);
                                    console.info("hasSuccesFile: " + hasSuccessFile);
                                    $("#hasSuccessFile").val(hasSuccessFile);
                                }
                            }
                            else {
                                toastr.error(data.error);
                                $("#hasUploadedFile").val(false);
                                $("#hasSuccessFile").val(false);
                            }

                            calcularTotales();
                        },
                        error: function (xhr, status, error) {
                            unblockUI($div);
                            $div.removeClass('loading');
                            if (xhr.responseText) {
                                var err = JSON.parse(xhr.responseText);
                                console.log(JSON.stringify(xhr));
                                toastr.error(err.Message);
                            }
                            else {
                                toastr.error("Hubo un error al procesar el comando... " + xhr.responseText);
                            }
                        },
                        cache: false,
                        contentType: false,
                        processData: false
                    });
                }
            } else {
                $("#fileErrorMessage").text(noFileSelectedMessage);
                toastr.error(noFileSelectedMessage, '', optionsToastr);
            }
        },

        nextValidationFunc = function onNextValidation($tab, $navigation, index) {
            //console.log("tab: " + JSON.stringify($tab));
            //console.log("navigation:" + JSON.stringify($navigation));
            console.log("index: " + index);
            var totalCompra = $("#TotalCompra").val();
            console.log("totalCompra: " + totalCompra);
            var msg;
            if (index === 1) {
                var fileInput = document.getElementById('ExcelFileUpload');
                var hasFileUploaded = $("#hasUploadedFile").val();
                console.log("hasFileUploaded: " + hasFileUploaded);
                if (fileInput.files.length === 0) {
                    msg = noFileSelectedMessage;
                    $("#fileErrorMessage").text(msg);
                    toastr.error(msg);
                    return false;
                }
                if (hasFileUploaded) {
                    var hasSuccessFile = JSON.parse($("#hasSuccessFile").val());
                    console.log("hasSuccessFile: " + hasSuccessFile);
                    if (hasSuccessFile !== true) {
                        msg = fileImportedWithErrors;
                        toastr.error(msg);
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    msg = noFileImportedMessage;
                    toastr.warning(msg);
                    $("#fileErrorMessage").text(msg);
                    return false;
                }
            }
            if (index === 2) {
                var formToValidate = $("#formImpCompra");
                var $validator = formToValidate.validate();
                var $valid = formToValidate.valid();

                if (!$valid) {
                    $validator.focusInvalid();
                    return false;
                }
            }
            return true;
        },

        Reset = function () {
            var $div = $("#formImpCompra");
            blockUI($div);
            $div.addClass('loading');

            setTimeout(function () {
                $("#divData").empty();
                $("#detallePagos").empty();
                fileErrorMessage.text('');
                $("#ExcelFileUpload").val('');
                $("#hasSuccessFile").val('');
                $("#hasUploadedFile").val('');

                $("#idProvider").val('');
                $("#identificacionProv").text('');
                $("#razonSocialProv").text('');
                $("#direccionProv").text('');

                $("#numeroPagos").val('');
                $("#numeroPlazoDias").val('');
                $("#ValorPagado").val('');

                $("#providerResult").fadeOut('fast');
                unblockUI($div);
                $div.removeClass('loading');
                document.getElementById("formImpCompra").reset();


            }, 2000);
        },

        processing = false,

        inicializarCuentasPagar = function () {

            if (processing) return;

            if ($('#detallePagos').length == 0) return false;

            var numPagos = $("#numeroPagos").toFloat();
            var plazoDias = $("#numeroPlazoDias").toFloat();
            var valorPagado = $("#ValorPagado").toFloat();
            var valorTotal = $("#TotalCompra").toFloat();

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
        },


        generarCuentasPagar = function (formaPago, numeroPagos, plazodias, valorCompra, valorPagado) {
            $('#detallePagos').empty();
            $.ajax({
                url: PurchaseImport.CuentasPagarUrl,
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
        },


        getCreateProviderForm = function () {
            var $divContainer = $(`#${providers.createControl.loadingContainer}`);
            $(`#${providers.createControl.resultContainer}`).empty();
            $(`#${providers.createControl.resultContainer}`).modal('show', { backdrop: 'static' });

            $.ajax({
                url: `${providers.createControl.url}`,
                type: 'GET',
                beforeSend: function () {
                    blockUI($divContainer);
                    $divContainer.addClass('loading');
                },
                complete: function () {
                    unblockUI($divContainer);
                    $divContainer.removeClass('loading');
                }
            }).done(function (respuesta) {
                if (respuesta !== "") {
                    $(`#${providers.createControl.resultContainer}`).html(respuesta);
                    $('#IdEstado').cmodevCheckToogle();
                }
            });
        };


    return {
        ImportExcelUrl: "",
        ImportTextUrl: "",
        CuentasPagarUrl: "",
        SearchSupplierUrl: "",
        CreateSupplierUrl: "",
        Init: function () {
            import_main();
        }
    };
}();

var providers = {
    searchControl: {
        url: '',
        loadingContainer: '',
        resultContainer: ''
    },
    createControl: {
        url: '',
        loadingContainer: '',
        resultContainer: ''
    },
    onSelect: function (provider) {
        console.log("onSelectProvider" + provider);
    },
    onCreate: function (newProvider) {
        console.log("onCreate" + newProvider);
    }
};



function onSelectProvider(provider) {
    providers.onSelect(provider);
}

function onCreateProvider(newProvider) {
    providers.onCreate(newProvider);
    $(`#${providers.searchControl.resultContainer}`).modal('hide');
}


 