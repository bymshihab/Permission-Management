
using ADODemo.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ADODemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialStockController : Controller
    {
        private readonly IConfiguration _configuration;
        public MaterialStockController(IConfiguration config)

        {
            _configuration = config;
        }
        [HttpGet("GetAllMaterialStocks")]
        public List<MaterialStockModel> GetAllMaterialStocks()
        {
            List<MaterialStockModel> lst = new List<MaterialStockModel>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                con.Open();
                SqlCommand command = new SqlCommand("MaterialStockForAll", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MaterialStockModel model = new MaterialStockModel();
            
                    model.GoodsName = reader.GetString(reader.GetOrdinal("GoodsName")) ;
                    model.Specification = reader.GetString(reader.GetOrdinal("Specification")) ;
                    model.StockQty = Convert.ToSingle(reader.GetDecimal(reader.GetOrdinal("StockQty")));
                    model.ProjectName = reader.GetString(reader.GetOrdinal("ProjectName"));
 
                    lst.Add(model);
                }
            }
            return lst;
        }
        [HttpPost]
       
        [Route("MaterialStocksUpdate")]
        public void MaterialStocksUpdate([FromForm] String approvedStock)
        {
            Console.WriteLine(approvedStock);
            if (!string.IsNullOrEmpty(approvedStock))
            {
                List<MaterialStockModel> StockList = JsonConvert.DeserializeObject<List<MaterialStockModel>>(approvedStock);
                Console.Write(StockList);

                string InsertSql = "INSERT INTO MaterialStockForSales ( GoodsName,Specification,StockQty, ApprovedQty,ProjectName ) VALUES ";
                for (int i = 0; i < StockList.Count; i++)
                {
                    InsertSql += $"( '{StockList[i].GoodsName}','{StockList[i].Specification}',{Convert.ToSingle(StockList[i].StockQty)},{Convert.ToSingle(StockList[i].ApproveQty)},'{StockList[i].ProjectName}')";
                    if (i < StockList.Count - 1)
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

        //[HttpPut("UpdateStatus/{idList}")]
        //public void Put(string idList, [FromBody] string value)
        //{
        //    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
        //    {
        //        List<int> ids = idList.Split(',').Select(int.Parse).ToList();
        //        string idListString = string.Join(",", ids);
        //        SqlCommand cmd = new SqlCommand("UPDATE HRM_EmployeeLeaveApplication SET LeaveProcessStatus = @value WHERE AppliedLeaveMstId IN (" + idListString + ")", con);
        //        cmd.Parameters.AddWithValue("@value", value);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}


    }
}
