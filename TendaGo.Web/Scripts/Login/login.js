

var formLogin = formLogin || {};

; (function ($, window, undefined) {
    "use strict";

    $(document).ready(function () {
        formLogin.$container = $("#form_login");

        var $settings = $.data($("form")[0], "validator").settings;

        $settings.highlight = function (element) {
            $(element).closest('.input-group').addClass('validate-has-error');
        };

        $settings.unhighlight = function (element) {
            $(element).closest('.input-group').removeClass('validate-has-error');
        };

        $settings.submitHandler = function (ev) {

            $(".login-page").addClass('logging-in'); // This will hide the login form and init the progress bar

            // Hide Errors
            $(".form-login-error").slideUp('fast');

            // We will wait till the transition ends				
            setTimeout(function () {
                var random_pct = 25 + Math.round(Math.random() * 30);

                // The form data are subbmitted, we can forward the progress to 70%
                formLogin.setPercentage(40 + random_pct);

                var post_url = formLogin.$container.attr("action");
                var request_method = formLogin.$container.attr("method");
                var form_data = formLogin.$container.serialize();

                // Send data to the server
                $.ajax({
                    url: post_url,
                    method: request_method,
                    data: form_data,
                    error: function (e,a,x) {
                        console.log(e);
                        console.log(a);
                        alert("An error occoured!");
                    },
                    success: function (response) {

                        // Login status [success|invalid]
                        var login_redirect = response.redirect;

                        // Form is fully completed, we update the percentage
                        formLogin.setPercentage(100);

                        // We will give some time for the animation to finish, then execute the following procedures	
                        setTimeout(function () {
                            // If login is invalid, we store the 
                            if (login_redirect == false) {
                                $(".login-page").removeClass('logging-in');
                                formLogin.resetProgressBar(true);
                            }
                            else {
                                if (login_redirect == true) {

                                    // Redirect to login page
                                    setTimeout(function () {
                                        var redirect_url = baseurl;

                                        if (response.redirectUrl && response.redirectUrl.length) {
                                            redirect_url = response.redirectUrl;
                                        }

                                        window.location.href = redirect_url;
                                    }, 400);
                                }
                            }
                        }, 1000);
                    }
                });


            }, 650);
        };

        //// Login Form & Validation
        //formLogin.$container.validate({
        //    //rules: {
        //    //    username: {
        //    //        required: true
        //    //    },

        //    //    password: {
        //    //        required: true
        //    //    },

        //    //},

        //    //highlight: function (element) {
        //    //    $(element).closest('.input-group').addClass('validate-has-error');
        //    //},

        //    //unhighlight: function (element) {
        //    //    $(element).closest('.input-group').removeClass('validate-has-error');
        //    //},

        //    submitHandler: function (ev) {

        //        $(".login-page").addClass('logging-in'); // This will hide the login form and init the progress bar

        //        // Hide Errors
        //        $(".form-login-error").slideUp('fast');

        //        // We will wait till the transition ends				
        //        setTimeout(function () {
        //            var random_pct = 25 + Math.round(Math.random() * 30);

        //            // The form data are subbmitted, we can forward the progress to 70%
        //            formLogin.setPercentage(40 + random_pct);

        //            var post_url = formLogin.$container.attr("action");
        //            var request_method = formLogin.$container.attr("method");
        //            var form_data = formLogin.$container.serialize();

        //            // Send data to the server
        //            $.ajax({
        //                url: post_url,
        //                method: request_method,
        //                data: form_data,
        //                error: function () {
        //                    alert("An error occoured!");
        //                },
        //                success: function (response) {
        //                    // Login status [success|invalid]
        //                    var login_status = response.login_status;

        //                    // Form is fully completed, we update the percentage
        //                    formLogin.setPercentage(100);


        //                    // We will give some time for the animation to finish, then execute the following procedures	
        //                    setTimeout(function () {
        //                        // If login is invalid, we store the 
        //                        if (login_status == 'invalid') {
        //                            $(".login-page").removeClass('logging-in');
        //                            formLogin.resetProgressBar(true);
        //                        }
        //                        else
        //                            if (login_status == 'success') {
        //                                // Redirect to login page
        //                                setTimeout(function () {
        //                                    var redirect_url = baseurl;

        //                                    if (response.redirect_url && response.redirect_url.length) {
        //                                        redirect_url = response.redirect_url;
        //                                    }

        //                                    window.location.href = redirect_url;
        //                                }, 400);
        //                            }

        //                    }, 1000);
        //                }
        //            });


        //        }, 650);
        //    }
        //});


        var is_lockscreen = $(".login-page").hasClass('is-lockscreen');

        //// Lockscreen & Validation
        //var is_lockscreen = $(".login-page").hasClass('is-lockscreen');

        //if (is_lockscreen) {
        //    formLogin.$container = $("#form_lockscreen");
        //    formLogin.$ls_thumb = formLogin.$container.find('.lockscreen-thumb');

        //    formLogin.$container.validate({
        //        rules: {

        //            password: {
        //                required: true
        //            },

        //        },

        //        highlight: function (element) {
        //            $(element).closest('.input-group').addClass('validate-has-error');
        //        },


        //        unhighlight: function (element) {
        //            $(element).closest('.input-group').removeClass('validate-has-error');
        //        },

        //        submitHandler: function (ev) {
        //            /* 
		//				Demo Purpose Only 
						
		//				Here you can handle the page login, currently it does not process anything, just fills the loader.
		//			*/

        //            $(".login-page").addClass('logging-in-lockscreen'); // This will hide the login form and init the progress bar

        //            // We will wait till the transition ends				
        //            setTimeout(function () {
        //                var random_pct = 25 + Math.round(Math.random() * 30);

        //                formLogin.setPercentage(random_pct, function () {
        //                    // Just an example, this is phase 1
        //                    // Do some stuff...

        //                    // After 0.77s second we will execute the next phase
        //                    setTimeout(function () {
        //                        formLogin.setPercentage(100, function () {
        //                            // Just an example, this is phase 2
        //                            // Do some other stuff...

        //                            // Redirect to the page
        //                            setTimeout("window.location.href = '../../'", 600);
        //                        }, 2);

        //                    }, 820);
        //                });

        //            }, 650);
        //        }
        //    });
        //}






        // Login Form Setup
        formLogin.$body = $(".login-page");
        formLogin.$login_progressbar_indicator = $(".login-progressbar-indicator h3");
        formLogin.$login_progressbar = formLogin.$body.find(".login-progressbar div");

        formLogin.$login_progressbar_indicator.html('0%');

        if (formLogin.$body.hasClass('login-form-fall')) {
            var focus_set = false;

            setTimeout(function () {
                formLogin.$body.addClass('login-form-fall-init')

                setTimeout(function () {
                    if (!focus_set) {
                        formLogin.$container.find('input:first').focus();
                        focus_set = true;
                    }

                }, 550);

            }, 0);
        }
        else {
            formLogin.$container.find('input:first').focus();
        }

        // Focus Class
        formLogin.$container.find('.form-control').each(function (i, el) {
            var $this = $(el),
				$group = $this.closest('.input-group');

            $this.prev('.input-group-addon').click(function () {
                $this.focus();
            });

            $this.on({
                focus: function () {
                    $group.addClass('focused');
                },

                blur: function () {
                    $group.removeClass('focused');
                }
            });
        });

        // Functions
        $.extend(formLogin, {
            setPercentage: function (pct, callback) {
                pct = parseInt(pct / 100 * 100, 10) + '%';

                // Lockscreen
                if (is_lockscreen) {
                    formLogin.$lockscreen_progress_indicator.html(pct);

                    var o = {
                        pct: currentProgress
                    };

                    TweenMax.to(o, .7, {
                        pct: parseInt(pct, 10),
                        roundProps: ["pct"],
                        ease: Sine.easeOut,
                        onUpdate: function () {
                            formLogin.$lockscreen_progress_indicator.html(o.pct + '%');
                            drawProgress(parseInt(o.pct, 10) / 100);
                        },
                        onComplete: callback
                    });
                    return;
                }

                // Normal Login
                formLogin.$login_progressbar_indicator.html(pct);
                formLogin.$login_progressbar.width(pct);

                var o = {
                    pct: parseInt(formLogin.$login_progressbar.width() / formLogin.$login_progressbar.parent().width() * 100, 10)
                };

                TweenMax.to(o, .7, {
                    pct: parseInt(pct, 10),
                    roundProps: ["pct"],
                    ease: Sine.easeOut,
                    onUpdate: function () {
                        formLogin.$login_progressbar_indicator.html(o.pct + '%');
                    },
                    onComplete: callback
                });
            },

            resetProgressBar: function (display_errors) {
                TweenMax.set(formLogin.$container, { css: { opacity: 0 } });

                setTimeout(function () {
                    TweenMax.to(formLogin.$container, .6, {
                        css: { opacity: 1 }, onComplete: function () {
                            formLogin.$container.attr('style', '');
                        }
                    });

                    formLogin.$login_progressbar_indicator.html('0%');
                    formLogin.$login_progressbar.width(0);

                    if (display_errors) {
                        var $errors_container = $(".form-login-error");

                        $errors_container.show();
                        var height = $errors_container.outerHeight();

                        $errors_container.css({
                            height: 0
                        });

                        TweenMax.to($errors_container, .45, {
                            css: { height: height }, onComplete: function () {
                                $errors_container.css({ height: 'auto' });
                            }
                        });

                        // Reset password fields
                        formLogin.$container.find('input[type="password"]').val('');
                    }

                }, 800);
            }
        });


        // Lockscreen Create Canvas
        if (is_lockscreen) {
            formLogin.$lockscreen_progress_canvas = $('<canvas></canvas>');
            formLogin.$lockscreen_progress_indicator = formLogin.$container.find('.lockscreen-progress-indicator');

            formLogin.$lockscreen_progress_canvas.appendTo(formLogin.$ls_thumb);

            var line_width = 3,
				thumb_size = formLogin.$ls_thumb.width() + line_width;

            formLogin.$lockscreen_progress_canvas.attr({
                width: thumb_size,
                height: thumb_size
            }).css({
                top: -line_width / 2,
                left: -line_width / 2
            });


            formLogin.lockscreen_progress_canvas = formLogin.$lockscreen_progress_canvas.get(0);

            // Create Progress Circle
            var bg = formLogin.lockscreen_progress_canvas,
				ctx = ctx = bg.getContext('2d'),
				imd = null,
				circ = Math.PI * 2,
				quart = Math.PI / 2,
				currentProgress = 0;

            ctx.beginPath();
            ctx.strokeStyle = '#eb7067';
            ctx.lineCap = 'square';
            ctx.closePath();
            ctx.fill();
            ctx.lineWidth = line_width;

            imd = ctx.getImageData(0, 0, thumb_size, thumb_size);

            var drawProgress = function (current) {
                ctx.putImageData(imd, 0, 0);
                ctx.beginPath();
                ctx.arc(thumb_size / 2, thumb_size / 2, 70, -(quart), ((circ) * current) - quart, false);
                ctx.stroke();

                currentProgress = current * 100;
            }

            drawProgress(0 / 100);


            formLogin.$lockscreen_progress_indicator.html('0%');

            ctx.restore();
        }

    });

})(jQuery, window);