using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
