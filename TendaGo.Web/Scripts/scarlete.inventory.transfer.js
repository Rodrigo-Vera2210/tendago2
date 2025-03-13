var onSelectProduct;
var idOrigen = 4;
var $btnBuscarProducto;
var productoSeleccionado;
var Transfer = function () {

    var processing = false,
        cargar_vendidos = 0,
        main = function (preload) {
            cargar_vendidos = preload;

            onSelectProduct = on_product_selected;

            $(".input-money").maskAsDecimal();

            $("#Fecha").datepickercustom();

            //$("#btnNuevoDetalle").on("click", find_product);

            $(".delete-button").on("click", handle_delete);

            $("#transferForm").on("submit", handle_form);

            $(".local-control").select();

            //configure_producto();
            $('.btnBuscarProducto').on('click', find_product);

            $("#btnNuevoDetalle").on("click", add_detail);

            // var detalles = $("#tablaDetalle > tbody > tr");

            // var percent = (detalles.length > 0 ? (100 / detalles.length) : 0);
            // show_loading_bar(1);

            // var handler;
            // for (var i = 0; i < detalles.length; i++) {

                // handler = set_product($(detalles[i]));
                // show_loading_bar(parseFloat(i * percent));

            // }

            // show_loading_bar(100);

        }, on_product_selected = function (data) {

            console.log("onSelectProduct" + data);
            var pro = JSON.parse(data);
            productoSeleccionado = pro;

            var $tr = $btnBuscarProducto.closest('tr');
            var $td = $tr.find('td');
            $td.find('.IdProducto').val(pro.Id);
            $td.find('.NombreProducto').val(pro.CodigoInterno + " " + pro.Nombre);

            set_product($tr)

        }, set_product = function ($tr) {

            var $id_producto = $tr.find('.IdProducto').val();
            $tr.find(".IdLocal-control").empty();
            $tr.find(".IdTipoUnidad-control").empty();
            $tr.find(".Cantidad-control").prop("max", 0);
            $tr.find(".Cantidad-control").prop("max-value", 0);
            $tr.find(".Cantidad-control").prop("min", 0);
            $tr.find(".Cantidad-control").prop("min-value", 0);

            //$data = {
            //    "Id": 1365,
            //    "CodigoInterno": "00-66DUONUT",
            //    "CodigoProveedor": null,
            //    "Nombre": "TINTE CAPILAR ROJO INTENSO 2 UND 35 ml."
            //}

            var ajaxStock, ajaxLocal, ajaxUnits;

            // 1. Cargamos el stock actual
             ajaxStock = $.get(`/bodega/ObtenerStock?idProducto=${$id_producto}`, function ($stock) {
                $tr.find(".Stock-control").find("input").val($stock);
                $tr.find(".Stock-control").find("span").text($stock);
            

            });

            // 2. Cargamos el stock por locales destino
            ajaxLocal = $.get(`/inventario/GetLocales/${$id_producto}`, function ($locales) {
                var $ctrl = $tr.find(".IdLocal-control");
                $ctrl.empty();

                if ($locales.length > 0) {
                    var maxFirst = $locales[0].StockInventario || 0;
                    $tr.find(".Cantidad-control").prop("max", maxFirst);
                    $tr.find(".Cantidad-control").prop("max-value", maxFirst);

                    for (var i = 0; i < $locales.length; i++) {
                        var $local = $locales[i];
                        $ctrl.append(`<option value="${$local.IdLocal}" data-stock="${$local.StockInventario || 0}"  >${$local.Local}   |  Stock: ${$local.StockInventario || 0}</option>`);
                    }

                }
                else {
                    $ctrl.append(`<option data-stock="0"  >No hay stock en otros locales</option>`);
                } 
            });

            // 3. Cargamos las unidades de medida
            ajaxUnits = $.get(`/productos/SearchUnitTypes/${$id_producto}`, function ($unidades) {
                var $ctrl = $tr.find(".IdTipoUnidad-control");
                $ctrl.empty();
                if ($unidades.length > 0) {
                    var minFirst = $unidades[0].CantidadMinima || 0;
                    $tr.find(".Cantidad-control").prop("min", minFirst);
                    $tr.find(".Cantidad-control").prop("min-value", minFirst);

                    if ($tr.find(".Cantidad-control").val() < minFirst) {
                        $tr.find(".Cantidad-control").val(minFirst);
                    }

                    for (var i = 0; i < $unidades.length; i++) {
                        var $unidad = $unidades[i];
                        $ctrl.append(`<option value="${$unidad.Id}" data-cantidad="${$unidad.CantidadMinima || 0}" >${$unidad.TipoUnidad}   |  Min: ${$unidad.CantidadMinima || 0}</option>`);
                    }
                }
            });

            return $.when(ajaxStock, ajaxLocal, ajaxUnits);

        }, load_product = function () {
            var $div = $("#productDetail")
            blockUI($div)
            $div.addClass('loading');

            var idProduct = $(this).val();

            if (idProduct > 0) $("#productDetail").load(Transfer.GetItemStocksUrl + "/" + idProduct, function () {
                var $div = $("#productDetail")

                $div.removeClass('loading');
                unblockUI($div)

                $(".input-spinner-stock").spinnercustom();
            });
            else {
                $div.removeClass('loading');
                unblockUI($div)
            }

        }, find_product = function (e) {
            $btnBuscarProducto = $(this);

            $('#divConsultaProductosModal').empty();
            $('#divConsultaProductosModal').modal('show', { backdrop: 'static' });
            getSearchProductsModal(idOrigen);
              
        }, removeFromList = function (idProducto, idLocal) {
            var list = $("input[value='" + idProducto + "'].producto-control").parents("tr");
            
            for (var x = 0; x < list.length; x++) {
                var row = $(list[x]);
                
                if (row.find("input[class='local-control']").val() == idLocal) {
                    row.remove();
                }
            }

        }, add_detail = function () {

            var $tab = $("#tablaDetalle > tbody");

            var $last = $tab.find("tr").last();
            var $id = 0;

            if ($last) {
                $id = $last.prop("id");
                if (!$id) {
                    $id = $tab.find("tr").length;
                }
            }

            // Ahora configuramos los controles.
            // Control Producto 

            var ctrlProducto = crear_control_producto($id);
            var ctrlCantidad = crear_control_cantidad($id);
            var ctrlStock = crear_control_stock($id);
            var ctrlUnidad = crear_control_select($id, "IdTipoUnidad");
            var ctrlLocal = crear_control_select($id, "IdLocal");
            var ctrlDelete = crear_control_delete($id);

            var tr = $("<tr id='" + $id + "'></tr>");
            tr.append($("<td></td>").append(ctrlProducto));
            if (cargar_vendidos) {
                tr.append($("<td></td>").append("0"));
            }
            tr.append($("<td></td>").append(ctrlStock));
            tr.append($("<td></td>").append(ctrlLocal));
            tr.append($("<td></td>").append(ctrlCantidad));
            tr.append($("<td></td>").append(ctrlUnidad));
            tr.append($("<td></td>").append(ctrlDelete));

            $tab.append(tr);
            ctrlLocal.on("change", function () {
                var $opt = $(this).find("option:selected");
                var max = $opt.attr("data-stock");

                var $tr = $(this).closest("tr");
                $tr.find(".Cantidad-control").attr("max", max || 0);
                $tr.find(".Cantidad-control").attr("max-value", max || 0);
            });

            ctrlUnidad.on("change", function () {
                var $opt = $(this).find("option:selected");
                var min = $opt.attr("data-cantidad");

                var $tr = $(this).closest("tr");
                $tr.find(".Cantidad-control").attr("min", min || 0);
                $tr.find(".Cantidad-control").attr("min-value", min || 0);
                if ($tr.find(".Cantidad-control").val() < (min || 0)) {
                    $tr.find(".Cantidad-control").val(min || 0); 
                }
            });

            ctrlProducto.find("button").click(find_product);                 

        }, crear_control_cantidad = function (id) {
            var ctrl = $(`<input type="number" class="Cantidad-control form-control" data-min="0" data-val="true" data-val-number="El campo cantidad debe tener un valor numerico." 
                            data-val-required="El campo cantidad es campo requerido." max="0" max-value="0" min="0" min-value="0"
                            id="Detalle_${id}__Cantidad" name="Detalle[${id}].Cantidad" type="text" value="0">`);
            ctrl.spinnercustom();
            return ctrl;

        }, crear_input_group = function (id, nombrePropiedadOculta, nombrePropiedad, nombreBotonBusqueda) {
            var input = "<div class='input-group' style='width:100%;'>";
            input += "<input class='" + nombrePropiedadOculta + "' id='Detalle_" + id + "__" + nombrePropiedadOculta + "' name='Detalle[" + id + "]." + nombrePropiedadOculta + "' type='hidden' >";
            input += "<input class='form-control " + nombrePropiedad + "' id='Detalle_" + id + "__" + nombrePropiedad + "' name='Detalle[" + id + "]." + nombrePropiedad + "' readonly='readonly' type='text' >";
            input += "<div class='input-group-btn'>";
            input += "<button type='button' class='btn btn-blue " + nombreBotonBusqueda + "'>";
            input += "<i class='entypo-search'></i>";
            input += "</button>";
            input += "</div>";
            input += "</div>";
            return $(input);

        }, crear_control_stock = function (id, stock) {
            stock = stock || 0;
            return $(`<div class="input-group Stock-control"><input type="hidden" name="Detalle[${id}].Stock" value="${stock}" /><span>${stock}</span> </div>`);

        }, crear_control_producto = function (id) {

            return crear_input_group(id, "IdProducto", "NombreProducto", "find-product");

        }, crear_control_select = function (id, field) {
            return $(`<select id = "Detalle_${id}__${field}" name = "Detalle[${id}].${field}" class="form-control ${field}-control" ></select>`);

        }, crear_control_local = function (id, idLocal, local) {
            return $(`<div class="input-group"> <input id = "Detalle_${id}__IdLocal" name = "Detalle[${id}].IdLocal" class="local-control" type="hidden" value="${idLocal}"/><span>${local}</span> </div>`);

        }, crear_control_delete = function (id) {
            var ctrl = $(`<a href="#" class="btn delete-button"><i class="fa fa-trash"></i></a>`);
            ctrl.on("click", handle_delete);
            return ctrl;

        }, crear_control_unidad = function (id) {
            return $(`<select id = "Detalle_${id}__IdTipoUnidad" name = "Detalle[${id}].IdTipoUnidad" class="unidad-control form-control" style="width:100%" required> </select>`);

        }, handle_delete = function () {
            $(this).parentsUntil("", "tr").remove();

        }, configure_producto = function () {

            var $idLocal = $("#IdLocalOrigen").val();
            var $selector = $("#productSelector");

            $selector.on("change select2:select", load_product);

            return $selector.select2({
                ajax: {
                    url: Transfer.ProductServiceUrl,
                    type: 'GET',
                    dataType: 'json',
                    async: true,
                    data: function (params) {
                        return {
                            idLocal : $idLocal,
                            searchTerm: params.term // search term
                        };
                    },
                    cache: true
                },
                width: 'resolve', // need to override the changed default
                //theme: 'classic',
                templateResult: format_product,
                allowClear: true,
                placeholder: 'Seleccione un producto... ',
                minimumInputLength: 1
            });
        }, format_product = function (repo) {
            if (repo.loading || !repo.data) {
                return repo.text;
            }

            
            var $container = $(
                "<div class='select2-result-repository clearfix row'>" +
                    "<div class='select2-result-repository__avatar col-md-2'>" +
                        "<img style='width:64px; height:64px;' src='" + Transfer.PhotoUrl + "?path=" + repo.photo + "' />" +
                    "</div>" +

                    "<div class='select2-result-repository__meta col-md-6'>" +
                        "<div class='select2-result-repository__title text-bold col-md-12'></div>" +
                        "<div class='select2-result-repository__description col-md-12'></div>" +
                        "<div class='select2-result-repository__marca col-md-12'> </div>" +
                    "</div>" +

                "</div>"
            );

            $container.find(".select2-result-repository__title").text(repo.data.Producto);
            $container.find(".select2-result-repository__description").text(repo.data.Descripcion);
            $container.find(".select2-result-repository__marca").append(repo.data.Marca);
            
            return $container;
            
        }, configure_unidad = function (control, id) {

            control.empty();

            $.ajax({
                url: Transfer.UnitTypeServiceUrl + "/" + id,
                type: 'GET',
                dataType: 'json',
                async: true,
                cache: true
            }).done(function (data) {

                control.empty();

                if (data && data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        var item = data[i];
                        control.append($("<option value='" + item.Id + "'>" + item.TipoUnidad + "</option>"))
                    }
                }

                control.select2({
                    width: 'resolve', // need to override the changed default
                    //theme: 'classic',
                    placeholder: 'Seleccione una unidad... '
                });
            });
        }, handle_form = function (e) {
            e.preventDefault();
            if (processing) {
                return false;
            }

            // Configuramos la grabación
            processing = true;

            var $div = $(".main-content");
            var $form = $("#transferForm");
            var $url = $form.prop("action");
            var $method = $form.prop("method");
            var $data = $form.serialize();

            $.ajax({
                url: $url,
                type: $method,
                data: $data, 
                processData: false,
                cache: false,
                beforeSend: function () {
                    blockUI($div)
                    $div.addClass('loading');
                },
                complete: function () {
                    unblockUI($div)
                    $div.removeClass('loading');
                }

            }).done(function (result) {
                if (result != "") {
                    if (result.success == true) {
                        console.log(result);

                        toastr.success(result.message, "Guardar", optionsToastr);

                        location.href = "/inventario/resumen/" + result.id;
                        //location.reload();
                    }
                    else {
                        console.log(result);
                        toastr.warning(result.message, "Error", optionsToastr);
                    }
                }
                else {
                    toastr.warning("Ocurrio un error al guardar la solicitud", "Error");
                }
            }).always(function () {
                unblockUI($div)
                $div.removeClass('loading');
                processing = false;
            });;


        };
    
    return {
        GetItemStocksUrl:'',
        ProductServiceUrl: '',
        UnitTypeServiceUrl: '',
        HomeUrl: '',
        PhotoUrl: '',
        Locales:[],
        Init: function (preload) {
            main(preload);
        }
    };
}();





