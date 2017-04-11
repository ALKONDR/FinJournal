using System;
using System.Collections.Generic;
// using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

using blogAPI.Interfaces;


namespace blogAPI.Models
{
    public class Story : IPostable
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Author { get; set; } = string.Empty;
        public int ReadingTime { get; set; } = 0;
        public int Watches { get; set; } = 0;
        public List<Opinion> Likes { get; set; } = new List<Opinion>();
        public List<Opinion> Dislikes { get; set; } = new List<Opinion>();
        public string Content { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}