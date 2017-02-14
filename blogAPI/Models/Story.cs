using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using blogAPI.Interfaces;

namespace blogAPI.Models
{
    public class Story : IPostable
    {
        [BsonId]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public User Author { get; set; }
        public string Content { get; set; } = string.Empty;
        public int ReadingTime { get; set; }
        public List<Tag> Tags { get; set; } = null;
        public List<Comment> Comments { get; set; } = null;
        public int Watches { get; set; }
        public List<Like> Likes { get; set; } = null;
        public List<Dislike> Dislikes { get; set; } = null;
    }
}