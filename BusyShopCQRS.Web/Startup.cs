using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;

namespace BusyShopCQRS.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration();

            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var appXmlType = configuration.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            configuration.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
            //{
            //    //ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            //    //ContractResolver = new DefaultContractResolver()
            //};

            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.UseDataContractJsonSerializer = true;

            //LinkyConfiguration.Configure(configuration);

            appBuilder.UseWebApi(configuration);

            appBuilder.UseDefaultFiles(new DefaultFilesOptions()
            {
                DefaultFileNames = new[] { "index.html" }
            });
            appBuilder.UseFileServer(new FileServerOptions()
            {
                //FileSystem = new PhysicalFileSystem(@".\content"),
                FileSystem = new PhysicalFileSystem("../../content"),
                RequestPath = PathString.Empty,
                EnableDefaultFiles = true
            });
        }
    }
}