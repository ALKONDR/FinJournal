using System;
using System.Collections.Generic;

namespace blogAPI.Models
{
    public class Comment
    {
        public DateTime Date { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }
        public List<Opinion> Likes { get; set; }
        public List<Opinion> Dislikes { get; set; }
    }
}