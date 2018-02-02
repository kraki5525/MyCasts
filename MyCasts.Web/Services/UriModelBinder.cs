using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyCasts.Web.Services
{
    public class UriModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Uri url = null;
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (result == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            if (result != null && string.IsNullOrEmpty(result.FirstValue))
            {
                if (Uri.TryCreate(result.FirstValue, UriKind.RelativeOrAbsolute, out url))
                {
                    bindingContext.ModelState.SetModelValue(bindingContext.ModelName, url, result.FirstValue);
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
            }
            return Task.CompletedTask;
        }
    }
}