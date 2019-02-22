using System.Web;
using System.Web.Optimization;

namespace Amazonweb.web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Template 1 Start
            bundles.Add(new ScriptBundle("~/Template1/jquery").Include(
                  "~/TemplateThemes/Template1/vendor/jquery/jquery.min.js",
  "~/TemplateThemes/Template1/vendor/bootstrap/js/bootstrap.bundle.min.js"
                     ));

            bundles.Add(new ScriptBundle("~/Template1/css").Include(
               "~/TemplateThemes/Template1/vendor/bootstrap/css/bootstrap.min.css",
               "~/TemplateThemes/Template1/vendor/css/shop-homepage.css"

                   ));
            #endregion 

            #region  Template 2 Start
            bundles.Add(new ScriptBundle("~/Template2/jquery").Include(
                  "~/TemplateThemes/Template2/vendor/jquery/jquery.min.js",
  "~/TemplateThemes/Template2/vendor/bootstrap/js/bootstrap.bundle.min.js"

                     ));
            #endregion
            // <!--Bootstrap core JavaScript -->
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/lib/jquery/jquery.js",
                      "~/lib/popper.js/popper.js",
                      "~/lib/bootstrap/bootstrap.js",
                      "~/lib/perfect-scrollbar/js/perfect-scrollbar.jquery.js",
                      "~/lib/jquery-toggles/toggles.min.js",
                      "~/lib/datatables/jquery.dataTables.js",
                     "~/lib/datatables-responsive/dataTables.responsive.js",
                      "~/lib/select2/js/select2.min.js",
                      //"~/lib/d3/d3.js",
                      //"~/lib/rickshaw/rickshaw.min.js",
                      //"~/lib/gmaps/gmaps.js",
                      //"~/lib/Flot/jquery.flot.js",
                      //"~/lib/Flot/jquery.flot.pie.js",
                      //"~/lib/Flot/jquery.flot.resize.js",
                      //"~/lib/flot-spline/jquery.flot.spline.js",
                      "~/js/amanda.js"
                      //"~/js/ResizeSensor.js",
                      /*"~/js/dashboard.js"*/));



            bundles.Add(new StyleBundle("~/sitecontent/css").Include(
                "~/lib/font-awesome/css/font-awesome.css",
                "~/lib/Ionicons/css/ionicons.css",
               "~/lib/perfect-scrollbar/css/perfect-scrollbar.css",
              "~/lib/datatables/jquery.dataTables.css",
               "~/lib/jquery-toggles/toggles-full.css",
                "~/lib/rickshaw/rickshaw.min.css",
                 "~/lib/select2/css/select2.min.css",
               "~/css/amanda.css"));

            bundles.Add(new ScriptBundle("~/sitebundles/jquery").Include(
"~/lib/jquery/jquery.js",
"~/lib/popper.js/popper.js",
"~/lib/bootstrap/bootstrap.js",
"~/lib/perfect-scrollbar/js/perfect-scrollbar.jquery.js",
"~/lib/jquery-toggles/toggles.min.js",
"~/lib/d3/d3.js",
"~/lib/rickshaw/rickshaw.min.js",
//"http://maps.google.com/maps/api/js?key=AIzaSyAEt_DBLTknLexNbTVwbXyq2HSf2UbRBU8",
"~/lib/gmaps/gmaps.js",
"~/lib/Flot/jquery.flot.js",
"~/lib/Flot/jquery.flot.pie.js",
"~/lib/Flot/jquery.flot.resize.js",
"~/lib/flot-spline/jquery.flot.spline.js",
"~/js/amanda.js",
"~/js/ResizeSensor.js",
"~/js/dashboard.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
"~/lib/font-awesome/css/font-awesome.css",
"~/lib/Ionicons/css/ionicons.css",
"~/lib/perfect-scrollbar/css/perfect-scrollbar.css",
"~/css/amanda.css"
  ));



        }
    }
}
