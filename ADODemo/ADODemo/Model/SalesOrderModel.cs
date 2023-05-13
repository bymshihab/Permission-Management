namespace ADODemo.Model
{
    public class SalesOrderModel
    {

        public int SalesOrderQty { get; set; }
        public decimal SalesOrderRate { get; set; }
        public int UnitSetId { get; set; }
        public int UnitId { get; set; }

        public string Remarks { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
   
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string GoodsName { get; set; }

    }
}
