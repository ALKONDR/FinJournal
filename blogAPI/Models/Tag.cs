using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace blogAPI.Models
{
    public class Tag
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int UseAmount
        {
            get
            {
                return this.Stories.Count;
            }
        }
        public string Context { get; set; } = string.Empty;
        public List< Tuple<string, string> > Stories { get; set; } = new List< Tuple<string, string> >();

        public Tag(string context)
        {
            this.Context = context;
            this.Id = new ObjectId();
        } 
    }
}