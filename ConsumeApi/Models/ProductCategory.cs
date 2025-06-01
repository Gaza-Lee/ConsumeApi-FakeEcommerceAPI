using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeApi.Models
{
    public class ProductCategory
    {
        public string CategoryName { get; set; }
        public string Image { get; set; }

        public string DisplayCategoryName =>
            string.IsNullOrWhiteSpace(CategoryName)?CategoryName : char.ToUpper(CategoryName[0]) + CategoryName.Substring(1);
    }
}
