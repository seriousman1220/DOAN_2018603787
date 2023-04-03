using System.Web;
using System.Web.Optimization;

namespace MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/angular.min.js",
                      "~/Scripts/Client/MainPage.js",
                      "~/Scripts/Admin/js/login.js",
                      "~/Scripts/script.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/elegant-icons.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/nice-select.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/Site.css",
                      "~/Content/slicknav.min.css",
                      "~/Content/style.css"));
        }
    }
}
