using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }      
        public string Password { get; set; }       
        public string FirstName { get; set; }        
        public string LastName { get; set; }       
        public string Occupation { get; set; }

    }
}
