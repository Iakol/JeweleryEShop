using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jewelery.Infrastructure.Binder.ProductBinder
{

    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Parameter, AllowMultiple = false, Inherited =false)]
    public class ProductCMSDTOBinderAttribute: Attribute
    {
        public BindingSource BindingSource => BindingSource.Custom;

    }
}
