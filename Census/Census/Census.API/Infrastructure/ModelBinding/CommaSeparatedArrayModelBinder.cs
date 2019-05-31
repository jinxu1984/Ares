using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Infrastructure.ModelBinding
{
    public class CommaSeparatedArrayModelBinder : IModelBinder
    {
        private static readonly Type[] supportedElementTypes =
            {
            typeof(int), typeof(long), typeof(short), typeof(byte),
            typeof(uint), typeof(ulong), typeof(ushort), typeof(Guid)
            };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!IsSupportedModelType(bindingContext.ModelType)) { return Task.CompletedTask; }

           var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue as string;
            if (string.IsNullOrEmpty(value)) { return Task.CompletedTask; }

            var stringArray = value.Split(',');
            var elementType = bindingContext.ModelType.GetElementType();
            var targetArray = CopyAndConvertArray(stringArray, elementType);

            bindingContext.Result = ModelBindingResult.Success(targetArray);

            return Task.CompletedTask;
        }

        private static Array CopyAndConvertArray(IReadOnlyList<string> sourceArray, Type elementType)
        {
            var targetArray = Array.CreateInstance(elementType, sourceArray.Count);

            if (sourceArray.Count > 0)
            {
                var converter = TypeDescriptor.GetConverter(elementType);
                for (var i = 0; i < sourceArray.Count; i++)
                { targetArray.SetValue(converter.ConvertFromString(sourceArray[i]), i); }
            }

            return targetArray;
        }

        internal static bool IsSupportedModelType(Type modelType)
        {
            return modelType.IsArray && modelType.GetArrayRank() == 1
                    && modelType.HasElementType
                    && supportedElementTypes.Contains(modelType.GetElementType());
        }
    }

    public class CustomBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }

            if (CommaSeparatedArrayModelBinder.IsSupportedModelType(context.Metadata.ModelType))
            {
                return new BinderTypeModelBinder(typeof(CommaSeparatedArrayModelBinder));
            }

            return null;
        }
    }
}
