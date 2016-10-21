using System.Web;
using System.Web.Optimization;

namespace DailySports
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js", "~/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/js/custom.js", "~/js/ie10-viewport-bug-workaround.js", "~/js/ie-emulation-modes-warning", "~/js/owl.carousel.min.js", "~/js/isotope.pkgd.min.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css","~/css/bootstrap.min.css", "~/css/ie10-viewport-bug-workaround.css", "~/css/font-awesome.min.css",
                      "~/css/hover-min.css", "~/css/custom.css", "~/css/responsive.css", "~/css/owl.carousel.css", "~/css/owl.theme.css"

                      ));

        }
    }
}
