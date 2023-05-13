namespace ADODemo.Model
{
    public class HrmLeaveModel
    {
        public int ApplicationId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveTypeCode { get; set; }
        public string LeaveApplyDate { get; set; }
        public string LeaveFromDate { get; set; }
        public string LeaveToDate { get; set; }
        public Double LeaveDaysNo { get; set; }
        public string LeaveProcessStatus { get; set; }


    }
}
 