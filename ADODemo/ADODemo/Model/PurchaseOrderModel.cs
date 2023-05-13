namespace ADODemo.Model
{
    public class PurchaseOrderModel
    {
        public int PurchaseID { get; set; }
        public string PurchaseOrderCode { get; set; }
        public string VendorName { get;set; }
        public string PurchaseDate { get; set; }
      
        public string PurchaseTypeName { get; set; }
        public string VendorSelectionNo { get; set; }
        public string OtherCostAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get;set; }
        public string ApproveStatus { get; set; }


    }
}
