﻿namespace HikingPlanAndRescue.Web
{
    using System.Web.Optimization;

    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                        "~/Content/kendo/kendo.common.min.css",
                        "~/Content/kendo/kendo.common-bootstrap.min.css",
                        "~/Content/kendo/kendo.default.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/kendo.all.min.js",
                        "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                        "~/Scripts/gridmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/gridmvc").Include(
                        "~/Content/Gridmvc.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/kendo/jquery.min.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/site.css"));

            
        }
    }
}
