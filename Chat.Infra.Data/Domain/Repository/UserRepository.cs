using Chat.Domain.Attributes;
using Chat.Domain.Models;
using Chat.Domain.Repositorys;
using Chat.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Chat.Domain.Models.User;

namespace Chat.Infra.Data.Domain.Repository
{
    [Bean]
    public class UserRepository : Repository<User, ChatContext>, IUserRepository

    {
        public UserRepository(ChatContext db) : base(db)
        {

        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await Db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}

