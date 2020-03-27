using System;

namespace AngularJS.Models
{
    public partial class ProductCategoryViewModel : BaseEntity
    {
        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
