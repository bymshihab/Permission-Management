using ADODemo.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADODemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateCollectionController : Controller
    {
        private readonly IConfiguration _configuration;
        public RateCollectionController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet]
        [Route("GetAllRateCollection")]
        public List<RateCollectionModel> GetAllRateCollection()
        {
            List<RateCollectionModel> lst = new List<RateCollectionModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand(" SELECT TOP 200 RateCollectionMasterID\r\n,RCCode\r\n,RCDate\r\n,ProjectMasterCode\r\n,ApprovedBy\r\n,ApproveDate\r\n" +
                ",ApproveStatus\r\n,ApproveBit,CompanyId\r\n,AddedBy\r\n ,AddedPC\r\n,DateAdded,UpdatedBy\r\n,DateUpdated\r\n ,UpdatedPC\r\n" + "FROM INV_RateCollectionMaster ORDER BY RCDate DESC ;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RateCollectionModel model = new RateCollectionModel();
                model.RateCollectionMasterID = int.Parse(dt.Rows[i]["RateCollectionMasterID"].ToString());
                model.RCCode = dt.Rows[i]["RCCode"].ToString();
                model.RCDate = dt.Rows[i]["RCDate"].ToString().Split(' ')[0];
                model.ProjectMasterCode = dt.Rows[i]["ProjectMasterCode"].ToString();
                model.ApprovedBy = dt.Rows[i]["ApprovedBy"].ToString();
                model.ApproveDate = dt.Rows[i]["ApproveDate"].ToString();
                model.ApproveStatus = dt.Rows[i]["ApproveStatus"].ToString();
                model.ApproveBit = dt.Rows[i]["ApproveBit"].ToString();
                model.CompanyId = dt.Rows[i]["CompanyId"].ToString();
                model.AddedBy = dt.Rows[i]["AddedBy"].ToString();
                model.DateAdded = dt.Rows[i]["DateAdded"].ToString();
                model.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                model.DateUpdated = dt.Rows[i]["DateUpdated"].ToString();



                lst.Add(model);

            }

            return lst;
        }


        [HttpGet]
        [Route("GetAllNewRateCollection")]
        public List<RateCollectionModel> GetNewRateCollection()
        {
            List<RateCollectionModel> lst = new List<RateCollectionModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            //SqlCommand cmd = new SqlCommand(" SELECT TOP 200 RateCollectionMasterID,RCCode,RCDate,ProjectMasterCode,ApproveStatus" +
            //    " CompanyId,AddedBy,DateAdded" + "FROM INV_RateCollectionMaster  WHERE ApproveStatus = 'New';", con);

            SqlCommand cmd = new SqlCommand(" SELECT TOP 200 RateCollectionMasterID\r\n,RCCode\r\n,RCDate\r\n,ProjectMasterCode\r\n,ApprovedBy\r\n,ApproveDate\r\n" +
                ",ApproveStatus\r\n,ApproveBit,CompanyId\r\n,AddedBy\r\n ,AddedPC\r\n,DateAdded,UpdatedBy\r\n,DateUpdated\r\n ,UpdatedPC\r\n" + "FROM INV_RateCollectionMaster  WHERE ApproveStatus = 'New' ORDER BY RCDate DESC ;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RateCollectionModel model = new RateCollectionModel();
                model.RateCollectionMasterID = int.Parse(dt.Rows[i]["RateCollectionMasterID"].ToString());
                model.RCCode = dt.Rows[i]["RCCode"].ToString();
                model.RCDate = dt.Rows[i]["RCDate"].ToString().Split(' ')[0];
                model.ProjectMasterCode = dt.Rows[i]["ProjectMasterCode"].ToString();
                model.ApprovedBy = dt.Rows[i]["ApprovedBy"].ToString();
                model.ApproveDate = dt.Rows[i]["ApproveDate"].ToString();
                model.ApproveStatus = dt.Rows[i]["ApproveStatus"].ToString();
                model.ApproveBit = dt.Rows[i]["ApproveBit"].ToString();
                model.CompanyId = dt.Rows[i]["CompanyId"].ToString();
                model.AddedBy = dt.Rows[i]["AddedBy"].ToString();
                model.DateAdded = dt.Rows[i]["DateAdded"].ToString();
                model.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                model.DateUpdated = dt.Rows[i]["DateUpdated"].ToString();

                lst.Add(model);

            }

            return lst;
        }


        [HttpGet]
        [Route("GetAllApprovedRateCollection")]
        public List<RateCollectionModel> GetAllApprovedRateCollection()
        {
            List<RateCollectionModel> lst = new List<RateCollectionModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            //SqlCommand cmd = new SqlCommand("SELECT TOP 200 RateCollectionMasterID,RCCode,RCDate,ProjectMasterCode,ApproveStatus" +
            //    " CompanyId,AddedBy,DateAdded" + "FROM INV_RateCollectionMaster  WHERE ApproveStatus = 'New';", con);

            SqlCommand cmd = new SqlCommand("SELECT TOP 200 RateCollectionMasterID\r\n,RCCode\r\n,RCDate\r\n,ProjectMasterCode\r\n,ApprovedBy\r\n,ApproveDate\r\n" +
                ",ApproveStatus\r\n,ApproveBit,CompanyId\r\n,AddedBy\r\n ,AddedPC\r\n,DateAdded,UpdatedBy\r\n,DateUpdated\r\n ,UpdatedPC\r\n" + "FROM INV_RateCollectionMaster  WHERE ApproveStatus = 'Approved' ORDER BY RCDate DESC ;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RateCollectionModel model = new RateCollectionModel();
                model.RateCollectionMasterID = int.Parse(dt.Rows[i]["RateCollectionMasterID"].ToString());
                model.RCCode = dt.Rows[i]["RCCode"].ToString();
                model.RCDate = dt.Rows[i]["RCDate"].ToString().Split(' ')[0];
                model.ProjectMasterCode = dt.Rows[i]["ProjectMasterCode"].ToString();
                model.ApprovedBy = dt.Rows[i]["ApprovedBy"].ToString();
                model.ApproveDate = dt.Rows[i]["ApproveDate"].ToString();
                model.ApproveStatus = dt.Rows[i]["ApproveStatus"].ToString();
                model.ApproveBit = dt.Rows[i]["ApproveBit"].ToString();
                model.CompanyId = dt.Rows[i]["CompanyId"].ToString();
                model.AddedBy = dt.Rows[i]["AddedBy"].ToString();
                model.DateAdded = dt.Rows[i]["DateAdded"].ToString();
                model.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                model.DateUpdated = dt.Rows[i]["DateUpdated"].ToString();
                lst.Add(model);
            }

            return lst;
        }


        [HttpGet]
        [Route("GetAllDisApprovedRateCollection")]
        public List<RateCollectionModel> GetAllDisApprovedRateCollection()
        {
            List<RateCollectionModel> lst = new List<RateCollectionModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            //SqlCommand cmd = new SqlCommand(" SELECT TOP 200 RateCollectionMasterID,RCCode,RCDate,ProjectMasterCode,ApproveStatus" +
            //    " CompanyId,AddedBy,DateAdded" + "FROM INV_RateCollectionMaster  WHERE ApproveStatus = 'New';", con);

            SqlCommand cmd = new SqlCommand("SELECT TOP 200 RateCollectionMasterID\r\n,RCCode\r\n,RCDate\r\n,ProjectMasterCode\r\n,ApprovedBy\r\n,ApproveDate\r\n" +
                ",ApproveStatus\r\n,ApproveBit,CompanyId\r\n,AddedBy\r\n ,AddedPC\r\n,DateAdded,UpdatedBy\r\n,DateUpdated\r\n ,UpdatedPC\r\n" + "FROM INV_RateCollectionMaster  WHERE ApproveStatus = 'DisApproved' ORDER BY RCDate DESC ;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RateCollectionModel model = new RateCollectionModel();
                model.RateCollectionMasterID = int.Parse(dt.Rows[i]["RateCollectionMasterID"].ToString());
                model.RCCode = dt.Rows[i]["RCCode"].ToString();
                model.RCDate = dt.Rows[i]["RCDate"].ToString().Split(' ')[0];
                model.ProjectMasterCode = dt.Rows[i]["ProjectMasterCode"].ToString();
                model.ApprovedBy = dt.Rows[i]["ApprovedBy"].ToString();
                model.ApproveDate = dt.Rows[i]["ApproveDate"].ToString();
                model.ApproveStatus = dt.Rows[i]["ApproveStatus"].ToString();
                model.ApproveBit = dt.Rows[i]["ApproveBit"].ToString();
                model.CompanyId = dt.Rows[i]["CompanyId"].ToString();
                model.AddedBy = dt.Rows[i]["AddedBy"].ToString();
                model.DateAdded = dt.Rows[i]["DateAdded"].ToString();
                model.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                model.DateUpdated = dt.Rows[i]["DateUpdated"].ToString();
                lst.Add(model);
            }
            return lst;
        }



        [HttpPut("UpdateStatus/{idList}")]
        public void Put(string idList, [FromBody] string value)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                List<int> ids = idList.Split(',').Select(int.Parse).ToList();
                string idListString = string.Join(",", ids);
                SqlCommand cmd = new SqlCommand("UPDATE INV_RateCollectionMaster SET ApproveStatus = @value WHERE RateCollectionMasterID IN (" + idListString + ")", con);
                cmd.Parameters.AddWithValue("@value", value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }



    }
}