(function () {

    $.fn.maskAsDecimal = function(options) {
        var settings = $.extend({
            precision: 2
        }, options);
        return this.maskMoney({
            thousands: '',
            decimal: '.',
            allowZero: true,
            precision: settings.precision
        }).maskMoney('mask');
    };
}(jQuery));