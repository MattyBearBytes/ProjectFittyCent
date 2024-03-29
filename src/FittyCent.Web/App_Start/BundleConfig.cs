﻿using System.Web.Optimization;

namespace FittyCent.Web 
{
    public class BundleConfig 
    {
        public static void RegisterBundles(BundleCollection bundles) 
        {
            BundleTable.EnableOptimizations = false;
                RegisterStyleBundles(bundles);
                RegisterScriptBundles(bundles);
        }

        public static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap/flatly-css/bootstrap.css")
                .Include("~/Content/app/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/login")
                .Include("~/Content/app/css/login.css"));

            bundles.Add(new StyleBundle("~/Content/css/about")
                .Include("~/Content/app/css/about.css"));

            bundles.Add(new StyleBundle("~/Content/css/onepage")
                .Include("~/Content/onepage/onepage-scroll.css"));
        }

        public static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/onepage")
                .Include("~/Content/onepage/jquery.onepage-scroll.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Content/bootstrap/js/bootstrap.js")
                .Include("~/Scripts/respond.js"));

        }
    }
}
