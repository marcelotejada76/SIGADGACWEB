using System.Web;
using System.Web.Optimization;

namespace DGACWWEB
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(

                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/plugins/daterangepicker/daterangepicker.css",
                      "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                      "~/Content/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.min.css",
                      "~/Content/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                      "~/Content/plugins/select2/css/select2.min.css",
                      "~/Content/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css",
                      "~/Content/plugins/bootstrap4-duallistbox/bootstrap-duallistbox.min.css",
                      "~/Content/plugins/bs-stepper/css/bs-stepper.min.css",
                      "~/Content/plugins/sweetalert2/sweetalert2.min.css",
                      "~/Content/plugins/dropzone/min/dropzone.min.css",
                       "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                      "~/Content/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                      "~/Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                       "~/Content/plugins/ddatatables-fixedheader/css/fixedHeader.bootstrap4.css",
                      "~/Content/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css",
                      "~/Content/dist/css/adminlte.min.css"));

            bundles.Add(new StyleBundle("~/Content/PluginsCSS").Include(

                     ));

            bundles.Add(new StyleBundle("~/Content/PluginsJS").Include(
                     "~/Content/plugins/jquery/jquery.min.js",
                      "~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Content/plugins/select2/js/select2.full.min.js",
                      "~/Content/plugins/bootstrap4-duallistbox/jquery.bootstrap-duallistbox.min.js",
                      "~/Content/plugins/moment/moment.min.js",
                      "~/Content/plugins/inputmask/jquery.inputmask.min.js",
                      "~/Content/plugins/daterangepicker/daterangepicker.js",
                      "~/Content/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.min.js",
                      "~/Content/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                      "~/Content/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/Content/plugins/sweetalert2/sweetalert2.min.js",
                      "~/Content/plugins/datatables/jquery.dataTables.min.js",
                      "~/Content/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                      "~/Content/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                      "~/Content/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                      "~/Content/plugins/datatables-buttons/js/dataTables.buttons.min.js",
                      "~/Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
                      "~/Content/plugins/datatables-fixedheader/css/fixedHeader.bootstrap4.min.js",
                      "~/Content/plugins/bs-stepper/js/bs-stepper.min.js",
                      "~/Content/plugins/dropzone/min/dropzone.min.js",
                      "~/Content/dist/js/adminlte.min.js",
                      "~/Content/dist/js/print.min.js",
                      "~/Content/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js",
                      "~/Content/dist/js/dgac.js"
                     ));
        }
    }
}
