using System;
namespace Chat.Domain.Core.SeedWork
{
    public class BusinessRuleValidationException : Exception
    {
        public IBusinessRule BrokenRule { get; }

        public string Details { get; }
        public string ErroCode { get; }

        public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            this.Details = brokenRule.Message;
            this.ErroCode = brokenRule.ErrorCode;
        }
        public BusinessRuleValidationException(Enumeration enumeration) : base(enumeration.Desc)
        {

            this.Details = enumeration.Desc;
            this.ErroCode = enumeration.Name;
        }
        public override string ToString()
        {
            return $"{BrokenRule?.GetType().FullName}: {BrokenRule?.Message}";
        }
    }
}
