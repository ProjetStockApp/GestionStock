using System.Web;
using System.Web.Optimization;

namespace StockApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/jquery-3.3.1.js",
                     "~/Scripts/jquery-ui-1.12.1.js",
                     "~/Scripts/datepicker-fr.js",
                     "~/Scripts/datepicker.bootstrap.js",
                    "~/Scripts/DataTables/jquery.dataTables.js",
                  //"~/Scripts/DataTables/dataTables.bootstrap.js",
                //"~/Scripts/DataTables/dataTables.foundation.min.js",
                      
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //"~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/DataTables/css/jquery.dataTables.min.css",
                       "~/Content/themes/base/jquery-ui.css",
                     // "~/Content/DataTables/css/foundation.min.css",
                     // "~/Content/DataTables/css/dataTables.foundation.min.css",
                       "~/Content/MonStyle.css",
                      "~/Content/site.css"));
        }
    }
}
