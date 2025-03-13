
var MantCliente = {

    idPais : null,
    idCiudad : null,
    idProvincia : null,
    idSector : null,
    urlProvincias : 'NotaPedido/ObtenerProvinciasPorPais',
    urlCiudades : 'NotaPedido/ObtenerCiudadesPorProvincia', 

    urlConsultaCliente: "NotaPedido/ConsultarClientesCrear/",
    init: function () {
        MantCliente.idPais = $("select[name='IdPais']").val();
        MantCliente.idProvincia = $("select[name='IdProvincia']").val();
        MantCliente.idCiudad = $("select[name='IdCiudad']").val();        
        MantCliente.idSector = $("select[name='IdSector']").val();
          
        var validaTipo = function () {
            var $tipo = $(".tipo-identidad").val();
            var $identidad = $(".valor-identidad");
            var $value = $identidad.val();

            if ($tipo == 'R') {
                $identidad.attr("type", "number");
                $identidad.val($value.substring(0, 13));
                $identidad.attr("data-validate", "number,minlength[13],maxlength[13]")
                $identidad.attr("maxlength", "13");
                $identidad.attr("minlength", "13");
            }
            else if ($tipo == 'C') {
                $identidad.attr("type", "number");
                $identidad.val($value.substring(0, 10));
                $identidad.attr("data-validate", "number,minlength[10],maxlength[10]");
                $identidad.attr("maxlength", "10");
                $identidad.attr("minlength", "10");
            }
            else {
                $identidad.attr("type", "text");
                $identidad.attr("data-validate", "number,minlength[4],maxlength[50]");
                $identidad.attr("minlength", "4");
                $identidad.attr("maxlength", "50");
            }
        };


        //$(".fileinput").fileinput();
        $(".tipo-identidad").on("change", validaTipo);

        $(".valor-identidad").on('change', function (e) {
            
            var $form = $("#crearClienteForm");

            /* if ($form.valid()) {*/



            var $divModal = $($form).closest('div');
            var identidad = $(".valor-identidad").val();
            
           

            if (identidad)

                blockUI($divModal);

                $.ajax({
                    url: MantCliente.urlConsultaCliente,
                    method: "POST",
                    data: {
                        'identidad': identidad
                    },
                    beforeSend: function () {
                       
                        $divModal.addClass('loading');
                    },
                    complete: function () {
                        
                        $divModal.removeClass('loading');
                    }
                }).done(function (respuesta) {
                    if (respuesta != "") {
                        if (respuesta.success == true) {
                            var client = respuesta.client;
                            console.log(respuesta.client)
                            console.log(client)

                            $("#RazonSocial").val(respuesta.client.RazonSocial);
                            $("#Direccion").val(respuesta.client.Direccion);
                            $("#Telefono").val(respuesta.client.Telefono);
                            $("#Celular").val(respuesta.client.Celular);
                            $("#Correo").val(respuesta.client.Correo);
                            $("#EstadoCivil").val(respuesta.client.EstadoCivil);
                            $("#NivelEstudio").val(respuesta.client.NivelEstudio);
                            $("#Profesion").val(respuesta.client.Profesion);
                            $("#IdPais").val(1);
                            $("#IdPais").change();

                            $("#IdProvincia").val(9);
                            $("#IdProvincia").change();

                            var fechaNac = null;

                            if (respuesta.client.FechaNacimiento != null
                                && respuesta.client.FechaNacimiento != '') {

                                fechaNac = new Date(respuesta.client.FechaNacimiento.replace("/Date(", "").replace(")/", "") * 1)
                                    .toISOString()
                                    .substring(0, 10);

                                $("#FechaNacimiento").val(fechaNac);
                            }

                            /*$("#IdPais").val();*/
                        }
                    }

                    unblockUI($divModal);
                }).fail(function (e) {
                    unblockUI($divModal);
                    console.log(e);
                    //debugger;
                }) 
                /* }*/
        });

        $("#btnCrearCliente").on("click", function (event) {
            var $form = $("#crearClienteForm");

            if ($form.valid()) {
                var $div = $($form).closest('div');
                var post_url = $($form).attr("action");
                var request_method = $($form).attr("method");
                //var form_data = new FormData($form[0]);
                var form_data = $form.serializeArray();

                $.ajax({
                    url: post_url,
                    method: request_method,
                    data: form_data,
                    //contentType: false,
                    //processData: false,
                    beforeSend: function () {
                        blockUI($div)
                        $div.addClass('loading');
                    },
                    complete: function () {
                        unblockUI($div)
                        $div.removeClass('loading');
                    }
                }).done(function (respuesta) {
                    //debugger;
                    if (respuesta != "") {
                        if (respuesta.success == true) {
                            toastr.success("Cliente guardado correctamente", "", optionsToastr);
                            console.log(respuesta.client);
                            var client = respuesta.client;
                            setClientData(client);
                            $("#IdCliente").val(client.Id);
                            $("#divModal").modal('hide');
                        }
                        else {
                            toastr.error(respuesta.mensaje, "", optionsToastr);
                            console.log(respuesta);
                        }
                    }
                }).fail(function (e) {
                    console.log(e);
                    //debugger;
                });
            }
        });

        $("#IdPais").change(function () {
            var $provinciasDropDown = $("#IdProvincia");
            $provinciasDropDown.empty();
            var op = new Option("Seleccione una provincia", "");
            $provinciasDropDown.append(op);
            $provinciasDropDown.attr("disabled", "disabled");

            var $ciudadesDropDown = $("#IdCiudad");
            $ciudadesDropDown.empty();

            var oc = new Option("Seleccione una ciudad", "");
            $ciudadesDropDown.append(oc);
            $ciudadesDropDown.attr("disabled", "disabled");

            var $sectorDropdown = $("#IdSector");
            $sectorDropdown.val(null);
            $sectorDropdown.attr("disabled", "disabled");

            if ($("#IdPais").val()) {
                MantCliente.idPais = $("#IdPais").val();

                $.ajax({
                    url: MantCliente.urlProvincias,
                    type: 'get',
                    data: { idPais: MantCliente.idPais }
                }).done(function (respuesta) {
                    if (respuesta.length >= 0) {
                        $provinciasDropDown.removeAttr("disabled");
                        $.each(respuesta, function (index, item) {
                            var p = new Option(item.Provincia, item.Id);
                            $provinciasDropDown.append(p);
                        });

                        if (MantCliente.idProvincia) {
                            $provinciasDropDown.val(MantCliente.idProvincia);
                            $provinciasDropDown.trigger("change");
                        }
                    }
                });
            }
        });



        $("#IdProvincia").change(function () {
            var $ciudadesDropDown = $("#IdCiudad");
            $ciudadesDropDown.empty();
            var oc = new Option("Seleccione una ciudad", "");
            $ciudadesDropDown.append(oc);
            $ciudadesDropDown.attr("disabled", "disabled");

            if ($("#IdProvincia").val()) {
                MantCliente.idProvincia = $("#IdProvincia").val();

                $.ajax({
                    url: MantCliente.urlCiudades,
                    type: 'get',
                    data: { idProvincia: MantCliente.idProvincia }
                }).done(function (respuesta) {
                    if (respuesta.length >= 0) {
                        $ciudadesDropDown.removeAttr("disabled");
                        $.each(respuesta, function (index, item) {
                            var p = new Option(item.Ciudad, item.Id);
                            $ciudadesDropDown.append(p);
                        });

                        if (MantCliente.idCiudad) {
                            $ciudadesDropDown.val(MantCliente.idCiudad);
                            $ciudadesDropDown.trigger("change");
                        }
                    }
                });
            }
        });

        $("#IdCiudad").change(function () {
            var $sector = $("#IdSector");
            $sector.attr("disabled", "disabled");

            if ($(this).val()) {
                $sector.removeAttr("disabled");
            }

            if (MantCliente.idSector) {
                $sector.val(MantCliente.idSector);
                $sector.trigger("change");
            }
        });

        validaTipo();

        if (MantCliente.idPais) {
            $("#IdPais").trigger("change");
        }
    }
}
