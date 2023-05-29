using Chat.Domain.Attributes;
using Chat.Domain.Core.SeedWork;

namespace Chat.Application.DomainService
{
    [Bean]
    [Scope(Chat.Domain.Enums.ServiceLifetime.Singleton)]
    public class BCryptPasswordHasher : IBCryptPasswordHasher
    {

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyHashedPassword(string hashedPassword,
            string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}
