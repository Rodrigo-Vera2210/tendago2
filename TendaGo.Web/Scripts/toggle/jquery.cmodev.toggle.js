(function() {
    $.fn.cmodevCheckToogle = function(options) {
        var settings = $.extend({
            on_text: 'Activo',
            off_text:'Inactivo',
            size: 'small',
            on_color_class: 'success',
            off_color_class:'danger'
        }, options);
        return this.bootstrapToggle({
            on: settings.on_text,
            off: settings.off_text,
            size: settings.size,
            onstyle: settings.on_color_class,
            offstyle: settings.off_color_class
        });
    };
}(jQuery));