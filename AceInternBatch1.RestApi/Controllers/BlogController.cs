using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AceInternBatch1.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        public BlogController()
        {
            //_connectionStringBuilder = new SqlConnectionStringBuilder();
            // _connectionStringBuilder.DataSource = "DESKTOP-G341RUV";
            // _connectionStringBuilder.InitialCatalog = "AceInternBatch1";
            ///_connectionStringBuilder.UserID = "sa";
            // _connectionStringBuilder.Password = "sasa@123";

            _connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "DESKTOP-G341RUV",
                InitialCatalog = "AceInternBatch1",
                UserID = "sa",
                Password = "sasa@123"
            };

        }
        [HttpGet]
        public IActionResult GetBlogs()
        {


            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var lst = db.Query<TblBlog>(Queries.BlogList).ToList();
            // string a = lst[0].Call();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {


            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<TblBlog>(Queries.BlogById, new { BlogId = id }).FirstOrDefault();
            if (item is null)
                return NotFound("No data found");

            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlogs(TblBlog blog)

        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Queries.BlogCreate, blog);
            string message = result > 0 ? "Saving Successful." : "Saving Failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, TblBlog blog)
        {   
            blog.BlogID = id;
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Queries.BlogUpadate, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,TblBlog blog)
        {
            return Ok("PatchBlogs");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Queries.BlogDelete, new { BlogId = id});
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";
            return Ok(message);
        }
    }

    public class TblBlog
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
    }
    public static class Queries

    { 

        public static string BlogList { get; } = @"SELECT [BlogId]
                ,[BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent]
                 FROM[dbo].[Tbl_Blog]";
        public static string BlogById { get; } = @"SELECT [BlogId]
                ,[BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent]
                 FROM[dbo].[Tbl_Blog] where BlogId = @BlogId";
        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
		   @BlogAuthor,
		   @BlogContent)";
        public static string BlogUpadate { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
		   @BlogAuthor,
		   @BlogContent)";

        public static string BlogDelete { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
		   @BlogAuthor,
		   @BlogContent)";

    }

}
