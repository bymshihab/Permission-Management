namespace ADODemo.Model
{
    public class MaterialRequisitionModel
    {
        public int MRequisitionId { get; set; }   
        public string MRequisitionCode { get; set; }
        public string ProjectMasterCode { get;set; }
        public string ManualRequisitionNo { get; set;}
        public string RequisitionBy { get; set; }
        public string Date { get; set;}
        public string Status { get; set; }

    }
}
