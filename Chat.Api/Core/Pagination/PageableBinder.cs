﻿using System;
using System.Threading.Tasks;
using Chat.Domain.Core.SeedWork.Pagination;
using Chat.Infra.Data.Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Chat.Api.Core.Pagination
{
    //public class PageableBinder : IModelBinder
    //{
    //    private readonly PageableBinderConfig _binderConfig = new PageableBinderConfig();

    //    public Task BindModelAsync(ModelBindingContext bindingContext)
    //    {
    //        //TODO Assert target method (only one pageable)
    //        //TODO defensive programing against PageableBinderConfig values

    //        if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

    //        var queryString = bindingContext.HttpContext.Request.QueryString;
    //        var pageable = ResolvePageableArgumentFromQueryString(queryString);
    //        bindingContext.Result = ModelBindingResult.Success(pageable);
    //        return Task.CompletedTask;
    //    }

    //    private IPageable ResolvePageableArgumentFromQueryString(QueryString queryString)
    //    {
    //        var pageNumberString = queryString.GetParameter(_binderConfig.PageParameterName);
    //        var pageSizeString = queryString.GetParameter(_binderConfig.SizeParameterName);
    //        var Filter = queryString.GetParameter(_binderConfig.FilterParameterName);
    //        var Order = queryString.GetParameter(_binderConfig.OrderParameterName);
    //        var pageNumber = ParseIntOrDefault(pageNumberString, _binderConfig.FallbackPageable.PageNumber);
    //        var pageSize = ParseIntOrDefault(pageSizeString, _binderConfig.FallbackPageable.PageSize,
    //            _binderConfig.MaxPageSize);

    //        return Pageable.Of(pageNumber, pageSize, Filter, Order);
    //    }

    //    private static int ParseIntOrDefault(string parameter, int defaultValue, int upper = int.MaxValue)
    //    {
    //        if (!int.TryParse(parameter, out var value)) value = defaultValue;

    //        value = value < 0 ? 0 : value;
    //        value = value > upper ? upper : value;

    //        return value;
    //    }
    //}
}
