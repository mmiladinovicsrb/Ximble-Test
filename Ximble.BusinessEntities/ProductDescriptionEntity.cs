using System;

namespace Ximble.BusinessEntities
{
    public class ProductDescriptionEntity
    {
        public int ProductDescriptionID { get; set; }
        public string Description { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
