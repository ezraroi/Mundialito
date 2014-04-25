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

            //App
            bundles.Add(new ScriptBundle("~/bundles/app")
                    .IncludeDirectory("~/App/", "*.js")
                    .IncludeDirectory("~/App/Accounts", "*.js")
                    .IncludeDirectory("~/App/Bets", "*.js")
                    .IncludeDirectory("~/App/Dashboard", "*.js")
                    .IncludeDirectory("~/App/Games", "*.js")
                    .IncludeDirectory("~/App/General", "*.js")
                    .IncludeDirectory("~/App/Stadiums", "*.js")
                    .IncludeDirectory("~/App/Teams", "*.js")
            );

            // External Libs
            bundles.Add(new ScriptBundle("~/bundles/external").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/ui-bootstrap-tpls-{version}.js",
                      "~/Scripts/autoFields-bootstrap.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/angular-spa-security.js",
                      "~/Scripts/promise-tracker.js",
                      "~/Scripts/datetimepicker.js",
                      "~/Scripts/angular-bootstrap-select.js",
                      "~/Scripts/facebookPluginDirectives.js",
                      "~/App/Lib/underscore.min.js",
                      "~/App/Lib/d3.min.js",
                      "~/App/Lib/line-chart.min.js",
                      "~/Scripts/angular-busy.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/datetimepicker.css",
                      "~/Content/font-awesome.css",
                      "~/Content/flags.css",
                      "~/Content/angular-busy.min.css",
                      "~/Content/bootstrap-select.min.css",
                      "~/Content/site.css"));
        }
    }
}
