using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Jewelery.Infrastructure.Binder.ProductBinder
{
    public class ProductCSMDTOBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) 
            {
                throw new ArgumentNullException("context");
            }

            if(context.Metadata.BindingSource !=null && context.Metadata.BindingSource.CanAcceptDataFrom(BindingSource.Custom)) 
            {
                return new BinderTypeModelBinder(typeof(ProductCSMDTOBinder));
            }

            return null;

        }
    }


}
