using ADODemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ADODemo.Properties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SalesOrderController(IConfiguration config)
        {
            _configuration = config;
        }
        
        
        [HttpPost]
        [Route("NewOrderDetails")]
        public void AddOrderDetails([FromForm] string NewOrderDetailsData)
        {
            Console.WriteLine(NewOrderDetailsData);
            if (!string.IsNullOrEmpty(NewOrderDetailsData))
            {
                List<SalesOrderModel> NewOrderData = JsonConvert.DeserializeObject<List<SalesOrderModel>>(NewOrderDetailsData);
                Console.Write(NewOrderData);
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
                con.Open();
                foreach (SalesOrderModel applicationData in NewOrderData)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO SalesOrderDetails ( SalesOrderQty, SalesOrderRate, UnitSetId, UnitId,  Remarks,  DiscountAmount,   DiscountPercent , AddedBy, DateAdded, GoodsName) VALUES ( @SalesOrderQty , @SalesOrderRate,  @UnitSetId ,@UnitId , @Remarks, @DiscountAmount, @DiscountPercent,'user', '2023-05-06', @GoodsName )", con))
                    {
                        command.Parameters.AddWithValue("@SalesOrderQty", applicationData.SalesOrderQty);
                        command.Parameters.AddWithValue("@SalesOrderRate", applicationData.SalesOrderRate);
                        command.Parameters.AddWithValue("@UnitSetId", applicationData.UnitSetId);
                        command.Parameters.AddWithValue("@UnitId", applicationData.UnitId);
                        command.Parameters.AddWithValue("@Remarks", applicationData.Remarks);
                        command.Parameters.AddWithValue("@DiscountAmount", applicationData.DiscountAmount);

                        command.Parameters.AddWithValue("@DiscountPercent", applicationData.DiscountPercent);

                        command.Parameters.AddWithValue("@AddedBy", applicationData.AddedBy);
                        command.Parameters.AddWithValue("@DateAdded", applicationData.DateAdded);

                        command.Parameters.AddWithValue("@GoodsName", applicationData.GoodsName);

                        command.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }



    }
}
