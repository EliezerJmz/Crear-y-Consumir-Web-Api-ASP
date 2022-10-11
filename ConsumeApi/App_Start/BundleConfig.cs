using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace ConsumeApi.App_Start
{
    public class BundleConfig
    {

        //Metodo para registrar los Bundles
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Nombre del Bundle: "~/bundles/jquery"
            //Ubicacion del archivo:  "~/Scripts/jquery-{version}.js"
            //{version}: version tomara la version del archivo en este caso jquery-3.0.0.js
            //Nota: los Bundles utilizan los archivos .min cuando estan en produccion
            //Podemos agregar cunatas ubicacines necesitemos y utilizar solo un nombre para el Bundle

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //StyleBundle
            //ubicacion: carpeta Content 
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }

    }
}