using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chat.Domain.Attributes
{

    public class MyStringLengthAttribute : StringLengthAttribute
    {
        public MyStringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return base.IsValid(value);
            string val = Convert.ToString(value);
            if (val.Length < base.MinimumLength)
                base.ErrorMessage = "Minimum length should be " + base.MinimumLength;
            if (val.Length > base.MaximumLength)
                base.ErrorMessage = "Maximum length should be " + base.MaximumLength;
            return base.IsValid(value);
        }
    }
}
