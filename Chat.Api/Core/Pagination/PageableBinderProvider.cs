﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace Chat.Api.Core.Pagination
{
    //public class PageableBinderProvider : IModelBinderProvider
    //{
    //    public IModelBinder GetBinder(ModelBinderProviderContext context)
    //    {
    //        if (context == null) throw new ArgumentNullException(nameof(context));

    //        if (context.Metadata.ModelType == typeof(IPageable))
    //            return new BinderTypeModelBinder(typeof(PageableBinder));

    //        return null;
    //    }
    //}
}
