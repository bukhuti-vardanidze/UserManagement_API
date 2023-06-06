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
            //  await AddPostDataInDb();
            // await AddAlbumsDataInDb();
            //await AddCommentsDataInDb();
            await AddPhotosDataInDb();


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

            string connectionString = "Server=localhost;Database=UserManagement_Db;Integrated security=true;Trusted_connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true; Encrypt=false";

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

        public static async Task AddAlbumsDataInDb()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            var response = await client.GetAsync("albums");
            var content = await response.Content.ReadAsStringAsync();

            List<Album> albums = JsonConvert.DeserializeObject<List<Album>>(content);

            foreach (var album in albums)
            {

                var fetchedPostData = $"User ID: {album.userId}, ID: {album.id},Title: {album.title})";

                Console.WriteLine(fetchedPostData);
            }


            string connectionString = "Server=localhost;Database=UserManagement_Db;Integrated security=true;Trusted_connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true; Encrypt=false";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var album in albums)
                {
                    string query = "INSERT INTO AlbumData (userId, id, title) VALUES (@userId, @id, @title)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", album.userId);
                        command.Parameters.AddWithValue("@id", album.id);
                        command.Parameters.AddWithValue("@title", album.title);

                        command.ExecuteNonQuery();
                    }
                }
            }


        }

        public static async Task AddCommentsDataInDb()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            var response = await client.GetAsync("comments");
            var content = await response.Content.ReadAsStringAsync();

            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(content);

            string connectionString = "Server=localhost;Database=UserManagement_Db;Integrated security=true;Trusted_connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true; Encrypt=false"; 


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var comment in comments)
                {
                    string query = "INSERT INTO CommentData (postId, id, name, email, body) VALUES (@postId, @id, @name, @email, @body)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@postId", comment.postId);
                        command.Parameters.AddWithValue("@id", comment.id);
                        command.Parameters.AddWithValue("@name", comment.name);
                        command.Parameters.AddWithValue("@email", comment.email);
                        command.Parameters.AddWithValue("@body", comment.body);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static async Task AddPhotosDataInDb()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            var response = await client.GetAsync("photos");
            var content = await response.Content.ReadAsStringAsync();

            List<Photo> photos = JsonConvert.DeserializeObject<List<Photo>>(content);

            string connectionString = "Server=localhost;Database=UserManagement_Db;Integrated security=true;Trusted_connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true; Encrypt=false";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var photo in photos)
                {
                    string query = "INSERT INTO PhotosData (albumId, id, title, url, thumbnailUrl) VALUES (@albumId, @id, @title, @url, @thumbnailUrl)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@albumId", photo.albumId);
                        command.Parameters.AddWithValue("@id", photo.id);
                        command.Parameters.AddWithValue("@title", photo.title);
                        command.Parameters.AddWithValue("@url", photo.url);
                        command.Parameters.AddWithValue("@thumbnailUrl", photo.thumbnailUrl);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }

 
}
