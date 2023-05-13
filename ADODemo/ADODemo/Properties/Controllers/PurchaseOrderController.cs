using ADODemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ADODemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PurchaseOrderController(IConfiguration config)
        {
            _configuration = config;
        }
        [HttpGet]
        [Route("GetAllPurchaseOrder")]
        public List<PurchaseOrderModel> GetAllPurchaseOrder()
        {
            List<PurchaseOrderModel> Lst = new List<PurchaseOrderModel>();
           
            string sql = "SELECT PO.PurchaseId, VM.VendorName, PO.PurchaseOrderDate, T.TypeName, PO.VendorSelectionNo, PO.OthersCostAmount, PO.DeliveryAddress, PO.ApprovedBy, PO.ApproveDate, PO.ApproveStatus " +
             "FROM (SELECT TOP 200 * FROM INV_PurchaseOrderMaster) AS PO " +
             "INNER JOIN VendorMaster AS VM ON VM.VendorCode = PO.VendorCode " +
             "INNER JOIN (" +
             "    SELECT 'T-0001' TypeCode, 'LC' TypeName " +
             "    UNION " +
             "    SELECT 'T-0002' TypeCode, 'Non-LC' TypeName " +
             ") AS T ON T.TypeCode = PO.TypeCode " +
             "ORDER BY PurchaseOrderDate DESC;";

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseOrderModel purchase = new PurchaseOrderModel();
                    purchase.PurchaseID= reader.GetInt32(0);
                    purchase.VendorName = reader.GetString(1);
                    purchase.PurchaseDate = reader.GetDateTime(2).ToString().Split(' ')[0];
                    purchase.PurchaseTypeName = reader.GetString(3);
                    purchase.VendorSelectionNo = reader.GetString(4);
                    purchase.OtherCostAmount = reader.GetValue(5) != DBNull.Value ? reader.GetValue(4).ToString() : null;
                    purchase.DeliveryAddress = reader.GetValue(6) != DBNull.Value ? reader.GetValue(5).ToString() : null;
                    purchase.ApprovedBy = reader.GetValue(7) != DBNull.Value ? reader.GetValue(6).ToString() : null;
                    purchase.ApprovedDate = reader.GetValue(8) != DBNull.Value ? reader.GetValue(7).ToString().Split(' ')[0] : null;
                    purchase.ApproveStatus = reader.GetString(9);
                   Lst.Add(purchase);
                }
                reader.Close();
            }


            return Lst;

        }

        [HttpGet]
        [Route("GetAllNewPurchaseOrder")]
        public List<PurchaseOrderModel> GetAllNewPurchaseOrder()
        {
            List<PurchaseOrderModel> Lst = new List<PurchaseOrderModel>();

            string sql = "SELECT PO.PurchaseId,PO.PurchaseOrderMasterCode, VM.VendorName, PO.PurchaseOrderDate,T.TypeName, PO.VendorSelectionNo, PO.OthersCostAmount, PO.DeliveryAddress, PO.ApprovedBy, PO.ApproveDate, PO.ApproveStatus " +
                    "FROM (SELECT TOP 200 * FROM INV_PurchaseOrderMaster) AS PO " +
                    "INNER JOIN VendorMaster AS VM ON VM.VendorCode = PO.VendorCode " +
                    "INNER JOIN ( " +
                    "    SELECT 'T-0001' TypeCode, 'LC' TypeName " +
                    "    UNION " +
                    "    SELECT 'T-0002' TypeCode, 'Non-LC' TypeName " +
                    ") AS T ON T.TypeCode = PO.TypeCode " +
                    "WHERE PO.ApproveStatus = 'Pending' " +
                    "ORDER BY PurchaseOrderDate DESC;";


            //It's a scope for sql object
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseOrderModel purchase = new PurchaseOrderModel();
                    purchase.PurchaseID = reader.GetInt32(0);
                    purchase.PurchaseOrderCode = reader.GetString(1);
                    purchase.VendorName = reader.GetString(2);
                    purchase.PurchaseDate = reader.GetDateTime(3).ToString().Split(' ')[0];
                    purchase.PurchaseTypeName = reader.GetString(4);
                    purchase.VendorSelectionNo = reader.GetString(5);
                    purchase.OtherCostAmount = reader.GetValue(6) != DBNull.Value ? reader.GetValue(6).ToString() : null;
                    purchase.DeliveryAddress = reader.GetValue(7) != DBNull.Value ? reader.GetValue(7).ToString() : null;
                    purchase.ApprovedBy = reader.GetValue(8) != DBNull.Value ? reader.GetValue(8).ToString() : null;
                    purchase.ApprovedDate = reader.GetValue(9) != DBNull.Value ? reader.GetValue(9).ToString().Split(' ')[0] : null;
                    purchase.ApproveStatus = reader.GetString(10);
                    Lst.Add(purchase);
                }
                reader.Close();
            }
            return Lst;
        }

       // All ApprovedData 

        [HttpGet]
        [Route("GetAllApprovedPurchaseOrder")]
        public List<PurchaseOrderModel> GetAllApprovedPurchaseOrder()
        {
            List<PurchaseOrderModel> Lst = new List<PurchaseOrderModel>();

            string sql = "SELECT PO.PurchaseId,PO.PurchaseOrderMasterCode, VM.VendorName, PO.PurchaseOrderDate,T.TypeName, PO.VendorSelectionNo, PO.OthersCostAmount, PO.DeliveryAddress, PO.ApprovedBy, PO.ApproveDate, PO.ApproveStatus " +
                    "FROM (SELECT TOP 200 * FROM INV_PurchaseOrderMaster) AS PO " +
                    "INNER JOIN VendorMaster AS VM ON VM.VendorCode = PO.VendorCode " +
                    "INNER JOIN ( " +
                    "    SELECT 'T-0001' TypeCode, 'LC' TypeName " +
                    "    UNION " +
                    "    SELECT 'T-0002' TypeCode, 'Non-LC' TypeName " +
                    ") AS T ON T.TypeCode = PO.TypeCode " +
                    "WHERE PO.ApproveStatus = 'Approved' " +
                    "ORDER BY PurchaseOrderDate DESC;";


            //It's a scope for sql object
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseOrderModel purchase = new PurchaseOrderModel();
                    purchase.PurchaseID = reader.GetInt32(0);
                    purchase.PurchaseOrderCode = reader.GetString(1);
                    purchase.VendorName = reader.GetString(2);
                    purchase.PurchaseDate = reader.GetDateTime(3).ToString().Split(' ')[0];
                    purchase.PurchaseTypeName = reader.GetString(4);
                    purchase.VendorSelectionNo = reader.GetString(5);
                    purchase.OtherCostAmount = reader.GetValue(6) != DBNull.Value ? reader.GetValue(6).ToString() : null;
                    purchase.DeliveryAddress = reader.GetValue(7) != DBNull.Value ? reader.GetValue(7).ToString() : null;
                    purchase.ApprovedBy = reader.GetValue(8) != DBNull.Value ? reader.GetValue(8).ToString() : null;
                    purchase.ApprovedDate = reader.GetValue(9) != DBNull.Value ? reader.GetValue(9).ToString().Split(' ')[0] : null;
                    purchase.ApproveStatus = reader.GetString(10);
                    Lst.Add(purchase);
                }
                reader.Close();
            }
            return Lst;
        }



        // All DisApprovedData 
        [HttpGet]
        [Route("GetAllDisApprovedPurchaseOrder")]
        public List<PurchaseOrderModel> GetAllDisApprovedPurchaseOrder()
        {
            List<PurchaseOrderModel> Lst = new List<PurchaseOrderModel>();

            string sql = "SELECT PO.PurchaseId,PO.PurchaseOrderMasterCode, VM.VendorName, PO.PurchaseOrderDate,T.TypeName, PO.VendorSelectionNo, PO.OthersCostAmount, PO.DeliveryAddress, PO.ApprovedBy, PO.ApproveDate, PO.ApproveStatus " +
                    "FROM (SELECT TOP 200 * FROM INV_PurchaseOrderMaster) AS PO " +
                    "INNER JOIN VendorMaster AS VM ON VM.VendorCode = PO.VendorCode " +
                    "INNER JOIN ( " +
                    "    SELECT 'T-0001' TypeCode, 'LC' TypeName " +
                    "    UNION " +
                    "    SELECT 'T-0002' TypeCode, 'Non-LC' TypeName " +
                    ") AS T ON T.TypeCode = PO.TypeCode " +
                    "WHERE PO.ApproveStatus = 'DisApproved' " +
                    "ORDER BY PurchaseOrderDate DESC;";


            //It's a scope for sql object
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseOrderModel purchase = new PurchaseOrderModel();
                    purchase.PurchaseID = reader.GetInt32(0);
                    purchase.PurchaseOrderCode = reader.GetString(1);
                    purchase.VendorName = reader.GetString(2);
                    purchase.PurchaseDate = reader.GetDateTime(3).ToString().Split(' ')[0];
                    purchase.PurchaseTypeName = reader.GetString(4);
                    purchase.VendorSelectionNo = reader.GetString(5);
                    purchase.OtherCostAmount = reader.GetValue(6) != DBNull.Value ? reader.GetValue(6).ToString() : null;
                    purchase.DeliveryAddress = reader.GetValue(7) != DBNull.Value ? reader.GetValue(7).ToString() : null;
                    purchase.ApprovedBy = reader.GetValue(8) != DBNull.Value ? reader.GetValue(8).ToString() : null;
                    purchase.ApprovedDate = reader.GetValue(9) != DBNull.Value ? reader.GetValue(9).ToString().Split(' ')[0] : null;
                    purchase.ApproveStatus = reader.GetString(10);
                    Lst.Add(purchase);
                }
                reader.Close();
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
                SqlCommand cmd = new SqlCommand("UPDATE INV_PurchaseOrderMaster SET ApproveStatus = @value WHERE PurchaseId IN (" + idListString + ")", con);
                cmd.Parameters.AddWithValue("@value", value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        [HttpGet("GetPurchaseReportData/{PO}")]
        public List<PurchaseReportModel> GetPurchaseReportData(string PO)
        {
            List<PurchaseReportModel> Lst = new List<PurchaseReportModel>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2")))
            {
                con.Open();
                SqlCommand command = new SqlCommand("INV_rptPurchaseOrder", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PurchaseOrderMasterCode", PO);


                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseReportModel model = new PurchaseReportModel();
                  
                    // Populate the object's properties from the data reader
                    model.CompanyShortName = reader.IsDBNull(reader.GetOrdinal("CompanyShortName")) ? "" : reader.GetString(reader.GetOrdinal("CompanyShortName"));
                    model.VatRegNo = reader.IsDBNull(reader.GetOrdinal("VatRegNo")) ? "" : reader.GetString(reader.GetOrdinal("VatRegNo"));
                    model.PO = reader.IsDBNull(reader.GetOrdinal("PO")) ? "" : reader.GetString(reader.GetOrdinal("PO"));
                    model.PODate = reader.GetDateTime(reader.GetOrdinal("PODate")).ToString().Split(" ")[0];
                    model.VendorName = reader.IsDBNull(reader.GetOrdinal("VendorName")) ? "" : reader.GetString(reader.GetOrdinal("VendorName"));
                    model.DeliveryAddress = reader.IsDBNull(reader.GetOrdinal("DeliveryAddress")) ? "" : reader.GetString(reader.GetOrdinal("DeliveryAddress"));
                    model.OwnerName = reader.IsDBNull(reader.GetOrdinal("OwnerName")) ? "" : reader.GetString(reader.GetOrdinal("OwnerName"));
                    model.OfficeAddress = reader.IsDBNull(reader.GetOrdinal("OfficeAddress")) ? "" : reader.GetString(reader.GetOrdinal("OfficeAddress"));
                    model.DeliveryContactPerson = reader.IsDBNull(reader.GetOrdinal("DeliveryContractPerson")) ? "" : reader.GetString(reader.GetOrdinal("DeliveryContractPerson"));
                    model.ContactPersonName = reader.IsDBNull(reader.GetOrdinal("ContactPersonName")) ? "" : reader.GetString(reader.GetOrdinal("ContactPersonName"));
                    model.MobileNumber = reader.IsDBNull(reader.GetOrdinal("MobileNumber")) ? "" : reader.GetString(reader.GetOrdinal("MobileNumber"));
                    model.RequisitionNo = reader.IsDBNull(reader.GetOrdinal("RequisitonNo")) ? "" : reader.GetString(reader.GetOrdinal("RequisitonNo"));
                    model.DisCountPercent =reader.IsDBNull(reader.GetOrdinal("DisCountPercent")) ? "0": reader.GetDecimal(reader.GetOrdinal("DisCountPercent")).ToString();
                    model.DiscountTk = reader.IsDBNull(reader.GetOrdinal("DiscountTk")) ? "0" : reader.GetDecimal(reader.GetOrdinal("DiscountTk")).ToString();
                    model.OriginalAmount = reader.IsDBNull(reader.GetOrdinal("OriginalAmount")) ? "0" : reader.GetDecimal(reader.GetOrdinal("OriginalAmount")).ToString();
                    model.AfterDisCountAmount = reader.IsDBNull(reader.GetOrdinal("AfterDisCountAmount")) ? "0" : reader.GetDecimal(reader.GetOrdinal("AfterDisCountAmount")).ToString();
                    model.Description = reader.IsDBNull(reader.GetOrdinal("Dscription")) ? "" : reader.GetString(reader.GetOrdinal("Dscription"));
                    model.ProjectShortName = reader.IsDBNull(reader.GetOrdinal("ProjectShortName")) ? "" : reader.GetString(reader.GetOrdinal("ProjectShortName"));
                    model.Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? "" : reader.GetString(reader.GetOrdinal("Unit"));
                    model.Qty = reader.IsDBNull(reader.GetOrdinal("Qty")) ? "0" : reader.GetDecimal(reader.GetOrdinal("Qty")).ToString();
                    model.UnitRate = reader.IsDBNull(reader.GetOrdinal("UnitRate")) ? "0" : reader.GetDecimal(reader.GetOrdinal("UnitRate")).ToString();
                    model.Amount = reader.IsDBNull(reader.GetOrdinal("Amount")) ? "0" : reader.GetDecimal(reader.GetOrdinal("Amount")).ToString();
                    model.TermsCondition = reader.IsDBNull(reader.GetOrdinal("TermsAndConditions")) ? "" : reader.GetString(reader.GetOrdinal("TermsAndConditions"));
                    model.ApprovedBy = reader.IsDBNull(reader.GetOrdinal("ApprovedBy")) ? "" : reader.GetString(reader.GetOrdinal("ApprovedBy"));
                    model.Designation = reader.IsDBNull(reader.GetOrdinal("Designation")) ? "" : reader.GetString(reader.GetOrdinal("Designation"));
                    model.QuotionNo = reader.IsDBNull(reader.GetOrdinal("QuotationNo")) ? "" : reader.GetString(reader.GetOrdinal("QuotationNo"));
                    model.RcDate = reader.IsDBNull(reader.GetOrdinal("RcDate")) ? "" : reader.GetDateTime(reader.GetOrdinal("RcDate")).ToString().Split(" ")[0];
                    model.OtherCost = reader.IsDBNull(reader.GetOrdinal("OthersCost")) ? "" : reader.GetString(reader.GetOrdinal("OthersCost"));
                    model.OtherCostAmount = reader.IsDBNull(reader.GetOrdinal("OthersCostAmount")) ? "0" : reader.GetDecimal(reader.GetOrdinal("OthersCostAmount")).ToString();
                    model.AddedByName = reader.IsDBNull(reader.GetOrdinal("AddedByName")) ? "" : reader.GetString(reader.GetOrdinal("AddedByName"));
                    model.Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name"));
                    model.BrandName = reader.IsDBNull(reader.GetOrdinal("BrandName")) ? "" : reader.GetString(reader.GetOrdinal("BrandName"));
                    model.ApprovedByName = reader.IsDBNull(reader.GetOrdinal("ApprovedByName")) ? null : reader.GetString(reader.GetOrdinal("ApprovedByName"));
                    model.ApprovedByDesignation = reader.IsDBNull(reader.GetOrdinal("ApprovedByDesignation")) ? null : reader.GetString(reader.GetOrdinal("ApprovedByDesignation"));
                    model.LcNo = reader.IsDBNull(reader.GetOrdinal("LcNo")) ? "" : reader.GetString(reader.GetOrdinal("LcNo"));

                    Lst.Add(model);
                }
            }



            return Lst;

        }



    }
}
