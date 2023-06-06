using Fetcher.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Fetcher
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await AddPostDataInDb();
        }

        public static async Task AddPostDataInDb()
        {

            HttpClient client = new()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            var response = await client.GetAsync("posts");
            var content = await response.Content.ReadAsStringAsync();

            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);


            foreach (var post in posts)
            {

                var fetchedPostData = $"User ID: {post.userId}, ID: {post.id},Title: {post.title}, Body: {post.body})";

                Console.WriteLine(fetchedPostData);
            }

            string connectionString = "Server=localhost;Database=UserManagement_Db;Integrated security=true;Trusted_connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true; Encrypt=false"; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var post in posts)
                {
                    string query = "INSERT INTO PostData (userId, id, title, body) VALUES (@userId, @id, @title, @body)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", post.userId);
                        command.Parameters.AddWithValue("@id", post.id);
                        command.Parameters.AddWithValue("@title", post.title);
                        command.Parameters.AddWithValue("@body", post.body);
                        command.ExecuteNonQuery();
                    }
                }
            }



        }
    }

 
}
