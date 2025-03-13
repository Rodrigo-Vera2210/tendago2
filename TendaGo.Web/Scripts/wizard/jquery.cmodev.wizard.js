(function () {

    $.fn.importPurchaseWizard = function (options) {
        var settings = $.extend({
            nextValidation: function ($tab, $navigation, index) {
                //console.log("tab: " + JSON.stringify($tab));
                //console.log("navigation:" + JSON.stringify($navigation));
                console.log("index: " + index);
                return true;
            }
        }, options);

        var $this = $(this);
        $progress = $this.find(".steps-progress div");
        _index = $this.find('> ul > li.active').index();

        $this.bootstrapWizard({
            tabClass: "",
            onTabShow: function ($tab, $navigation, index) {
                var $total = $navigation.find('li').length;
                var $current = index + 1;
                setCurrentProgressTab($this, $navigation, $tab, $progress, index);
                if ($total === $current) {
                    //$('.wizard li.finish button').prop('disabled', false);
                    $('.wizard li.finish button').fadeIn('fast');
                }
                else {
                    //$('.wizard li.finish button').prop('disabled', true);
                    $('.wizard li.finish button').fadeOut('fast');
                }
            },
            onNext: settings.nextValidation,
            onTabClick: function ($tab, $navigation, index) {
                console.log("tab: " + $tab + " navigation:" + JSON.stringify($navigation) + " index: " + index);
                return false;
            }
        });
        $this.data('bootstrapWizard').show(_index);
    };

    $.fn.purchaseWizard = function(options) {
        var settings = $.extend({
            nextValidation: function ($tab, $navigation, index) {
                console.log("index: " + index);
                return true;
            }
        }, options);

        var $this = $(this);

        // Verifico si existe algun tipo de wizard
        if ($this.length == 0) {
            return;
        }


        $progress = $this.find(".steps-progress div");
        _index = $this.find('> ul > li.active').index();

        $this.bootstrapWizard({
            tabClass: "",
            onTabShow: function ($tab, $navigation, index) {
                var $total = $navigation.find('li').length;
                var $current = index + 1;
                setCurrentProgressTab($this, $navigation, $tab, $progress, index);
                if ($total === $current) {
                    $('.wizard li.finish button').prop('disabled', false);
                    //$('.wizard li.finish button').fadeIn('fast');
                }
                else {
                    $('.wizard li.finish button').prop('disabled', true);
                    //$('.wizard li.finish button').fadeOut('fast');
                }
            },
            onNext: settings.nextValidation,
            onTabClick: function ($tab, $navigation, index) {
                console.log("tab: " + $tab + " navigation:" + JSON.stringify($navigation) + " index: " + index);
                return false;
            }
        });

        $this.data('bootstrapWizard').show(_index);
    };

}(jQuery));