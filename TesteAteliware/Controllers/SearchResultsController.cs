using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using Octokit;

namespace TesteAteliware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchResultsController : ControllerBase
    {
        private string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        private SearchRepositoryResult result;

        [HttpGet]
        public IEnumerable<SearchResults> Get(string searchTerm, string allTopics)
        {
            var topics = allTopics.Replace("\"", "'").Replace("[", "").Replace("]", "");

            var task = Task.Run(() => searchGitHubRepository(searchTerm));

            Task.WaitAll(task);

            var result = Enumerable.Range(0, this.result.Items.Count).Select(index => new SearchResults
            {
                Id = this.result.Items[index].Id,
                Name = this.result.Items[index].Name,
                Description = this.result.Items[index].Description,
                CloneUrl = this.result.Items[index].CloneUrl,
                SvnUrl = this.result.Items[index].SvnUrl,
                WatchersCount = this.result.Items[index].WatchersCount,
                Homepage = this.result.Items[index].Homepage,
            })
            .ToArray();

            addToDatabase(searchTerm, result);

            return getFromDatabase(topics);
        }

        public async Task<SearchRepositoryResult> searchGitHubRepository(string searchTerm)
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("TesteAteliware"));

            var request = new SearchRepositoriesRequest(searchTerm);
            var result = await githubClient.Search.SearchRepo(request);

            this.result = result;

            return result;
        }

        public void addToDatabase(string searchTerm, IEnumerable<SearchResults> items)
        {
            var queryString = "";

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(queryString, connection);

            foreach (SearchResults item in items)
            {
                queryString = "SELECT * FROM dbo.SearchResults WHERE id = " + item.Id + ";";

                command.CommandText = queryString;

                var reader = command.ExecuteReader();

                while (reader.Read()){ }

                var hasRows = reader.HasRows;
                reader.Close();

                if (hasRows == false)
                {
                    var description = "";

                    if (item.Description != null)
                    {
                        description = item.Description.Replace("'", "");
                    }


                    queryString = "insert into dbo.SearchResults " +
                                      "(id, name, description, cloneUrl, svnUrl, watchersCount, homepage, topic, favorite) " +
                                      "values (" +
                                                   item.Id + "," +
                                             "'" + item.Name + "'," +
                                             "'" + description + "'," +
                                             "'" + item.CloneUrl + "'," +
                                             "'" + item.SvnUrl + "'," +
                                                   item.WatchersCount + "," +
                                             "'" + item.Homepage + "'," +
                                             "'" + searchTerm + "'," +
                                                   0 + ");";

                    command.CommandText = queryString;

                    command.ExecuteNonQuery();
                }
            }

            connection.Close();
        }

        public List<SearchResults> getFromDatabase(string topics)
        {
            List<SearchResults> results = new List<SearchResults>();
            SearchResults item;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(string.Format("SELECT * FROM dbo.SearchResults WHERE topic in ({0});", topics), connection);
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
