using System.Web;
using System.Web.Optimization;

namespace TendaGo.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.kontrol.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.serializejson.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Msodernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/responsive")
                .Include("~/Scripts/respond.js"));

#if DEBUG
            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
               .Include("~/Scripts/bootstrap.js"));
#else
            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                    .Include("~/Scripts/bootstrap.min.js"));
#endif


            bundles.Add(new ScriptBundle("~/bundles/neon-main")
                      .Include("~/Assets/Neon/js/joinable.js")
                      .Include("~/Assets/Neon/js/resizeable.js")
                      .Include("~/Assets/Neon/js/neon-api.js")
                      .Include("~/Assets/Neon/js/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/neon-custom")
                      .Include("~/Assets/Neon/js/neon-custom.js")
                      .Include("~/Assets/Neon/js/neon-demo.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/login")
                      .Include("~/Scripts/Login/login.js"));

            bundles.Add(new StyleBundle("~/styles/neon")
                      .Include("~/Assets/Neon/css/neon-core.css")
                      .Include("~/Assets/Neon/css/neon-theme.css")
                      .Include("~/Assets/Neon/css/neon-forms.css")
                      .Include("~/assets/Neon/css/sweetalert.css"));

            bundles.Add(new StyleBundle("~/styles/css")
                .Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/styles/entypo").Include(
                      "~/Content/entypo.css"));

            bundles.Add(new StyleBundle("~/styles/fontawesome").Include(
                      "~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/tweenmax").Include(
                      "~/Scripts/TweenMax.js"));

            bundles.Add(new StyleBundle("~/styles/jqueryui").Include(
                      "~/Content/jquery-ui-1.10.3.custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.10.3.minimal.min.js"));

            bundles.Add(new StyleBundle("~/styles/toastr").Include(
                      "~/Content/toastr.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                      "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables")
                .Include("~/assets/dataTables/dataTables.min.js")
                .Include("~/scripts/dataTables/jquery.cmodev.datatables.js"));

            bundles.Add(new StyleBundle("~/styles/datatables").Include(
                     "~/assets/dataTables/datatables.min.css"));
                        
            bundles.Add(new StyleBundle("~/styles/select2").Include(
                     "~/assets/select2/select2.min.css"));

            bundles.Add(new StyleBundle("~/bundles/select2").Include(
                     "~/assets/select2/select2.full.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/site")
                .Include("~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/wizard")
                .Include("~/Scripts/jquery.bootstrap.wizard.min.js")
                .Include("~/Scripts/wizard/jquery.cmodev.wizard.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker")
                .Include("~/Scripts/bootstrap-datepicker.js")
                .Include("~/Scripts/bootstrap-datepicker.es.js"));

            bundles.Add(new StyleBundle("~/styles/toggle").Include(
                     "~/Content/bootstrap-toggle.min.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/toggle")
                .Include("~/Scripts/toggle/bootstrap-toggle.min.js")
                .Include("~/Scripts/toggle/jquery.cmodev.toggle.js"));

            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                     "~/Scripts/bootstrap-timepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/paginate").Include(
                "~/Scripts/jquery.paginate.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                "~/Scripts/fileinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/maskmoney")
                .Include("~/Scripts/jquery.maskMoney.js")
                .Include("~/Scripts/mask/jquery.cmodev.maskMoney.js"));

            bundles.Add(new ScriptBundle("~/bundles/notapedido").Include(
                      "~/Scripts/TendaGo.notapedido.mantenimiento.js"));

            bundles.Add(new StyleBundle("~/styles/notapedido").Include(
                      "~/Content/notapedido.css"));

            bundles.Add(new ScriptBundle("~/bundles/inventory/Transfer").Include(
                      "~/Scripts/TendaGo.inventory.transfer.js"));

            bundles.Add(new StyleBundle("~/styles/keyboard").Include(
                    "~/Assets/Neon/css/keyboard-previewkeyset.css",
                    "~/Assets/Neon/css/keyboard.css" ));

            bundles.Add(new StyleBundle("~/styles/pointofsale").Include(
                    "~/Assets/Neon/css/waves.min.css",
                    "~/Content/PointOfSale.css"));
             
            bundles.Add(new ScriptBundle("~/bundles/keyboard").Include(
                    "~/Assets/Neon/js/jquery.keyboard.js",
                    "~/Assets/Neon/js/jquery.keyboard.extension-all.js",
                    "~/Assets/Neon/js/jquery.keyboard.extension-extender.js",
                    "~/Assets/Neon/js/jquery.keyboard.extension-typing.js",
                    "~/Assets/Neon/js/jquery.mousewheel.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/creditcard").Include(
                    "~/Assets/Neon/js/credit-card-scanner.js",
                    "~/Assets/Neon/js/jquery.creditCardValidator.js"));

            bundles.Add(new ScriptBundle("~/bundles/pointofsale").Include(
                    "~/Assets/Neon/js/jquery.slimscroll.min.js",
                    "~/Assets/Neon/js/waves.min.js",
                    "~/Scripts/PointOfSale.js"));

            bundles.Add(new ScriptBundle("~/bundles/compras/importar").Include(
                    "~/Scripts/TendaGo.compra.importar.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/compras/index").Include(
                    "~/Scripts/TendaGo.compra.index.js"));

            bundles.Add(new ScriptBundle("~/bundles/productos/index").Include(
                    "~/Scripts/TendaGo.products.index.js"));
 
            bundles.Add(new ScriptBundle("~/bundles/cobros/index").Include(
                    "~/Scripts/TendaGo.cobros.index.js"));

            bundles.Add(new ScriptBundle("~/bundles/caja/cierre").Include(
                    "~/Scripts/TendaGo.caja.cierre.js"));

            bundles.Add(new ScriptBundle("~/bundles/cotizacion").Include(
                    "~/Assets/Neon/js/jquery.slimscroll.min.js",
                    "~/Assets/Neon/js/waves.min.js",
                    "~/Scripts/cotizacion.js"));

            bundles.Add(new ScriptBundle("~/bundles/inventario/importar").Include(
                    "~/Scripts/TendaGo.inventario.importar.js"));

        }
    }
}
