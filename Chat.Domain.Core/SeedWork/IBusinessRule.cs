using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Core.SeedWork
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
        string ErrorCode { get; }
    }
}
