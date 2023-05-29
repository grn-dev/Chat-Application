using Chat.Application.Interfaces;
using Chat.Domain.Attributes;
using Chat.Infra.Security.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace Chat.Infra.Security
{
    [Bean]
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public Guid? UserId => _httpContextAccessor.HttpContext?.User.GetUserId();

        public string UserName => _httpContextAccessor.HttpContext?.User.Identity.Name;
    }
}
