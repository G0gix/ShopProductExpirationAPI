using System;
using System.Collections.Generic;

#nullable disable

namespace ShopProductExpirationAPI
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public DateTime ProductManufacturingDate { get; set; }
        public DateTime? ProductPackagingDate { get; set; }
        public short? ShelfLife { get; set; }
        public string TimeUnits { get; set; }
        public DateTime? SellBy { get; set; }
        public short? ProductCount { get; set; }
        public string CountUnits { get; set; }
        public string ShopDepartment { get; set; }
        public string DepartmentHeadFio { get; set; }
        public short? RowNumber { get; set; }
        public short? ShelvingNumber { get; set; }
        public short? ShelfNumber { get; set; }
    }
}
