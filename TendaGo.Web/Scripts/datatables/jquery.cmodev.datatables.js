/*
    Plugin javascript para control DataTable
    Dependencias : jquery.dataTables.js - 1.10.0
*/
var language = { "processing": "Procesando...", "lengthMenu": "Ver _MENU_ registros", "zeroRecords": "No se encontraron resultados", "info": "Registros del _START_ al _END_ - Total _TOTAL_ registros", "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros", "infoFiltered": "(filtrado de un total de _MAX_ registros)", "infoPostFix": "", "search": "Buscar:", "url": "", "loadingRecords": "Cargando...", "paginate": { "first": "Primero", "last": "Último", "next": "Siguiente", "previous": "Anterior" }, "aria": { "sortAscending": ": Activar para ordenar la columna de manera ascendente", "sortDescending": ": Activar para ordenar la columna de manera descendente" } };

(function () {
    
    $.fn.basicDatatable = function () {
        return this.DataTable({
            responsive: true,
            "jQueryUI": false,
            "info": false,
            'bLengthChange': false,
            searching: false,
            paging:false,
            //"sSearchPlaceholder": "busqueda..",
            "language": language,
            "iDisplayLength": 10
        });
    };

    $.fn.datatableNoSortColumn = function (options) {
        var settings = $.extend({
            column_no_sort: 0
        }, options);
        return this.dataTable({
            responsive: true,
            info: true,
            "jQueryUI": false,
            "sSearchPlaceholder": "busqueda..",
            "language": language,
            "iDisplayLength": 10,
            "lengthMenu": [[5, 10, 50, -1], [5, 10, 50, "Todos"]],
            "columnDefs": [{ "targets": settings.column_no_sort, "orderable": false }]
        });
    };

    $.fn.vscrollableDatatable = function() {

    };

    // Plugin para convertir el elemento html <table> en un grid jQuery Datatables simple, sólo con busqueda y pagineo. Sin combo de longuitud de paginas, ni informacion de registros
    $.fn.DataTableSimple = function () {
        return this.dataTable({
            "jQueryUI": false,
            "info": false,
            'bLengthChange': false,
            'iDisplayLength': '10',
            'scrollY': '200',
            'language': language
        });
    };
    
}(jQuery));