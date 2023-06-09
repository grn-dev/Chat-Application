using System;
using Chat.Domain.Core.SeedWork.Pagination;
using Microsoft.AspNetCore.Http;

namespace Chat.Api.Web.Utilities
{
    //public static class PaginationUtil
    //{
    //    private const string _LinkHeaderName = "Link";
    //    private const string _XTotalCountHeaderName = "X-Total-Count";

    //    public static IHeaderDictionary GeneratePaginationHttpHeaders<T>(IPage<T> page, HttpRequest request)
    //        where T : class
    //    {
    //        var scheme = request.Scheme;
    //        var host = request.Host.Value;
    //        var path = request.Path.Value;
    //        if (string.IsNullOrEmpty(path)) path = "/";
    //        var baseUrl = $"{scheme}://{host}{path}";
    //        return GeneratePaginationHttpHeaders(page, baseUrl);
    //    }

    //    public static IHeaderDictionary GeneratePaginationHttpHeaders<T>(IPage<T> page, string baseUrl) where T : class
    //    {
    //        IHeaderDictionary headers = new HeaderDictionary();
    //        headers.Add(_XTotalCountHeaderName, page.TotalElements.ToString());
    //        var link = "";
    //        if (page.Number + 1 < page.TotalPages)
    //            link += $"<{GenerateUri(baseUrl, page.Number + 1, page.Size)}>; rel=\"next\",";

    //        if (page.Number > 0) link += $"<{GenerateUri(baseUrl, page.Number - 1, page.Size)}>; rel=\"prev\",";

    //        var lastPage = 0;
    //        if (page.TotalPages > 0) lastPage = page.TotalPages - 1;

    //        link += $"<{GenerateUri(baseUrl, lastPage, page.Size)}>; rel=\"last\",";
    //        link += $"<{GenerateUri(baseUrl, 0, page.Size)}>; rel=\"first\"";
    //        headers.Add(_LinkHeaderName, link);

    //        return headers;
    //    }

    //    private static string GenerateUri(string baseUrl, int page, int size)
    //    {
    //        return new UriBuilder(baseUrl)
    //        {
    //            Query = $"page={page}&size={size}"
    //        }.Uri.ToString();
    //    }
    //}
}
