using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace RestSampleNew.Helpers
{
    public class PizzaNameConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (values.TryGetValue(parameterName, out var value) && value != null)
            {
                var str = value.ToString();
                return str.Length < 10;
            }

            return false;
        }
    }
}
