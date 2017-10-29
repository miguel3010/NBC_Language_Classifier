using System.Web;
using System.Web.Optimization;

namespace NBC_Idiomas
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/script").Include(
                        "~/Content/*.js")); 
            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/*.css"));
        }
    }
}
