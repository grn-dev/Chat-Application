using System;
using System.Collections.Generic;

namespace Chat.Application.ViewModels
{
    public partial class DirectViewModel
    {
        public class RegisterDirectOutput
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class RegisterDirect
        {
            public Guid UserId { get; set; }
        }

        public class MyDirect
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public virtual List<DirectUserExpose> DirectUsers { get; set; }
        }

        public class DirectUserExpose
        {
            public bool Show { get; set; }
            public UserViewModel.UserBaseExpose User { get; set; }
        }

        public class DirectExpose
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public virtual List<DirectUserExpose> DirectUsers { get; set; }
        }
    }
}