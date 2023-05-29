using System;

namespace Chat.Application.Configuration.Exceptions
{
    public class MyApplicationException : Exception
    {


        public string Details { get; }
        public string ErroCode { get; }

        public string Name { get; set; }

        public MyApplicationException(ApplicationErrorCode applicationErrorCode) : base(applicationErrorCode.Desc)
        {

            this.Details = applicationErrorCode.Desc;
            this.ErroCode = "01-" + applicationErrorCode.Id;
            Name = applicationErrorCode.Name;
        }


    }
}
