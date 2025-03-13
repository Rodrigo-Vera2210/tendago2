
var POS = { 
    SaveOrder_Url: "/POS/save/",
    GetCustomers_Url:"/POS/customers",
    SetCustomer_Url: "/POS/customer",
    AddCustomer_Url: "/notapedido/crearcliente",
    NombreCliente_Url: "/POS/nombreCliente",
    idCliente_Url: "/POS/idCliente",
    SearchProducts_Url: "/POS/products/",
    LoadProduct_Url: "/POS/product/",
    Details_Url: "/POS/details",
    SubtotalCart_Url: "/POS/subtotal",
    ValorIVA_Url: "/POS/valoriva",
    SubtotalIVA_Url: "/POS/baseiva",
    Subtotal0_Url: "/POS/basecero",
    TotalCart_Url: "/POS/totitems",
    FindProduct_Url : "/POS/findproduct/",
    AddItem_Url : "/POS/addpdc/",
    EditItem_Url : "/POS/edit/",
    DeleteItem_Url : "/POS/delete/",
    HoldList_Url : "/POS/holdList/",
    AddHold_Url : "/POS/addHold",
    RemoveHold_Url : "/POS/RemoveHold/",
    GetDiscount_Url : "/POS/GetDiscount/",
    ResetPos_Url : "/POS/ResetPos/",
    AddNewSale_Url : "/POS/AddNewSale/",
    CloseRegister_Url : "/POS/CloseRegister/",
    SubmitRegister_Url : "/POS/SubmitRegister/",
    SelectHold_Url: "/POS/SelectHold/", 
    LoadModal_Url: "/POS/modal_save",
    SetRuc_Url: "/POS/ruc",
    GeneratePayments_Url: '/NotaPedido/GenerarCuentasPorCobrar',
    SaveOrderCot_Url: "/Cotizacion/save/",
};


var $customerSelect;

$(".page-container").addClass("sidebar-collapsed");

 
// barcode scanner
function barcode(e) {
    show_loading_bar(60);

    var code = $('.barcode').val();
    $.ajax({
        url: POS.FindProduct_Url +"?id="+fixedEncodeURI(code),
        type: "POST",
        dataType: "JSON",
        crossDomain: true,
        success: function (data) {
            add_posale(data);
            $('.barcode').val('');
            show_loading_bar(100);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error");
            show_loading_bar(100);
        }
    });

    return false;
};

$("select#locales").on("change", function () {
    $.get("/home/local/ruc", function (data) {
        data && $("#ruc").val(data);

    });
});


//  **********************select categorie

$(".categories").on("click", function () {
    // Retrieve the input field text
    var filter = $(this).attr('id');
    $(this).parent().children().removeClass('selectedGat');

    $(this).addClass('selectedGat');
    // Loop through the list
    $("#products #category").each(function () {
        // If the list item does not contain the text phrase fade it out
        if ($(this).val().search(new RegExp(filter, "i")) < 0) {
            $(this).parent().parent().parent().parent().hide();
            // Show the list item if the phrase matches
        } else {
            $(this).parent().parent().parent().parent().show();
        }
    });


});

// function to calculate a percentage from a number
function percentage(tot, n) {
    tot = tot ? tot.replace(",", "") : "0.00";
    n = n ? n.replace(",", "") : "0.00";

    var perc = ((parseFloat(tot) * (parseFloat(n ? n : 0) * 0.01)));
    return perc;
}

// function to calculate the total number
function total_change() {
    var tot = parseFloat($('#Subtot').text().replace(",", ""));
    var iva = parseFloat($("#taxValue").text().replace(",", ""));

    tot = tot - percentage($('#Subtot').text(), $('.Remise').val());

    $('#RemiseValue').text('$ ' + percentage($('#Subtot').text(), $('.Remise').val()).toFixed(2));

    $('#total').text(tot.toFixed(2));
    $('#pagado').val(tot.toFixed(2)).change();
    $('#TotalModal').text('Total $ ' + tot.toFixed(2));

}


function delete_posale(id) {
    // ajax delete data to database
    $.ajax({
        url: POS.DeleteItem_Url + id,
        type: "POST",  
        success: function (data) {
            $('#details').load(POS.Details_Url);
            $('#ItemsNum span, #ItemsNum2 span').load(POS.TotalCart_Url);

            $('#subtotalIVA').load(POS.SubtotalIVA_Url);
            $('#subtotal0').load(POS.Subtotal0_Url);
            $('#taxValue').load(POS.ValorIVA_Url);

            $('#Subtot').load(POS.SubtotalCart_Url, null, total_change);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#details').load(POS.Details_Url);
            alert("error");
        }
    });

}

/********************************** Hold functions ************************************/
function AddHold() {
    $.ajax({
        url: POS.AddHold_Url,
        type: "POST",
        dataType: "JSON",
        crossDomain: true,
        success: function (data) {
            $('#details').load(POS.Details_Url);
            $('#ItemsNum span, #ItemsNum2 span').load(POS.TotalCart_Url);
            $('#subtotalIVA').load(POS.SubtotalIVA_Url);
            $('#subtotal0').load(POS.Subtotal0_Url);
            $('#taxValue').load(POS.ValorIVA_Url);

            $('#Subtot').load(POS.SubtotalCart_Url, null, total_change);
            $('.holdList').load(POS.HoldList_Url);
            $('#AddSale').load(POS.LoadModal_Url);


            

            $('#customerSelect').empty();
            $.get(POS.idCliente_Url, function (idCliente) {
                $('#customerSelect').val(idCliente).trigger('change');
            });

            $.get(POS.NombreCliente_Url, function (nombreCliente) {
                $('#customerSelect').parent().find(".select2-selection__rendered").html(nombreCliente).prop("title", nombreCliente);
            });

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error");
        }
    });

}

function RemoveHold() {
    var number = $('.selectedHold').prop("id");
     
    swal({
        title: 'Estas seguro?',
        text: 'La bandeja actual sera eliminada!',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Aceptar",
        closeOnConfirm: false
    },
        function () {
            // ajax delete data to database
            $.ajax({
                url: POS.RemoveHold_Url + number,
                type: "POST",
                dataType: "JSON",
                crossDomain: true,
                success: function (data) {
                    $('#details').load(POS.Details_Url);
                    $('#ItemsNum span, #ItemsNum2 span').load(POS.TotalCart_Url);
                    $('#subtotalIVA').load(POS.SubtotalIVA_Url);
                    $('#subtotal0').load(POS.Subtotal0_Url);
                    $('#taxValue').load(POS.ValorIVA_Url);

                    $('#Subtot').load(POS.SubtotalCart_Url, null, total_change);
                    $('.holdList').load(POS.HoldList_Url);
                    $('#AddSale').load(POS.LoadModal_Url);

                    $.get(POS.SetCustomer_Url, setClientData)
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("error");
                }
            });
            swal.close();
        });
     
}

function SelectHold(number) {
    // ajax delete data to database
    $.ajax({
        url: POS.SelectHold_Url + number,
        type: "POST",
        dataType: "JSON",
        crossDomain: true,
        success: function (data) {
            $('#details').load(POS.Details_Url);
            $('#ItemsNum span, #ItemsNum2 span').load(POS.TotalCart_Url);
            $('#subtotalIVA').load(POS.SubtotalIVA_Url);
            $('#subtotal0').load(POS.Subtotal0_Url);
            $('#taxValue').load(POS.ValorIVA_Url);

            $('#Subtot').load(POS.SubtotalCart_Url, null, total_change);
            $('#' + number).parent().children().removeClass('selectedHold');
            $('#' + number).addClass('selectedHold');
            $('#AddSale').load(POS.LoadModal_Url);

            $('#customerSelect').empty();
            $.get(POS.idCliente_Url, function (idCliente) {
                $('#customerSelect').val(idCliente).trigger('change');
            });

            $.get(POS.NombreCliente_Url, function (nombreCliente) {
                $('#customerSelect').parent().find(".select2-selection__rendered").html(nombreCliente).prop("title", nombreCliente);
            });

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error");
        }
    });

}

/********************************** end Hold functions ************************************/

function add_posale(id) {
    // ajax delete data to database
    $.ajax({
        url: POS.AddItem_Url + id,
        type: "POST",
        crossDomain: true,
        success: function (data) {
            if (data === 'stock') {
                swal("No hay Stock disponible");
            } else if (data === 'unidad') {
                swal("Debe agregar Unidad de medida");
            } else {
                $('#details').load(POS.Details_Url);
                $('#ItemsNum span, #ItemsNum2 span').load(POS.TotalCart_Url);
                $('#subtotalIVA').load(POS.SubtotalIVA_Url);
                $('#subtotal0').load(POS.Subtotal0_Url);
                $('#taxValue').load(POS.ValorIVA_Url);

                $('#Subtot').load(POS.SubtotalCart_Url, null, total_change);
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR, textStatus);
            alert("error");
        }
    });

    $(".barcode")[0].focus();
}

var changing = false;


var $edit_xhr;
function edit_posale(id) {
    if ($edit_xhr && $edit_xhr.readyState < 4) {
        $edit_xhr.abort("Cancelado para continuar el proceso");
    }

    var qt1 = $('#qt-' + id).val().replace(",", "");
    var und = $('#un-' + id).val();
    var prc = $('#prc-' + id).val().replace(",","");

    $edit_xhr = $.ajax({
        url: POS.EditItem_Url + id,
        type: "POST",
        data: { qt: qt1, prc: prc, und: und },
        success: function (data) {
            if (data === 'stock') {
                swal("No hay Stock Disponible");
                
                $('#details').load(POS.Details_Url);
            }
            else if (data === 'unidad') {
                swal("Debe agregar Unidad de medida");

                $('#details').load(POS.Details_Url);
            }
            else {
                $('#details').load(POS.Details_Url);
                $('#ItemsNum span, #ItemsNum2 span').load(POS.TotalCart_Url);
                $('#subtotalIVA').load(POS.SubtotalIVA_Url);
                $('#subtotal0').load(POS.Subtotal0_Url);
                $('#taxValue').load(POS.ValorIVA_Url);

                $('#Subtot').load(POS.SubtotalCart_Url, null, total_change);
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {
            //alert("error");
            
        }
    });

    $(".barcode")[0].focus();


}



$("#customerSelect").change(function () {

    var id = $(this).find('option:selected').val();
    var name = $(this).find('option:selected').text();

    $("#customerName").text(name);

    if (id > 0)
        $.post(POS.SetCustomer_Url + "/" + id + "?name=" + name);
    else {
        $('.Remise').val('');
    }  

    $(".barcode")[0].focus();

});

function cancelPOS() {
    swal({
        title: 'Estas seguro ?',
        text: 'Perderas todos los items agregados a todas las canastas!',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: 'Aceptar!',
        closeOnConfirm: false
    },
        function () {

            $('#customerSelect').val('0');
            $('#customerSelect').trigger('change.select2');
            $('.Remise').val('');
            $('.TAX').val('0.00');

            $.ajax({
                url: POS.ResetPos_Url,
                type: "POST",
                crossDomain: true,
                success: function (data) {
                    $('#details').load(POS.Details_Url);
                    $('#subtotalIVA').load(POS.SubtotalIVA_Url);
                    $('#subtotal0').load(POS.Subtotal0_Url);
                    $('#taxValue').load(POS.ValorIVA_Url);

                    $.get(POS.SetCustomer_Url, setClientData)

                    $('#Subtot').load(POS.SubtotalCart_Url, null, total_change);
                    $('#ItemsNum span, #ItemsNum2 span').text("0");
                    $('.holdList').load(POS.HoldList_Url);

                    swal('Cancelado!', 'Se han eliminado todos los pedidos.', "success");

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("error");
                }
            });

           
        });
};



function savePOS() {
    var idcustomer = $("#customerSelect").val();
    if (!idcustomer || idcustomer == 0) {
        swal("", "Debe seleccionar un cliente de la lista!", "warning")
        return false;
    }

    var count = $("#ItemsNum > span").text()
    if (!count || count == "0") {
        swal("", "No ha seleccionado ningún producto todavía!", "warning")
        return false;
    }

    $("#customerSelect").change();
    
    $('#AddSale').load(POS.LoadModal_Url, function (data) {
        console.log(data);    
        $('#AddSale').modal("show");
    });
}

function saleBtn() {
    
    var $data = $("#saveForm").serializeArray();
    var $div = $("#AddSale");

    blockUI($div)
    $div.addClass('loading');

    /*Porcentaje de descuento*/
    var pordes = $('.Remise').val();
    var valorporDes = {
        name: 'Descuento',
        value: pordes
    };
    $data.push(valorporDes);

    /*Valor de descuento*/
    var valdes = percentage($('#Subtot').text(), $('.Remise').val()).toFixed(2);
    var valorDesc = {
        name: 'ValorDescuento',
        value: valdes
    };
    $data.push(valorDesc);



    $.ajax({
        url: POS.SaveOrder_Url,
        data: $data,
        method: "POST",
        success: function (data) {
            var $div = $("#AddSale");
            unblockUI($div);
            $div.removeClass('loading');


            if (data.status == true) {
                swal("Guardar Pedido", data.message, "success");

                $('.holdList').load(POS.HoldList_Url);

                SelectHold(0);

                if (data.url) {
                    show_url(data.url);
                }

                $div.modal("hide");
            }
            else {
                console.log(data);
                swal("", (data && data.message) || "Hubo un error al guardar el pedido!", "warning")
            } 

            $(".barcode")[0].focus();

        }, error: function (data) {
            swal("", (data && data.message) || "Hubo un error al guardar el pedido!", "error")
        }
    });

    $(".barcode")[0].focus();

}

function saleBtnCt() {


    swal({
        title: 'Estas seguro?',
        text: 'La cotizacion sera guardada!',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Aceptar",
        closeOnConfirm: false
    },
        function () {


            var $data = $("#saveForm").serializeArray();
            var $div = $("#AddSale");

            blockUI($div)
            $div.addClass('loading');

            $.ajax({
                url: POS.SaveOrderCot_Url,
                data: $data,
                method: "POST",
                success: function (data) {
                    var $div = $("#AddSale");
                    unblockUI($div);
                    $div.removeClass('loading');


                    if (data.status == true) {
                        swal("Guardar Cotizacion", data.message, "success");

                        $('.holdList').load(POS.HoldList_Url);

                        SelectHold(0);

                        if (data.url) {
                            show_url(data.url);
                        }

                        $div.modal("hide");
                    }
                    else {
                        console.log(data);
                        swal("", (data && data.message) || "Hubo un error al guardar el pedido!", "warning")
                    }

                    $(".barcode")[0].focus();

                }, error: function (data) {
                    swal("", (data && data.message) || "Hubo un error al guardar el pedido!", "error")
                }
            });

            $(".barcode")[0].focus();


        });


}

function show_url(url) {
    
    var $input = document.createElement("input");
    $input.name = "descargar";
    $input.type = "hidden";
    $input.value = "True";

    var $form = document.createElement("form");
    $form.action = url;
    $form.target = "_blank";
    $form.appendChild($input);

    document.body.appendChild($form);
    
    $form.submit();

    document.body.removeChild($form);
}


function PrintTicket() {
    $('.modal-body').removeAttr('id');
    window.print();
    $('.modal-body').attr('id', 'modal-body');
}

function CloseRegister() {
    $.ajax({
        url: POS.CloseRegister_Url,
        type: "POST",
        success: function (data) {
            $('#closeregsection').html(data);
            $('#CloseRegister').modal('show');
            setTimeout(function () { $('#countedcash').focus() }, 1000);
            $('#countedcash').on('keyup', function () {
                var change = -(parseFloat($('#expectedcash').text()) - parseFloat($(this).val()));
                var difftot = change + parseFloat($('#diffcc').text()) + parseFloat($('#diffcheque').text());
                var total = parseFloat($('#countedcc').val()) + parseFloat($('#countedcheque').val()) + parseFloat($('#countedcash').val());
                $('#countedtotal').text(total.toFixed(1));
                $('#difftotal').text(difftot.toFixed(1))
                if (change < 0) {
                    $('#diffcash').text(change.toFixed(1));
                    $('#diffcash').addClass("red");
                    $('#diffcash').removeClass("light-blue");
                } else {
                    $('#diffcash').text(change.toFixed(1));
                    $('#diffcash').removeClass("red");
                    $('#diffcash').addClass("light-blue");
                }
            });

            $('#countedcc').on('keyup', function () {
                var change = -(parseFloat($('#expectedcc').text()) - parseFloat($(this).val()));
                var difftot = change + parseFloat($('#diffcash').text()) + parseFloat($('#diffcheque').text());
                var total = parseFloat($('#countedcc').val()) + parseFloat($('#countedcheque').val()) + parseFloat($('#countedcash').val());
                $('#countedtotal').text(total.toFixed(1));
                $('#difftotal').text(difftot.toFixed(1))
                if (change < 0) {
                    $('#diffcc').text(change.toFixed(1));
                    $('#diffcc').addClass("red");
                    $('#diffcc').removeClass("light-blue");
                } else {
                    $('#diffcc').text(change.toFixed(1));
                    $('#diffcc').removeClass("red");
                    $('#diffcc').addClass("light-blue");
                }
            });

            $('#countedcheque').on('keyup', function () {
                var change = -(parseFloat($('#expectedcheque').text()) - parseFloat($(this).val()));
                var difftot = change + parseFloat($('#diffcc').text()) + parseFloat($('#diffcash').text());
                var total = parseFloat($('#countedcc').val()) + parseFloat($('#countedcheque').val()) + parseFloat($('#countedcash').val());
                $('#countedtotal').text(total.toFixed(1));
                $('#difftotal').text(difftot.toFixed(1))
                if (change < 0) {
                    $('#diffcheque').text(change.toFixed(1));
                    $('#diffcheque').addClass("red");
                    $('#diffcheque').removeClass("light-blue");
                } else {
                    $('#diffcheque').text(change.toFixed(1));
                    $('#diffcheque').removeClass("red");
                    $('#diffcheque').addClass("light-blue");
                }
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error");
        }
    });
}

function SubmitRegister() {
    var expectedcash = $('#expectedcash').text();
    var countedcash = $('#countedcash').val();
    var expectedcc = $('#expectedcc').text();
    var countedcc = $('#countedcc').val();
    var expectedcheque = $('#expectedcheque').text();
    var countedcheque = $('#countedcheque').val();
    var RegisterNote = $('#RegisterNote').val();

    swal({
        title: 'Are you sure ?',
        text: 'You will not be able to recover the Holds later!',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: 'Yes, Close it!',
        closeOnConfirm: false
    },
        function () {

            $.ajax({
                url: POS.SubmitRegister_Url,
                type: "POST",
                data: { expectedcash: expectedcash, countedcash: countedcash, expectedcc: expectedcc, countedcc: countedcc, expectedcheque: expectedcheque, countedcheque: countedcheque, RegisterNote: RegisterNote },
                success: function (data) {
                    //window.location.href = "/";
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("error");
                }
            });

            swal.close();
        });
}

function email() {
    $('#ticket').modal('hide');
    swal({
        title: "An input!",
        text: "Email:",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Email"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("You need to write an email!");
            return false
        }
        var content = $('#printSection').html();
        $.ajax({
            url: "/POS/email/",
            type: "POST",
            data: { content: content, email: inputValue },
            success: function (data) {
                $('#ticket').modal('show');
                swal.close();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("error");
            }
        });
    });
}

function pdfreceipt() {


    var content = $('#printSection').html();
    //$.redirect('/pos/pdfreceipt/', { content: content });

}


//removing virtual keyboard on mobile and tablet
var currentState = false;

function setSize() {
    var state = $(window).width() < 961;
    if (state != currentState) {
        currentState = state;
        if (state) {
            $('.barcode').removeAttr('id');
            $('.TAX').removeAttr('id');
            $('.Remise').removeAttr('id');
        } else {
            $('.barcode').attr('id', 'keyboard');
            $('.barcode').attr('id', 'num01');
            $('.barcode').attr('id', 'num02');
        }
    }
}

setSize();
$(window).on('resize', setSize);

// slim scroll setup
//for the product list in the left side
$(function () {
    $('#details').slimScroll({
        height: '355px',
        alwaysVisible: true,
        railVisible: true,
    });
});

// and the right side
$(function () {
    $('#products').slimScroll({
        height: '740px',
        allowPageScroll: true,
        alwaysVisible: true,
        railVisible: true,
    });
});

// waves paramateres
Waves.init();
Waves.attach('.flat-box', ['waves-block']);
Waves.attach('.flat-box-btn', ['waves-button']);

// virtual keyboard parametres

$('#keyboard').keyboard({
    autoAccept: true,
    usePreview: false
})
    // activate the typing extension
    .addTyping({
        showTyping: true,
        delay: 250
    });

$('#num01')
    .keyboard({
        layout: 'numpad',
        restrictInput: true, // Prevent keys not in the displayed keyboard from being typed in
        preventPaste: true,  // prevent ctrl-v and right click
        autoAccept: true,
        usePreview: false
    })
    .addTyping();

$('#num02')
    .keyboard({
        layout: 'numpad',
        restrictInput: true, // Prevent keys not in the displayed keyboard from being typed in
        preventPaste: true,  // prevent ctrl-v and right click
        autoAccept: true,
        usePreview: false
    })
    .addTyping();

$('.pagado')
    .keyboard({
        layout: 'numpad',
        restrictInput: true, // Prevent keys not in the displayed keyboard from being typed in
        preventPaste: true,  // prevent ctrl-v and right click
        autoAccept: true,
        usePreview: false
    })
    .addTyping();

/***************************** LOGIN form ***********/

$(".LoginInput").focusin(function () {
    $(this).find("span").animate({ "opacity": "0" }, 200);
});

$(".LoginInput").focusout(function () {
    $(this).find("span").animate({ "opacity": "1" }, 300);
});

/******** passwors confirmation validation ****************/

var password = document.getElementById("password")
    , confirm_password = document.getElementById("confirm_password");

function validatePassword() {
    if (password.value != confirm_password.value) {
        confirm_password.setCustomValidity("Passwords Don't Match");
    } else {
        confirm_password.setCustomValidity('');
    }
}

if (password) password.onchange = validatePassword;
if (confirm_password) confirm_password.onkeyup = validatePassword;



/************************* modal shifting fix ****************************/

$(document.body)
    .on('show.bs.modal', function () {
        if (this.clientHeight <= window.innerHeight) {
            return;
        }
        // Get scrollbar width
        var scrollbarWidth = getScrollBarWidth()
        if (scrollbarWidth) {
            $(document.body).css('padding-right', scrollbarWidth);
            $('.navbar-fixed-top').css('padding-right', scrollbarWidth);
        }
    })
    .on('hidden.bs.modal', function () {
        $(document.body).css('padding-right', 0);
        $('.navbar-fixed-top').css('padding-right', 0);
    });

function getScrollBarWidth() {
    var inner = document.createElement('p');
    inner.style.width = "100%";
    inner.style.height = "200px";

    var outer = document.createElement('div');
    outer.style.position = "absolute";
    outer.style.top = "0px";
    outer.style.left = "0px";
    outer.style.visibility = "hidden";
    outer.style.width = "200px";
    outer.style.height = "150px";
    outer.style.overflow = "hidden";
    outer.appendChild(inner);

    document.body.appendChild(outer);
    var w1 = inner.offsetWidth;
    outer.style.overflow = 'scroll';
    var w2 = inner.offsetWidth;
    if (w1 == w2) w2 = outer.clientWidth;

    document.body.removeChild(outer);

    return (w1 - w2);
};


function setClientData(data) {

    if (data && data.id > 0) {

        if ($('#customerSelect').find("option[value='" + data.id + "']").length) {

            $('#customerSelect').val(data.id).trigger('change');
            $('#customerSelect').parent().find(".select2-selection__rendered").html(data.text).prop("title", data.text);

        } else {
            // Create a DOM Option and pre-select by default
            var newOption = new Option(data.text, data.id, true, true);
            // Append it to the select
            $('#customerSelect').append(newOption).trigger('change');
        } 

        $('#customerSelect').trigger('change.select2');
        $('#divModal').load(POS.AddCustomer_Url);
    }

    $(".barcode")[0].focus();

}

function configure_customer() { 
    $customerSelect = $("#customerSelect").select2({
        ajax: {
            url: POS.GetCustomers_Url,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    term: params.term || "", // search term
                    page: params.page || 1
                };
            },
            cache: true
        },
        placeholder: 'Por favor seleccione un cliente',
        minimumInputLength: 0
    }); 
}


var currentHandler;

function generarCuentasCobrar(formaPago, numeroPagos, plazodias, valorNotaPedido, valorAbono) {    
    var $detallePagosDiv = $('#detallePagos');
    if ($detallePagosDiv.length == 0) {
        return;
    }

    $detallePagosDiv.empty();

    //toastr.warning("Por favor espere...", "Cargando cuotas");
    blockUI($detallePagosDiv.parent());

    return $.ajax({
        url: POS.GeneratePayments_Url,
        data: { formaPago: formaPago, numeroPagos: numeroPagos, plazoDias: plazodias, valorNotaPedido: valorNotaPedido, valorPagado: valorAbono },
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


function inicializarCuentasCobrar(e) {
    //if (processing) return;

    if ($('#detallePagos').length == 0) return false;

    var formaPago = $("#paymentForm").val();
     
    var numPagos = ($("#cuotas").val() * 1) || 0.00;
    var plazoDias = ($("#plazo").val() * 1) || 0.00;
    var valorAbono = ($("#TotalMeth").text().replace("$ ", "") * 1) || 0.00;
    var valorTotal = ($("#total").text() * 1) || 0.00;
     
    if (e) {
        if (formaPago == '2') {
            if (valorAbono >= valorTotal) {
                $("#pagado").val(0);
                valorAbono = 0;
            }
        }
        else {
            $("#pagado").val(valorTotal);
            valorAbono = valorTotal;
        }
    }

    $('#detallePagos').empty();

    if (currentHandler && currentHandler.readyState < 4) {
        currentHandler.abort("Cancelado para continuar el proceso");
    }
    
    currentHandler = generarCuentasCobrar(formaPago, numPagos, plazoDias, valorTotal, valorAbono);
}



$(document).ready(function () {
    // Colapso el menu antes de iniciar
    document.onkeyup = function (e) {
        if (e.key == "F2" ) {
            $(".barcode")[0].focus();
        }
    };
    $('[data-toggle="popover"]').popover();

    // select2 intialisation
    $(".js-select-options").select2();

    $("#FechaEntrega").datepickercustom();
    $("#HoraEntrega").timepickercustom();

    $('#details').load(POS.Details_Url);

    $('#divModal').load(POS.AddCustomer_Url);

    $('#ItemsNum span, #ItemsNum2 span').load(POS.TotalCart_Url);

    $('#subtotalIVA').load(POS.SubtotalIVA_Url);
    $('#subtotal0').load(POS.Subtotal0_Url);
    $('#taxValue').load(POS.ValorIVA_Url);
    $('#Subtot').load(POS.SubtotalCart_Url, null, total_change);

    $('.holdList').load(POS.HoldList_Url);

    $.get(POS.SetCustomer_Url, setClientData)

    
    //  search product
    $("#searchProd").change(function () {
        // Retrieve the input field text
        var filter = $(this).val();
        filter = filter ? encodeURI(filter) : '';

        // realizamos la busqueda de articulos y llenamos la lista
        $("#searchContaner > div > span > button > span").removeClass("glyphicon glyphicon-search");
        $("#searchContaner > div > span > button > span").addClass("fa fa-refresh fa-spin");


        var load_products = function () {
            var items = $(".product-item");

            if (items && items.length > 0) {
                var percent = (items.length > 0 ? (100 / items.length) : 0);

                show_loading_bar(1);

                for (var i = 0; i < items.length; i++) {
                    var item = $(items[i]);
                    var uid = item.attr("data-uid");

                    item.load(POS.LoadProduct_Url + uid);

                    show_loading_bar(parseFloat(i * percent));
                }

            }
            else {
                toastr.error("No se encontró productos con los parámetros especificados!");
            }

            show_loading_bar(100);

            $("#searchContaner > div > span > button > span").removeClass("fa fa-refresh fa-spin");
            $("#searchContaner > div > span > button > span").addClass("glyphicon glyphicon-search");

            $(".barcode")[0].focus();
        }

        if (filter != '') {
            show_loading_bar(10);

            $("#products").load(POS.SearchProducts_Url + "?id=" + fixedEncodeURI(filter), null, load_products);
        }
    });


    configure_customer();

    $(".barcode")[0].focus();

    
});
