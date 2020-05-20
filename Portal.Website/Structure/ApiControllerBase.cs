using Portal.Structure.Requests;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Portal.Website.Structure {

    public abstract class ApiControllerBase : ApiController, IRequestProcessor {

        protected TService Get<TService>() {
            return WebApiApplication.Services.Get<TService>();
        }

        public T Process<T>(Func<T> requestCall) {
            return WebApiApplication.RequestProcessor.Process(requestCall);
        }

        public void Process(Action requestCall) {
            WebApiApplication.RequestProcessor.Process(requestCall);
        }

        protected T ParseFormData<T>() {
            NameValueCollection form = HttpContext.Current.Request.Params;
            string[] keys = form.AllKeys;
            PropertyInfo[] properties = typeof(T).GetProperties();
            T model = PortalUtility.ConstructEmpty<T>();
            foreach (string key in keys) {
                PropertyInfo property = properties.Where(p => p.Name == key).SingleOrDefault();
                if (property != null) {
                    if (property.PropertyType.Equals(typeof(int)))
                        property.SetValue(model, int.Parse(form[key]));
                    if (property.PropertyType.Equals(typeof(string)))
                        property.SetValue(model, form[key]);
                    if (property.PropertyType.Equals(typeof(bool)))
                        property.SetValue(model, bool.Parse(form[key]));
                    if (property.PropertyType.Equals(typeof(DateTime)))
                        property.SetValue(model, DateTime.Parse(form[key]));
                }
            }
            return model;
        }

    }

}
