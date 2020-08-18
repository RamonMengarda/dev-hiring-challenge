using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace TesteAteliware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateFavoriteController : ControllerBase
    {
        private string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        [HttpGet]
        public void Get(string repoId)
        {
            var queryString = "";

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(queryString, connection);

            queryString = "update dbo.SearchResults set favorite = 1 where id=" + repoId + ";";

            command.CommandText = queryString;

            command.ExecuteNonQuery();

            return;
        }
    }
}
