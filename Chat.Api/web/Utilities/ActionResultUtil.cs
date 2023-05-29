using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Web.Utilities
{
    public static class ActionResultUtil
    {
        public static ActionResult WrapOrNotFound(object value)
        {
            return value != null ? (ActionResult)new OkObjectResult(value) : new NotFoundResult();
        }
    }
}
