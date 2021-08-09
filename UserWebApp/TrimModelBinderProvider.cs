using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

public class NoTrimmingAttribute : Attribute
{ }

public class TrimmingModelBinderProvider : IModelBinderProvider
{
    public class TrimmingModelBinder : ComplexTypeModelBinder
    {
        public TrimmingModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders, ILoggerFactory loggerFactory) : base(propertyBinders, loggerFactory) {
        }

        protected override void SetProperty(ModelBindingContext bindingContext, string modelName, ModelMetadata propertyMetadata, ModelBindingResult result)
        {
            var value = result.Model as string;
            if (value != null)
                result = ModelBindingResult.Success(value.Trim());
            base.SetProperty(bindingContext, modelName, propertyMetadata, result);
        }
    }

    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));
        if (!context.Metadata.IsComplexType || context.Metadata.IsCollectionType)
            return null;

        var propertyBinders = new Dictionary<ModelMetadata, IModelBinder>();
        for (var i = 0; i < context.Metadata.Properties.Count; i++)
        {
            var property = context.Metadata.Properties[i];
            propertyBinders.Add(property, context.CreateBinder(property));
        }
        return new TrimmingModelBinder(propertyBinders, context.Services.GetRequiredService<ILoggerFactory>());
    }
}