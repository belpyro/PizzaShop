using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace RestSampleNew.Controllers
{
    public class FilterBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if(value == null)
            {
                actionContext.ModelState.AddModelError(bindingContext.ModelName, "Request doesn't contain valid model data");
                return false;
            }

            if (value.RawValue is string)
            {
                var data = value.RawValue.ToString();
                var items = data.Split(',');
                if (items.Length != 2)
                {
                    actionContext.ModelState.AddModelError(bindingContext.ModelName, "Model data is empty");
                    return false;
                }

                bindingContext.Model = new Filter
                {
                    GroupBy = items[0],
                    SortedByAsc = bool.Parse(items[1])
                };

                return true;
            }

            actionContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid model schema");
            return false;
        }
    }
}