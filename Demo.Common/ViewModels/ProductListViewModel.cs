using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.ViewModels
{
    public class ProductListViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
