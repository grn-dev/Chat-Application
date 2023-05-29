using Chat.Domain.Models;
using System;
using System.Threading.Tasks;
using Chat.Domain.Models.User;

namespace Chat.Domain.Repositorys
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserById(Guid userId);
    }
}

