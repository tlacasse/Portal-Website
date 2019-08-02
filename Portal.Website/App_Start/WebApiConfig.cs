using System.Net.Http.Headers;
using System.Web.Http;

namespace Portal.Website {

    public static class WebApiConfig {

        public static void Register(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("multipart/form-data"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("multipart/form-data"));
        }

    }

}
