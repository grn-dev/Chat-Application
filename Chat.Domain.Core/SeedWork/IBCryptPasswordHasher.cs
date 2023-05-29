using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Core.SeedWork
{
    public interface IBCryptPasswordHasher
    {
        string HashPassword(string password);

        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
