using ADODemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;

namespace ADODemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialRequisitionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MaterialRequisitionController(IConfiguration config)
        {
            _configuration = config;
        }
        [HttpGet]
        [Route("GetAllMaterialRequisition")]
        public List<MaterialRequisitionModel> GetAllMaterialRequisition()
        {
            List<MaterialRequisitionModel> Lst = new List<MaterialRequisitionModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("SELECT ELA.MRequisitionId, ELA.MRequisitionCode, CONCAT(EMA.EmployeeFirstName, ' ', EMA.EmployeeMiddleName, ' ', EMA.EmployeeLastName) AS RequisitionBy, ELA.ProjectMasterCode, ELA.ManualRequisitionNo, ELA.RequisitionDate, ELA.Status " +
             "FROM (SELECT TOP 200 * FROM INV_MaterialRequisitionMaster) AS ELA " +
             "INNER JOIN HRM_EmployeeMaster AS EMA ON EMA.EmployeeCode = ELA.RequisitionBy " +
             "ORDER BY RequisitionDate DESC;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MaterialRequisitionModel modelObj = new MaterialRequisitionModel();
                modelObj.MRequisitionId = int.Parse(dt.Rows[i]["MRequisitionId"].ToString());
                modelObj.MRequisitionCode= dt.Rows[i]["MRequisitionCode"].ToString();
                modelObj.ProjectMasterCode = dt.Rows[i]["ProjectMasterCode"].ToString();
                modelObj.ManualRequisitionNo= dt.Rows[i]["ManualRequisitionNo"].ToString();
                modelObj.RequisitionBy = dt.Rows[i]["RequisitionBy"].ToString();
                modelObj.Date = dt.Rows[i]["RequisitionDate"].ToString().Split(' ')[0];
                modelObj.Status = dt.Rows[i]["Status"].ToString();
                //EmployeeId,LeaveTypeCode,LeaveApplyDate,LeaveFromDate,LeaveToDate,LeaveDaysNo,LeaveProcessStatus
                Lst.Add(modelObj);
            }

            return Lst;

        }

        [HttpGet]
        [Route("GetAllMRequisitionPending")]
        public List<MaterialRequisitionModel> GetAllMRequisitionPending()
        {
            List<MaterialRequisitionModel> Lst = new List<MaterialRequisitionModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("SELECT ELA.MRequisitionId, ELA.MRequisitionCode, CONCAT(EMA.EmployeeFirstName, ' ', EMA.EmployeeMiddleName, ' ', EMA.EmployeeLastName) AS RequisitionBy, ELA.ProjectMasterCode, ELA.ManualRequisitionNo, ELA.RequisitionDate, ELA.Status FROM (SELECT TOP 200 * FROM INV_MaterialRequisitionMaster) AS ELA INNER JOIN HRM_EmployeeMaster AS EMA ON EMA.EmployeeCode = ELA.RequisitionBy WHERE ELA.Status = 'Checked' ORDER BY RequisitionDate DESC;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MaterialRequisitionModel modelObj = new MaterialRequisitionModel();
                modelObj.MRequisitionId = int.Parse(dt.Rows[i]["MRequisitionId"].ToString());
                modelObj.MRequisitionCode = dt.Rows[i]["MRequisitionCode"].ToString();
                modelObj.ProjectMasterCode = dt.Rows[i]["ProjectMasterCode"].ToString();
                modelObj.ManualRequisitionNo = dt.Rows[i]["ManualRequisitionNo"].ToString();
                modelObj.RequisitionBy = dt.Rows[i]["RequisitionBy"].ToString();
                modelObj.Date = dt.Rows[i]["RequisitionDate"].ToString().Split(' ')[0];
                modelObj.Status = dt.Rows[i]["Status"].ToString();
                //EmployeeId,LeaveTypeCode,LeaveApplyDate,LeaveFromDate,LeaveToDate,LeaveDaysNo,LeaveProcessStatus
                Lst.Add(modelObj);
            }

            return Lst;

        }
        [HttpGet]
        [Route("GetAllMRequisitionApproved")]
        public List<MaterialRequisitionModel> GetAllMRequisitionApproved()
        {
            List<MaterialRequisitionModel> Lst = new List<MaterialRequisitionModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("SELECT ELA.MRequisitionId, ELA.MRequisitionCode, CONCAT(EMA.EmployeeFirstName, ' ', EMA.EmployeeMiddleName, ' ', EMA.EmployeeLastName) AS RequisitionBy, ELA.ProjectMasterCode, ELA.ManualRequisitionNo, ELA.RequisitionDate, ELA.Status FROM (SELECT TOP 200 * FROM INV_MaterialRequisitionMaster) AS ELA INNER JOIN HRM_EmployeeMaster AS EMA ON EMA.EmployeeCode = ELA.RequisitionBy WHERE ELA.Status = 'Approved' ORDER BY RequisitionDate DESC;", con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MaterialRequisitionModel modelObj = new MaterialRequisitionModel();
                modelObj.MRequisitionId = int.Parse(dt.Rows[i]["MRequisitionId"].ToString());
                modelObj.MRequisitionCode = dt.Rows[i]["MRequisitionCode"].ToString();
                modelObj.ProjectMasterCode = dt.Rows[i]["ProjectMasterCode"].ToString();
                modelObj.ManualRequisitionNo = dt.Rows[i]["ManualRequisitionNo"].ToString();
                modelObj.RequisitionBy = dt.Rows[i]["RequisitionBy"].ToString();
                modelObj.Date = dt.Rows[i]["RequisitionDate"].ToString().Split(' ')[0];
                modelObj.Status = dt.Rows[i]["Status"].ToString();
                //EmployeeId,LeaveTypeCode,LeaveApplyDate,LeaveFromDate,LeaveToDate,LeaveDaysNo,LeaveProcessStatus
                Lst.Add(modelObj);
            }

            return Lst;

        }
        [HttpGet]
        [Route("GetAllMRequisitionDisApproved")]
        public List<MaterialRequisitionModel> GetAllMRequisitionDisApproved()
        {
            List<MaterialRequisitionModel> Lst = new List<MaterialRequisitionModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("SELECT ELA.MRequisitionId, ELA.MRequisitionCode, CONCAT(EMA.EmployeeFirstName, ' ', EMA.EmployeeMiddleName, ' ', EMA.EmployeeLastName) AS RequisitionBy, ELA.ProjectMasterCode, ELA.ManualRequisitionNo, ELA.RequisitionDate, ELA.Status FROM (SELECT TOP 200 * FROM INV_MaterialRequisitionMaster) AS ELA INNER JOIN HRM_EmployeeMaster AS EMA ON EMA.EmployeeCode = ELA.RequisitionBy WHERE ELA.Status = 'DisApproved' ORDER BY RequisitionDate DESC;", con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MaterialRequisitionModel modelObj = new MaterialRequisitionModel();
                modelObj.MRequisitionId = int.Parse(dt.Rows[i]["MRequisitionId"].ToString());
                modelObj.MRequisitionCode = dt.Rows[i]["MRequisitionCode"].ToString();
                modelObj.ProjectMasterCode = dt.Rows[i]["ProjectMasterCode"].ToString();
                modelObj.ManualRequisitionNo = dt.Rows[i]["ManualRequisitionNo"].ToString();
                modelObj.RequisitionBy = dt.Rows[i]["RequisitionBy"].ToString();
                modelObj.Date = dt.Rows[i]["RequisitionDate"].ToString().Split(' ')[0];
                modelObj.Status = dt.Rows[i]["Status"].ToString();
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
                SqlCommand cmd = new SqlCommand("UPDATE INV_MaterialRequisitionMaster SET Status = @value WHERE MRequisitionId IN (" + idListString + ")", con);
                cmd.Parameters.AddWithValue("@value", value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [HttpPost]
        [Route("AddMaterialWithRemarks")]
        public void AddMaterialWithRemarks([FromForm]String insertRequisition)
        {
            Console.WriteLine(insertRequisition);
            if (!string.IsNullOrEmpty(insertRequisition))
            {
                List<MaterialRequisitionWithRemarksModel> RequisionList = JsonConvert.DeserializeObject<List<MaterialRequisitionWithRemarksModel>>(insertRequisition);
                Console.Write(RequisionList);
                
                        string InsertSql = "INSERT INTO  RequisitionWithRemarks (MRequisitionId,RequisitionBy,MRequisitionCode,ProjectMasterCode,RequisitionDate,Remarks) VALUES ";
                        for (int i = 0; i < RequisionList.Count; i++)
                        {                          
                    InsertSql += $"('{RequisionList[i].MRequisitionId}','{RequisionList[i].RequisitionBy}','{RequisionList[i].MRequisitionCode}'," +
                        $"'{RequisionList[i].ProjectMasterCode}','{RequisionList[i].RequisitionDate}','{RequisionList[i].Remarks}')";
                    if (i < RequisionList.Count - 1)
                    {
                        InsertSql += ",";
                    }
                }
                        Console.WriteLine(InsertSql);
                        SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
                        SqlCommand com = new SqlCommand(InsertSql, con);
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
             }
             
        }
    }
}
