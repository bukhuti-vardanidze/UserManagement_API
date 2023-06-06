using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fetcher.Models
{
    public class Post
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
    }
}
