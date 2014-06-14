using System;
using System.Web;
using System.Web.Optimization;

namespace Mundialito
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;
            //BundleTable.EnableOptimizations = false;

            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

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
                    .IncludeDirectory("~/App/GeneralBets", "*.js")
                    .IncludeDirectory("~/App/Stadiums", "*.js")
                    .IncludeDirectory("~/App/Teams", "*.js")
                    .IncludeDirectory("~/App/Users", "*.js")
            );

            // External Libs
            bundles.Add(new ScriptBundle("~/bundles/external").Include(
                      "~/App/Lib/select2.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                      "~/Scripts/autoFields-bootstrap.min.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/angular-spa-security.min.js",
                      "~/Scripts/promise-tracker.min.js",
                      "~/Scripts/datetimepicker.min.js",
                      "~/Scripts/angular-bootstrap-select.js",
                      "~/Scripts/facebookPluginDirectives.min.js",
                      "~/App/Lib/underscore.min.js",
                      "~/App/Lib/d3.min.js",
                      "~/App/Lib/line-chart.min.js",
                      "~/Scripts/ng-grid.min.js",
                      "~/App/Lib/ng-grid-flexible-height.js",
                      "~/App/Lib/angular-select2.js",
                      "~/App/Lib/ng-google-chart.js",
                      "~/App/Lib/angular-cache.min.js",
                      "~/Scripts/angular-busy.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/datetimepicker.css",
                      "~/Content/font-awesome.css",
                      "~/Content/flags.css",
                      "~/Content/angular-busy.min.css",
                      "~/Content/select2.min.css",
                      "~/Content/select2-bootstrap.min.css",
                      "~/Content/ng-grid.min.css",
                      "~/Content/site.css"));
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}
