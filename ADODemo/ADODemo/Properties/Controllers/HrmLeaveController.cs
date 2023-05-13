using ADODemo.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Runtime.Intrinsics.X86;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ADODemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrmLeaveController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HrmLeaveController(IConfiguration config)
        {
            _configuration = config;
        }
        [HttpGet]
        [Route("GetAllApplication")]
        public List<HrmLeaveModel> GetAllHrmApplicant()
        {
            List<HrmLeaveModel> Lst = new List<HrmLeaveModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("SELECT TOP 100 \r\n  " +
                "  HRM_EmployeeLeaveApplication.AppliedLeaveMstId, \r\n " +
                "   HRM_EmployeeMaster.EmployeeId, \r\n    CONCAT(HRM_EmployeeMaster.EmployeeFirstName, ' '," +
                " HRM_EmployeeMaster.EmployeeMiddleName, ' ', HRM_EmployeeMaster.EmployeeLastName) AS EmployeeName,\r\n  " +
                "  HRM_EmployeeLeaveApplication.LeaveTypeCode, \r\n    CONVERT(date, HRM_EmployeeLeaveApplication.LeaveApplyDate)" +
                " AS LeaveApplyDate, \r\n    HRM_EmployeeLeaveApplication.LeaveFromDate, \r\n    HRM_EmployeeLeaveApplication.LeaveToDate, \r\n    HRM_EmployeeLeaveApplication.LeaveDaysNo, \r\n    HRM_EmployeeLeaveApplication.LeaveProcessStatus \r\nFROM HRM_EmployeeLeaveApplication \r\nINNER JOIN HRM_EmployeeMaster ON HRM_EmployeeMaster.EmployeeId = HRM_EmployeeLeaveApplication.EmployeeId;\r\n", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HrmLeaveModel modelObj = new HrmLeaveModel();
                modelObj.ApplicationId = int.Parse(dt.Rows[i]["AppliedLeaveMstId"].ToString());
                modelObj.EmployeeId = int.Parse(dt.Rows[i]["EmployeeId"].ToString());
                modelObj.EmployeeName = dt.Rows[i]["EmployeeName"].ToString();
                modelObj.LeaveTypeCode = dt.Rows[i]["LeaveTypeCode"].ToString();
                modelObj.LeaveApplyDate = dt.Rows[i]["LeaveApplyDate"].ToString().Split(' ')[0];
                modelObj.LeaveFromDate = dt.Rows[i]["LeaveFromDate"].ToString().Split(' ')[0];
                modelObj.LeaveToDate = dt.Rows[i]["LeaveToDate"].ToString().Split(' ')[0];
                modelObj.LeaveDaysNo = double.Parse(dt.Rows[i]["LeaveDaysNo"].ToString());
                modelObj.LeaveProcessStatus = dt.Rows[i]["LeaveProcessStatus"].ToString();
                //EmployeeId,LeaveTypeCode,LeaveApplyDate,LeaveFromDate,LeaveToDate,LeaveDaysNo,LeaveProcessStatus
                Lst.Add(modelObj);
            }

            return Lst;

        }


        [HttpGet]
        [Route("GetAllPending")]
        public List<HrmLeaveModel> GetAllPending()
        {
            List<HrmLeaveModel> Lst = new List<HrmLeaveModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("SELECT ELA.AppliedLeaveMstId, EMA.EmployeeId, CONCAT(EMA.EmployeeFirstName, ' '," +
                " EMA.EmployeeMiddleName, ' ', EMA.EmployeeLastName) AS EmployeeName, ELA.LeaveTypeCode," +
                " CONVERT(date, ELA.LeaveApplyDate) AS LeaveApplyDate, ELA.LeaveFromDate, ELA.LeaveToDate, ELA.LeaveDaysNo," +
                " ELA.LeaveProcessStatus FROM (SELECT TOP 100 * FROM HRM_EmployeeLeaveApplication) AS ELA" +
                " INNER JOIN HRM_EmployeeMaster AS EMA ON EMA.EmployeeId = ELA.EmployeeId WHERE ELA.LeaveProcessStatus = 'Pending'" +
                " ORDER BY LeaveApplyDate DESC;", con);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HrmLeaveModel modelObj = new HrmLeaveModel();
                modelObj.ApplicationId = int.Parse(dt.Rows[i]["AppliedLeaveMstId"].ToString());
                modelObj.EmployeeId = int.Parse(dt.Rows[i]["EmployeeId"].ToString());
                modelObj.EmployeeName = dt.Rows[i]["EmployeeName"].ToString();
                modelObj.LeaveTypeCode = dt.Rows[i]["LeaveTypeCode"].ToString();
                modelObj.LeaveApplyDate = dt.Rows[i]["LeaveApplyDate"].ToString().Split(' ')[0];
                modelObj.LeaveFromDate = dt.Rows[i]["LeaveFromDate"].ToString().Split(' ')[0];
                modelObj.LeaveToDate = dt.Rows[i]["LeaveToDate"].ToString().Split(' ')[0];
                modelObj.LeaveDaysNo = double.Parse(dt.Rows[i]["LeaveDaysNo"].ToString());
                modelObj.LeaveProcessStatus = dt.Rows[i]["LeaveProcessStatus"].ToString();
                //EmployeeId,LeaveTypeCode,LeaveApplyDate,LeaveFromDate,LeaveToDate,LeaveDaysNo,LeaveProcessStatus
                Lst.Add(modelObj);
            }

            return Lst;

        }


        [HttpGet]
        [Route("GetAllApproved")]
        public List<HrmLeaveModel> GetAllApproved()
        {
            List<HrmLeaveModel> Lst = new List<HrmLeaveModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("SELECT ELA.AppliedLeaveMstId, EMA.EmployeeId, CONCAT(EMA.EmployeeFirstName, ' '," +
                 " EMA.EmployeeMiddleName, ' ', EMA.EmployeeLastName) AS EmployeeName, ELA.LeaveTypeCode," +
                 " CONVERT(date, ELA.LeaveApplyDate) AS LeaveApplyDate, ELA.LeaveFromDate, ELA.LeaveToDate, ELA.LeaveDaysNo," +
                 " ELA.LeaveProcessStatus FROM (SELECT TOP 100 * FROM HRM_EmployeeLeaveApplication) AS ELA" +
                 " INNER JOIN HRM_EmployeeMaster AS EMA ON EMA.EmployeeId = ELA.EmployeeId WHERE ELA.LeaveProcessStatus = 'Approved'" +
                 " ORDER BY LeaveApplyDate DESC;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HrmLeaveModel modelObj = new HrmLeaveModel();
                modelObj.ApplicationId = int.Parse(dt.Rows[i]["AppliedLeaveMstId"].ToString());
                modelObj.EmployeeId = int.Parse(dt.Rows[i]["EmployeeId"].ToString());
                modelObj.EmployeeName = dt.Rows[i]["EmployeeName"].ToString();
                modelObj.LeaveTypeCode = dt.Rows[i]["LeaveTypeCode"].ToString();
                modelObj.LeaveApplyDate = dt.Rows[i]["LeaveApplyDate"].ToString().Split(' ')[0];
                modelObj.LeaveFromDate = dt.Rows[i]["LeaveFromDate"].ToString().Split(' ')[0];
                modelObj.LeaveToDate = dt.Rows[i]["LeaveToDate"].ToString().Split(' ')[0];
                modelObj.LeaveDaysNo = double.Parse(dt.Rows[i]["LeaveDaysNo"].ToString());
                modelObj.LeaveProcessStatus = dt.Rows[i]["LeaveProcessStatus"].ToString();
                //EmployeeId,LeaveTypeCode,LeaveApplyDate,LeaveFromDate,LeaveToDate,LeaveDaysNo,LeaveProcessStatus
                Lst.Add(modelObj);
            }

            return Lst;

        }

        [HttpGet]
        [Route("GetAllDisApproved")]
        public List<HrmLeaveModel> GetAllDisApproved()
        {
            List<HrmLeaveModel> Lst = new List<HrmLeaveModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("SELECT ELA.AppliedLeaveMstId, EMA.EmployeeId, CONCAT(EMA.EmployeeFirstName, ' '," +
                " EMA.EmployeeMiddleName, ' ', EMA.EmployeeLastName) AS EmployeeName, ELA.LeaveTypeCode," +
                " CONVERT(date, ELA.LeaveApplyDate) AS LeaveApplyDate, ELA.LeaveFromDate, ELA.LeaveToDate, ELA.LeaveDaysNo," +
                " ELA.LeaveProcessStatus FROM (SELECT TOP 100 * FROM HRM_EmployeeLeaveApplication) AS ELA" +
                " INNER JOIN HRM_EmployeeMaster AS EMA ON EMA.EmployeeId = ELA.EmployeeId WHERE ELA.LeaveProcessStatus = 'DisApproved'" +
                " ORDER BY LeaveApplyDate DESC;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HrmLeaveModel modelObj = new HrmLeaveModel();
                modelObj.ApplicationId = int.Parse(dt.Rows[i]["AppliedLeaveMstId"].ToString());
                modelObj.EmployeeId = int.Parse(dt.Rows[i]["EmployeeId"].ToString());
                modelObj.EmployeeName = dt.Rows[i]["EmployeeName"].ToString();
                modelObj.LeaveTypeCode = dt.Rows[i]["LeaveTypeCode"].ToString();
                modelObj.LeaveApplyDate = dt.Rows[i]["LeaveApplyDate"].ToString().Split(' ')[0];
                modelObj.LeaveFromDate = dt.Rows[i]["LeaveFromDate"].ToString().Split(' ')[0];
                modelObj.LeaveToDate = dt.Rows[i]["LeaveToDate"].ToString().Split(' ')[0];
                modelObj.LeaveDaysNo = double.Parse(dt.Rows[i]["LeaveDaysNo"].ToString());
                modelObj.LeaveProcessStatus = dt.Rows[i]["LeaveProcessStatus"].ToString();
                //EmployeeId,LeaveTypeCode,LeaveApplyDate,LeaveFromDate,LeaveToDate,LeaveDaysNo,LeaveProcessStatus
                Lst.Add(modelObj);
            }

            return Lst;

        }



        [HttpPut("UpdateStatus/{idList}")]
        public void Put(string idList, [FromBody] string value)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                List<int> ids = idList.Split(',').Select(int.Parse).ToList();
                string idListString = string.Join(",", ids);
                SqlCommand cmd = new SqlCommand("UPDATE HRM_EmployeeLeaveApplication SET LeaveProcessStatus = @value WHERE AppliedLeaveMstId IN (" + idListString + ")", con);
                cmd.Parameters.AddWithValue("@value", value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [HttpGet("GetLeaveApplicationReportData/{ApplicationId}")]
        public List<LeaveApplicationReportModel> GetLeaveApplicationReportData(string ApplicationId)
        {
            List<LeaveApplicationReportModel> Lst = new List<LeaveApplicationReportModel>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                con.Open();
                SqlCommand command = new SqlCommand("HRM_spGetEmpAppliedLeaveDtlsListForReport", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AppliedLeaveMstId", ApplicationId);


                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LeaveApplicationReportModel model = new LeaveApplicationReportModel();
                    model.applyDate= reader.GetDateTime(reader.GetOrdinal("LeaveAppliedDate")).ToString().Split(" ")[0];
                    model.employeeFullName = reader.IsDBNull(reader.GetOrdinal("EmployeeFullName")) ? "" : reader.GetString(reader.GetOrdinal("EmployeeFullName"));
                    model.fromDate= reader.GetDateTime(reader.GetOrdinal("LeaveFromDate")).ToString().Split(" ")[0];
                    model.toDate= reader.GetDateTime(reader.GetOrdinal("LeaveToDate")).ToString().Split(" ")[0];
                    model.employeDisplayeId= reader.IsDBNull(reader.GetOrdinal("EmployeeDisplayId")) ? "" : reader.GetString(reader.GetOrdinal("EmployeeDisplayId"));
                    model.employeeDepartment= reader.IsDBNull(reader.GetOrdinal("EmployeeDepartment")) ? "" : reader.GetString(reader.GetOrdinal("EmployeeDepartment"));
                    model.employeeDesignation= reader.IsDBNull(reader.GetOrdinal("EmployeeDesignation")) ? "" : reader.GetString(reader.GetOrdinal("EmployeeDesignation"));
                    model.leaveBalance= reader.IsDBNull(reader.GetOrdinal("LeaveBalance")) ? "" : reader.GetDecimal(reader.GetOrdinal("LeaveBalance")).ToString();
                    model.leaveOpeningBalance= reader.IsDBNull(reader.GetOrdinal("LeaveOpeningBalance")) ? "" : reader.GetDecimal(reader.GetOrdinal("LeaveOpeningBalance")).ToString();
                    model.leaveClosingBalance= reader.IsDBNull(reader.GetOrdinal("LeaveClosingBalance")) ? "" : reader.GetDecimal(reader.GetOrdinal("LeaveClosingBalance")).ToString();
                    model.leaveDaysNo = reader.IsDBNull(reader.GetOrdinal("LeaveDaysNo")) ? "" : reader.GetDecimal(reader.GetOrdinal("LeaveDaysNo")).ToString();
                    model.leavePresentBalance= reader.IsDBNull(reader.GetOrdinal("LeavePresentBalance")) ? "" : reader.GetDecimal(reader.GetOrdinal("LeavePresentBalance")).ToString();
                    model.leaveRestBalance= reader.IsDBNull(reader.GetOrdinal("LeaveRestBalance")) ? "" : reader.GetDecimal(reader.GetOrdinal("LeaveRestBalance")).ToString();

                    Lst.Add(model);
                }

            }
            return Lst;
        }
    }
}