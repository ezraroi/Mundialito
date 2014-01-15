using System.Web;
using System.Web.Optimization;

namespace Mundialito
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //Angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                               "~/Scripts/angular.js",
                               "~/Scripts/angular-animate.js",
                               "~/Scripts/angular-sanitize.js",
                               "~/Scripts/angular-resource.js",
                               "~/Scripts/angular-route.js"
            ));

            //Angular Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/angularBootstrap").Include(
                               "~/Scripts/ui-bootstrap-tpls-{version}.js",
                               "~/Scripts/angular-spa-security.js",
                               "~/Scripts/autoFields-bootstrap.js"
            ));

            //App
            bundles.Add(new ScriptBundle("~/bundles/app")
                    .IncludeDirectory("~/App/", "*.js")
                    .IncludeDirectory("~/App/Controllers", "*.js")
                    .IncludeDirectory("~/App/Directives", "*.js")
                    .IncludeDirectory("~/App/Services", "*.js")
                    .IncludeDirectory("~/App/Filters", "*.js")
            );

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // External Libs
            bundles.Add(new ScriptBundle("~/bundles/external").Include(
                      "~/Scripts/promise-tracker.js",
                      "~/Scripts/angular-busy.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/flags.css",
                      "~/Content/angular-busy.min.css",
                      "~/Content/site.css"));
        }
    }
}
