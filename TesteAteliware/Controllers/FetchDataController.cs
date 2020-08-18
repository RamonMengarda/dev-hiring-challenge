using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace TesteAteliware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FetchDataController : ControllerBase
    {
        private string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        [HttpGet]
        public IEnumerable<SearchResults> Get()
        {
            List<SearchResults> results = new List<SearchResults>();
            SearchResults item;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(string.Format("SELECT * FROM dbo.SearchResults WHERE favorite=1;"), connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                item = new SearchResults();
                item.Id = (int)reader.GetValue(0);
                item.Name = reader.GetValue(1).ToString();
                item.Description = reader.GetValue(2).ToString();
                item.CloneUrl = reader.GetValue(3).ToString();
                item.SvnUrl = reader.GetValue(4).ToString();
                item.WatchersCount = (int)reader.GetValue(5);
                item.Homepage = reader.GetValue(6).ToString();

                results.Add(item);
            }

            reader.Close();
            connection.Close();

            return results;
        }
    }
}
