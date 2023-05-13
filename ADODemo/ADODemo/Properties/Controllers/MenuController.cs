using ADODemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ADODemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MenuController(IConfiguration config)
        {
            _configuration = config;
        }

       
        [HttpGet]
        [Route("GetAllMenuList")]
        public List<MenuModel> GetAllProduct()
        {
            List<MenuModel> Lst = new List<MenuModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
            SqlCommand cmd = new SqlCommand("Select * from PermissionSidebarMenu", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MenuModel modelObj = new MenuModel();
               
                modelObj.Menu_ID = int.Parse(dt.Rows[i]["Menu_ID"].ToString());
                modelObj.Menu_Name = dt.Rows[i]["Menu_Name"].ToString();
                modelObj.parent_ID = int.Parse(dt.Rows[i]["Parent_ID"].ToString());
                modelObj.page_Name = dt.Rows[i]["Page_Name"].ToString();

                Lst.Add(modelObj);
            }

            return Lst;

        }

        


    }
}
