using System;
using System.Collections.Generic;

namespace blogAPI.Models
{
    public class Comment
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<Opinion> Likes { get; set; } = new List<Opinion>();
        public List<Opinion> Dislikes { get; set; } = new List<Opinion>();
    }
}