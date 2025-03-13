var product_config = {
    edit: {
        url:''
    },
    deleteUrl: '',
    createUrl:''
};
var searchProductsControl= {
    url: '',
    resultContainer: ''
};

var searchModalSettings = {
    url: '',
    resultContainer:''
};

function getSearchControl(origen) {
    show_loading_bar(50);
    var $div = $("#" + searchProductsControl.resultContainer);
    $.ajax({
        url: searchProductsControl.url,
        type: "GET",
        data: { idOrigen: origen },
        beforeSend: function () {
            $div.empty();
            blockUI($div);
            $div.addClass('loading');
            $div.append('<div id="divLoading" class="overlay"><i class="fa fa-refresh fa-spin"></i></div>');
        },
        complete: function () {
            unblockUI($div);
            $div.removeClass('loading');
        },
        success: function (dataReceived) {
            if (dataReceived != "") {
                $("#" + searchProductsControl.resultContainer).html(dataReceived);
            }
        }
    }).done(function (dataResponse) {
        show_loading_bar(100);
    });
}

function getSearchProductsModal(origen) {
    var $div = $(`#${searchModalSettings.resultContainer}`);
    $.ajax({
        url: searchModalSettings.url,
        type: "GET",
        data: { idOrigen: origen },
        beforeSend: function () {
            $div.empty();
            blockUI($div);
            $div.addClass('loading');
            $div.append('<div id="divLoading" class="overlay"><i class="fa fa-refresh fa-spin"></i></div>');
        },
        complete: function () {
            unblockUI($div);
            $div.removeClass('loading');
        },
        success: function (dataReceived) {
            if (dataReceived !== "") {
                $(`#${searchModalSettings.resultContainer}`).html(dataReceived);
            }
        }
    });
}

function handleParentProducts($url) {

    $(".producto-padre").select2({
        ajax: {
            url: $url,
            type: "GET",
            dataType: 'json',
            async: true,
            data: function (params) {
                return {
                    searchTerm: params.term // search term
                };
            },
            cache: true
        },
        allowClear: true,
        placeholder: 'Seleccione un producto... ',
        minimumInputLength: 1 
    });
}
 