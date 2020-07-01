using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.ViewModels
{
    public class AuthUserOutputModel
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string Token { get; set; }
    }
}
