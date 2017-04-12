using System;

namespace blogAPI.Models
{
    public class Opinion
    {
        public DateTime Date { get; set; }
        public string Author { get; set; } = string.Empty;
    }
}