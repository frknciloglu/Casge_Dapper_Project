using Casge_Dapper_Project.DAL.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Casge_Dapper_Project.Controllers
{
    public class HeadingController : Controller
    {
        private readonly string _connectionString = "Server=DESKTOP-PBKE4PO\\SQLEXPRESS; initial Catalog=CasgemDbDapper; integrated Security=true";
        
        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connectionString);
            var values = await connection.QueryAsync<Headings>("Select * From TblHeading");
            return View(values);
        }

        [HttpGet]
        public IActionResult AddHeading()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddHeading(Headings headings)
        {
            await using var connection=new SqlConnection(_connectionString);
            var query = $"Insert Into TblHeading (HeadingName,HeadingStatus) Values ('{headings.HeadingName}','True')";
            await connection.QueryAsync(query);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> DeleteHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($"Delete From TblHeading Where HeadinID = '{id}'");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult>UpdateHeading(int id)
        {
            await using var connection= new SqlConnection(_connectionString);
            var values = await connection.QueryFirstAsync<Headings>($"Select * From TblHeading Where HeadinID='{id}'");
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHeading(Headings headings)
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"Update TblHeading set HeadingName='{headings.HeadingName}' where HeadinID='{headings.HeadinID}'";
            await connection.QueryAsync(query);

            return RedirectToAction("Index");
        }
    }
}
