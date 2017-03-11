using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

using blogAPI.Interfaces;


namespace blogAPI.Models
{
    public class Story : IPostable
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public User Author { get; set; }
        public int ReadingTime { get; set; }
        public int Watches { get; set; }
        public List<Opinion> Likes { get; set; } = null;
        public List<Opinion> Dislikes { get; set; } = null;
        public string Content { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = null;
        public List<Comment> Comments { get; set; } = null;
    }
}