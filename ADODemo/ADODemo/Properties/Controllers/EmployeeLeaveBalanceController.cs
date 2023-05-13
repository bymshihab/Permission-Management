using ADODemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ADODemo.Properties.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeLeaveBalanceController : Controller
    {

        private readonly IConfiguration _configuration;

        public EmployeeLeaveBalanceController(IConfiguration config)
        {
            _configuration = config;
        }
         

        [HttpGet]
        [Route("GetEmployeeLeaveBalance/{id}")]
        public List<EmployeeLeaveBalanceModel> GetEmployeeLeaveBalance(int id)
        {

            List<EmployeeLeaveBalanceModel> lst = new List<EmployeeLeaveBalanceModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            // SqlCommand cmd = new SqlCommand("SELECT TOP (100)  LeaveTypeCode\r\n ,LeaveBalance\r\n from  HRM_EmployeeLeaveApplication  WHERE EmployeeId =" + id +"", con);
            SqlCommand cmd = new SqlCommand("SELECT LeaveBalance FROM HRM_EmployeeLeaveBalance WHERE EmployeeId = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //if (dt.Rows.Count == 0)
            //{
            //    // id not found in table, handle the error here
            //    throw new ArgumentException("Employee ID not found in table.");
            //}

            foreach (DataRow row in dt.Rows)
            {
                EmployeeLeaveBalanceModel modelObj = new EmployeeLeaveBalanceModel();

                //modelObj.LeaveBalance = Convert.ToSingle(row["LeaveBalance"]);           
                modelObj.LeaveBalance = Convert.ToDecimal(row["LeaveBalance"]);
                lst.Add(modelObj);
            }

            return lst;
        }


        [HttpGet]
        [Route("GetEmployeePostleaveHistory/{id}")]
        public List<EmployeeLeaveBalanceModel> GetEmployeePostleaveHistory(int id)
        {
            List<EmployeeLeaveBalanceModel> lst = new List<EmployeeLeaveBalanceModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            // SqlCommand cmd = new SqlCommand("SELECT TOP (100)  LeaveTypeCode\r\n ,LeaveBalance\r\n from  HRM_EmployeeLeaveApplication  WHERE EmployeeId =" + id +"", con);
            SqlCommand cmd = new SqlCommand("SELECT EmployeeId, LeaveTypeCode,LeaveFromDate,LeaveToDate,LeaveBalance FROM HRM_EmployeeLeaveApplication WHERE EmployeeId = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                EmployeeLeaveBalanceModel modelObj = new EmployeeLeaveBalanceModel();
                modelObj.EmployeeId = int.Parse(row["EmployeeId"].ToString());
                modelObj.LeaveType = row["LeaveTypeCode"].ToString();
                modelObj.FromDate = (DateTime)row["LeaveFromDate"];
                modelObj.ToDate = (DateTime)row["LeaveToDate"];
                modelObj.LeaveBalance = Convert.ToDecimal(row["LeaveBalance"]);
                lst.Add(modelObj);
            }
            return lst;
        }

        //[HttpGet]
        //[Route("GetEmployeePostleaveHistory/{id}")]
        //public List<EmployeeLeaveBalanceModel> GetEmployeePostleaveHistory(int id)
        //{

        //    List<EmployeeLeaveBalanceModel> lst = new List<EmployeeLeaveBalanceModel>();
        //    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
        //    // SqlCommand cmd = new SqlCommand("SELECT TOP (100)  LeaveTypeCode\r\n ,LeaveBalance\r\n from  HRM_EmployeeLeaveApplication  WHERE EmployeeId =" + id +"", con);
        //    SqlCommand cmd = new SqlCommand("SELECT EmployeeId, LeaveTypeCode,LeaveFromDate,LeaveToDate LeaveBalance,LeaveRestBalance FROM HRM_EmployeeLeaveApplication WHERE EmployeeId = @id", con);
        //    cmd.Parameters.AddWithValue("@id", id);
        //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    adapter.Fill(dt);
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        EmployeeLeaveBalanceModel modelObj = new EmployeeLeaveBalanceModel();
        //        modelObj.EmployeeId = int.Parse(row["EmployeeId"].ToString());
        //        modelObj.LeaveCode = row["LeaveTypeCode"].ToString();
        //        modelObj.FromDate = (DateTime)row["LeaveFromDate"];
        //        //modelObj.ToDate = (DateTime)row["LeaveToDate"];
        //        //modelObj.LeaveDays = Convert.ToSingle(row["LeaveDaysNo"]);
        //        //modelObj.LeaveRestBalance = Convert.ToSingle(row["LeaveRestBalance"]);
        //        //modelObj.LeaveBalance = Convert.ToSingle(row["LeaveBalance"]);
        //        modelObj.LeaveBalance = Convert.ToDecimal(row["LeaveBalance"]);
        //        modelObj.LeaveRestBalance = Convert.ToDecimal(row["LeaveRestBalance"]);
        //        lst.Add(modelObj);
        //    }

        //    return lst;
        //}




        [HttpGet]
        [Route("LeaveRecommendedBy")]
        public List<EmployeeLeaveBalanceModel> LeaveRecommendedBy()
        {

            List<EmployeeLeaveBalanceModel> lst = new List<EmployeeLeaveBalanceModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
          
            SqlCommand cmd = new SqlCommand("SELECT EmployeeId ,Designation,Department FROM HRM_EmployeeEmploymentHistory ", con);
    
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
 

            foreach (DataRow row in dt.Rows)
            {
                EmployeeLeaveBalanceModel modelObj = new EmployeeLeaveBalanceModel();

                modelObj.EmployeeDesignation = row["Designation"].ToString();
                modelObj.EmployeeDepartment = row["Department"].ToString();
                modelObj.EmployeeId = int.Parse(row["EmployeeId"].ToString());

                lst.Add(modelObj);
            }

            return lst;
        }




        //[HttpPost]
        //[Route("CreateNewLeaveApplication")]
        //public void CreateNewLeaveApplication([FromForm] string NewLeaveApplicationData)
        //{
        //    if (!string.IsNullOrEmpty(NewLeaveApplicationData))
        //    {
        //        List<EmployeeLeaveBalanceModel> NewApplicationdata = JsonConvert.DeserializeObject<List<EmployeeLeaveBalanceModel>>(NewLeaveApplicationData);

        //        SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
        //        con.Open();

        //        foreach (var item in NewApplicationdata)
        //        {
        //            using (SqlCommand command = new SqlCommand("INSERT INTO HRM_EmployeeLeaveApplication (AppliedLeaveMstId ,AppliedLeaveDtlsId ,EmployeeId ,LeaveTypeCode ,LeaveApplyDate,LeaveFromDate,LeaveToDate,LeaveDaysNo,LeaveProcessStatus,LeaveProcessedBy,LeaveProcessedDate ,CompanyId ,AccessedId) VALUES ('0','0','0',@LeaveType, GETDATE(), @FromDate,@ToDate, @LeaveDays ,'','','2023-05-06',0,0)", con))
        //            {
        //                //command.Parameters.AddWithValue("@EmployeeId", item.EmployeeId);
        //                command.Parameters.AddWithValue("@LeaveType", item.LeaveType);
        //                command.Parameters.AddWithValue("@FromDate", item.FromDate);
        //                command.Parameters.AddWithValue("@ToDate", item.ToDate);
        //                command.Parameters.AddWithValue("@LeaveDays", item.LeaveDays);
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        con.Close();
        //    }
        //}

        //[HttpPost]
        //[Route("NewLeaveApplication")]
        //public void AddNewLeaveApplication([FromForm] string NewLeaveApplicationData)
        //{
        //    Console.WriteLine(NewLeaveApplicationData);
        //    if (!string.IsNullOrEmpty(NewLeaveApplicationData))
        //    {
        //        List<EmployeeLeaveBalanceModel> NewApplicationdata = JsonConvert.DeserializeObject<List<EmployeeLeaveBalanceModel>>(NewLeaveApplicationData);
        //        Console.Write(NewApplicationdata);
        //        SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
        //        con.Open();
        //        using (SqlCommand command = new SqlCommand("INSERT INTO HRM_EmployeeLeaveApplication (  AppliedLeaveMstId ,AppliedLeaveDtlsId ,EmployeeId ,LeaveTypeCode ,LeaveApplyDate   ,LeaveFromDate   ,LeaveToDate ,LeaveDaysNo ,LeaveProcessStatus ,LeaveProcessedBy,LeaveProcessedDate ,CompanyId ,AccessedId) VALUES ('0','0',@EmployeeId,@LeaveType, GETDATE(), @FromDate,@ToDate, @LeaveDays ,'','','2023-05-06',0,0)", con))
        //        {
        //            command.Parameters.AddWithValue("@EmployeeId", NewApplicationdata[0].EmployeeId);
        //            command.Parameters.AddWithValue("@LeaveType", NewApplicationdata[0].LeaveType);
        //            command.Parameters.AddWithValue("@FromDate", NewApplicationdata[0].FromDate);
        //            command.Parameters.AddWithValue("@ToDate", NewApplicationdata[0].ToDate);
        //            command.Parameters.AddWithValue("@LeaveDays", NewApplicationdata[0].LeaveDays);
        //            command.ExecuteNonQuery();
        //        }
        //        con.Close();
        //    }

        //}


        [HttpPost]
        [Route("NewLeaveApplication")]
        public void AddNewLeaveApplications([FromForm] string NewLeaveApplicationsData)
        {
            Console.WriteLine(NewLeaveApplicationsData);
            if (!string.IsNullOrEmpty(NewLeaveApplicationsData))
            {
                List<EmployeeLeaveBalanceModel> NewApplicationsData = JsonConvert.DeserializeObject<List<EmployeeLeaveBalanceModel>>(NewLeaveApplicationsData);
                Console.Write(NewApplicationsData);
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
                con.Open();
                foreach (EmployeeLeaveBalanceModel applicationData in NewApplicationsData)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO HRM_EmployeeLeaveApplication (  AppliedLeaveMstId ,AppliedLeaveDtlsId ,EmployeeId ,LeaveTypeCode ,LeaveApplyDate   ,LeaveFromDate   ,LeaveToDate ,LeaveDaysNo ,LeaveProcessStatus ,LeaveProcessedBy,LeaveProcessedDate ,CompanyId ,AccessedId) VALUES ('0','0',@EmployeeId,@LeaveType, GETDATE(), @FromDate,@ToDate, @LeaveDays ,'','','2023-05-06',0,0)", con))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", applicationData.EmployeeId);
                        command.Parameters.AddWithValue("@LeaveType", applicationData.LeaveType);
                        command.Parameters.AddWithValue("@FromDate", applicationData.FromDate);
                        command.Parameters.AddWithValue("@ToDate", applicationData.ToDate);
                        command.Parameters.AddWithValue("@LeaveDays", applicationData.LeaveDays);
                        command.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }




        [HttpPost]
        [Route("leaveMaster")]
        public void test2([FromForm] string MasterLeaveformationData)
        {
            if (!string.IsNullOrEmpty(MasterLeaveformationData))
            {
                List<EmployeeLeaveBalanceModel> LeaveInfodata = JsonConvert.DeserializeObject<List<EmployeeLeaveBalanceModel>>(MasterLeaveformationData);

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO HRM_EmployeeLeaveApplicationMaster (AppliedLeaveMstId, AppliedLeaveCode, EmployeeId, AppliedDate, RowStatus, AddedBy, CompanyId, AccessedId) " +
                                                                                                            "VALUES (77,@AppliedLeaveCode,@EmployeeId,@AppliedDate,'inserted', @EmployeeName,0, 0)", con);
                {
                    command.Parameters.AddWithValue("@AppliedLeaveCode", LeaveInfodata[0].LeaveCode);
                    command.Parameters.AddWithValue("@EmployeeId", LeaveInfodata[0].EmployeeId);
                    command.Parameters.AddWithValue("@EmployeeName", LeaveInfodata[0].EmployeeName);
                    command.Parameters.AddWithValue("@AppliedDate", DateTime.Now);
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
        }





    }

}
