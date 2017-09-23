using System.Web;
using System.Web.Optimization;

namespace PhotoGallerySite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
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
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/addalbum").Include(
                      "~/Scripts/dynamicimagelist.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/myalbums").Include(
                      "~/Scripts/deletealert.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/edit").Include(
                    "~/Scripts/deleteimage.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/show").Include(
                    "~/Scripts/ajax-showalbum.js"
                    ));
        }
    }
}
